using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEditorInternal;

public class AutoPropBatchBrushEditor : EditorWindow
{
    public struct PlacecdObject
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public GameObject prefab;
    }

    private Vector2 scrollPos = Vector2.zero;

    private bool isDraw = true;
    private bool isMouseDown = false;
    private bool isStraightMode = false;

    private bool isSnapY = false;
    private Vector2 snapRayDirection = Vector2.down;
    private float snapDistanceY = 0f;

    private bool useTileMapTerrain = false;
    private int tilemapBrushSize = 1;

    private Tilemap drawTilemap;
    private TilemapCollider2D tilemapCollider2D;
    private CompositeCollider2D compositeCollider2D;

    private int drawDensity = 5;
    private bool useRandomDensity = false;
    private AmountRangeInt randomDensityRange = new AmountRangeInt(1, 10);

    private Vector3 prevPoint = Vector3.zero;
    private List<Vector3> drawPointList = new List<Vector3>();
    private List<Vector3> drawNormalPointList = new List<Vector3>();

    private float stepDistance = 3f;

    private float drawStroke;
    private float drawNormalHeightOffset;

    private Color drawBrushColor = Color.green;
    private Color drawLineColor = Color.cyan;
    private Color drawNormalColor = Color.red;

    private AutoPropBatchFolderData autoPropBatchFolderData;
    private SerializedObject autoPropBatchFolderDataSO;

    private ReorderableList reorderableList;

    private bool showColorOptions = true;
    private bool showDrawOptions = true;
    private bool showGuideMessages = true;

    private List<PlacecdObject> placecdObjectList = new List<PlacecdObject>();

    private static Transform brushTransfom;
    private Material brushMaterial;

    [MenuItem("Window/TileMap Prop Brush")]
    private static void Init()
    {
        AutoPropBatchBrushEditor window = (AutoPropBatchBrushEditor)GetWindow(typeof(AutoPropBatchBrushEditor), false, "TileMap Prop Brush", true);
        window.Show();
    }

    protected void OnEnable()
    {
        SceneView.duringSceneGui -= UpdateScene;
        SceneView.duringSceneGui += UpdateScene;

        CreateBrushObject();

        if (autoPropBatchFolderData != null)
        {
            RefreshBrushData();
        }
    }

    protected void OnDisable()
    {
        SceneView.duringSceneGui -= UpdateScene;

        if (brushTransfom != null)
        {
            DestroyImmediate(brushTransfom.gameObject);
        }
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUIUtility.labelWidth = 200;

        EditorGUI.indentLevel++;

        if (showGuideMessages)
        {
            EditorGUILayout.HelpBox("Scene 드로잉 기능을 켜고 끕니다.", MessageType.Info, true);
        }

        isDraw = EditorGUILayout.Toggle("Drawable", isDraw);

        EditorGUILayout.Space(10);

        if (showGuideMessages)
        {
            EditorGUILayout.HelpBox("바닥 지형에 프랍을 자동으로 붙입니다.", MessageType.Info, true);
        }

        isSnapY = EditorGUILayout.Toggle("Snap Y Axis", isSnapY);
        snapDistanceY = EditorGUILayout.FloatField("Snap Distance", snapDistanceY);
        snapRayDirection = EditorGUILayout.Vector2Field("Snap RayDirection", snapRayDirection);

        EditorGUILayout.Space(10);

        if (showGuideMessages)
        {
            EditorGUILayout.HelpBox("드로잉 시 타일 맵 지형도 같이 그립니다.", MessageType.Info, true);
        }
        useTileMapTerrain = EditorGUILayout.Toggle("Use Draw TileMap Terrain", useTileMapTerrain);

        if (useTileMapTerrain)
        {
            tilemapBrushSize = EditorGUILayout.IntSlider("TileMap Brush Size", tilemapBrushSize, 1, 100);

            EditorGUI.BeginChangeCheck();
            drawTilemap = EditorGUILayout.ObjectField("Tilemap", drawTilemap, typeof(Tilemap), true) as Tilemap;

            if (EditorGUI.EndChangeCheck())
            {
                if (drawTilemap != null)
                {
                    tilemapCollider2D = drawTilemap.GetComponent<TilemapCollider2D>();
                    compositeCollider2D = drawTilemap.GetComponent<CompositeCollider2D>();
                }
                else
                {
                    tilemapCollider2D = null;
                    compositeCollider2D = null;
                }
            }
        }

        EditorGUILayout.Space(10);

        if (showGuideMessages)
        {
            EditorGUILayout.HelpBox("랜덤 범위 밀도 값 사용 여부.", MessageType.Info, true);
        }
        useRandomDensity = EditorGUILayout.Toggle("Use Density Range", useRandomDensity);

        EditorGUI.indentLevel++;
        if (useRandomDensity)
        {
            if (showGuideMessages)
            {
                EditorGUILayout.HelpBox("밀도의 랜덤 범위 값", MessageType.Info, true);
            }
            randomDensityRange.min = EditorGUILayout.IntField("Draw Density Min", randomDensityRange.min);
            randomDensityRange.max = EditorGUILayout.IntField("Draw Density Max", randomDensityRange.max);
        }
        else
        {
            if (showGuideMessages)
            {
                EditorGUILayout.HelpBox("밀도의 값", MessageType.Info, true);
            }
            drawDensity = EditorGUILayout.IntField("Draw Density", drawDensity);
        }
        EditorGUI.indentLevel--;

        if (showGuideMessages)
        {
            EditorGUILayout.HelpBox("오브젝트 위치 간격(거리 단위 : M)", MessageType.Info, true);
        }
        stepDistance = EditorGUILayout.Slider("Step Distance", stepDistance, 0.01f, 100f);

        EditorGUILayout.Space(10);

        if (showGuideMessages)
        {
            EditorGUILayout.HelpBox("컬러 옵션", MessageType.Info, true);
        }
        showColorOptions = EditorGUILayout.Foldout(showColorOptions, "Color Options");

        if (showColorOptions)
        {
            EditorGUI.indentLevel++;
            EditorGUI.BeginChangeCheck();
            drawBrushColor = EditorGUILayout.ColorField("Draw Brush Color", drawBrushColor);
            if (EditorGUI.EndChangeCheck())
            {
                UpdateBrushColor();
            }
            drawLineColor = EditorGUILayout.ColorField("Draw Line Color", drawLineColor);
            drawNormalColor = EditorGUILayout.ColorField("Draw Normal Color", drawNormalColor);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space(10);

        if (showGuideMessages)
        {
            EditorGUILayout.HelpBox("드로잉 옵션", MessageType.Info, true);
        }
        showDrawOptions = EditorGUILayout.Foldout(showDrawOptions, "Draw Options");
        if (showDrawOptions)
        {
            EditorGUI.indentLevel++;
            drawStroke = EditorGUILayout.Slider("Draw Stroke", drawStroke, 1f, 100f);
            drawNormalHeightOffset = EditorGUILayout.Slider("Draw Normal Height Offset", drawNormalHeightOffset, 1f, 100f);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space(10);

        if (showGuideMessages)
        {
            EditorGUILayout.HelpBox("브러쉬 데이터 파일", MessageType.Info, true);
        }
        EditorGUI.BeginChangeCheck();
        autoPropBatchFolderData = EditorGUILayout.ObjectField("BrushData", autoPropBatchFolderData, typeof(AutoPropBatchFolderData), false) as AutoPropBatchFolderData;
        if (EditorGUI.EndChangeCheck())
        {
            if (autoPropBatchFolderData != null)
            {
                RefreshBrushData();
            }
            else
            {
                autoPropBatchFolderDataSO = null;
            }
        }

        /*
        if (autoPropBatchFolderDataSO != null)
        {
            autoPropBatchFolderDataSO.Update();
            reorderableList.DoLayoutList();
            autoPropBatchFolderDataSO.ApplyModifiedProperties();
        }
        */

        EditorGUILayout.Space(10);

        EditorGUILayout.EndScrollView();
    }

    private void OnSceneGUI()
    {
        Debug.Log("asd");
    }

    private void CreateBrushObject()
    {
        if (brushTransfom == null)
        {
            var brushObject = GameObject.Find("[PrefabBrushObject]");

            if (brushObject == null)
            {
                brushObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                brushObject.name = "[PrefabBrushObject]";
                DestroyImmediate(brushObject.GetComponent<Collider>());

                brushMaterial = new Material(Shader.Find("Unlit/Transparent Colored"));
                brushMaterial.SetColor("_Color", drawBrushColor);
                brushMaterial.hideFlags = HideFlags.HideAndDontSave;

                Renderer renderer = brushObject.GetComponent<Renderer>();
                renderer.sharedMaterial = brushMaterial;
            }

            brushObject.hideFlags = HideFlags.HideAndDontSave;

            brushTransfom = brushObject.transform;
            brushTransfom.rotation = Quaternion.identity;
            brushTransfom.localScale = Vector3.one * Mathf.Max(1f, tilemapBrushSize);
        }
    }
    private void UpdateBrushColor()
    {
        brushMaterial.SetColor("_Color", drawBrushColor);
    }

    private void UpdateBrushObject(Vector3 point)
    {
        brushTransfom.position = point;
        brushTransfom.localScale = Vector3.one * Mathf.Max(1f, tilemapBrushSize);
    }

    private void UpdateScene(SceneView sceneView)
    {
        DisplayLines();

        if (isDraw)
        {
            Event e = Event.current;
            ProcecssKeyInput(sceneView, e);
            ProcessMouseInput(sceneView, e);
            sceneView.Repaint();
        }
    }

    private void DisplayLines()
    {
        Handles.color = drawLineColor;
        for (var i = 0; i < drawPointList.Count - 1; ++i)
        {
            Handles.DrawLine(drawPointList[i], drawPointList[i + 1], drawStroke);
        }

        Handles.color = drawNormalColor;
        for (var i = 0; i < drawNormalPointList.Count; ++i)
        {
            Handles.DrawLine(drawPointList[i], drawPointList[i] + drawNormalPointList[i] * drawNormalHeightOffset, drawStroke);
        }
    }

    private void ProcessMouseInput(SceneView sceneView, Event e)
    {
        int id = GUIUtility.GetControlID(FocusType.Passive);

        if (e.button == 0 && autoPropBatchFolderData != null)
        {
            var mouseWorldPosition = GetWorldPosition(sceneView);
            UpdateBrushObject(mouseWorldPosition);
            sceneView.Repaint();
            switch (e.type)
            {
                case EventType.MouseMove:
                case EventType.MouseDrag:
                case EventType.Repaint:
                case EventType.Layout:
                    if (isMouseDown)
                    {
                        DrawTile(mouseWorldPosition);

                        if (isStraightMode)
                        {
                            if (drawPointList.Count > 2)
                            {
                                drawPointList.RemoveRange(1, drawPointList.Count - 1);
                                drawNormalPointList.RemoveRange(1, drawNormalPointList.Count - 1);
                            }

                            var pivot = drawPointList[0];
                            var diffVec = mouseWorldPosition - pivot;
                            var distance = diffVec.magnitude;
                            var direction = diffVec.normalized;

                            for (var i = 1; ; ++i)
                            {
                                var splitDistance = stepDistance * i;
                                if (splitDistance > distance)
                                {
                                    break;
                                }

                                var splitWorldPosition = pivot + direction * splitDistance;

                                AddDrawPoint(splitWorldPosition);
                            }
                        }
                        else
                        {
                            var distance = (prevPoint - mouseWorldPosition).magnitude;
                            if (distance >= stepDistance)
                            {
                                prevPoint = mouseWorldPosition;

                                AddDrawPoint(mouseWorldPosition);
                            }
                        }

                        HandleUtility.AddDefaultControl(id);
                    }
                    break;
                case EventType.MouseDown:
                    isMouseDown = true;
                    prevPoint = mouseWorldPosition;

                    if (useTileMapTerrain)
                    {
                        Undo.RegisterCompleteObjectUndo(drawTilemap, "Draw Tilemap");
                    }

                    DrawTile(mouseWorldPosition);
                    AddDrawPoint(mouseWorldPosition);

                    GUIUtility.hotControl = id;
                    e.Use();
                    break;
                case EventType.MouseUp:
                    if (isMouseDown)
                    {
                        for (var i = 0; i < drawPointList.Count; ++i)
                        {
                            var density = drawDensity;
                            if (useRandomDensity)
                            {
                                density = randomDensityRange.GetRandomAmount();
                            }

                            for (var k = 0; k < density; ++k)
                            {
                                var selectElement = autoPropBatchFolderData.GetRandomElement();
                                var placePosition = drawPointList[i] + selectElement.GetPosition();
                                var normalPoint = drawNormalPointList[i];
                                var placeRotation = selectElement.GetRotation();
                                var placeScale = selectElement.prefab.transform.localScale + selectElement.GetScale();

                                placecdObjectList.Add(new PlacecdObject()
                                {
                                    position = placePosition,
                                    rotation = placeRotation,
                                    scale = placeScale,
                                    prefab = selectElement.prefab
                                });
                            }
                        }

                        foreach (var item in placecdObjectList)
                        {
                            var placecdObject = (GameObject)PrefabUtility.InstantiatePrefab(item.prefab);
                            placecdObject.transform.SetPositionAndRotation(item.position, item.rotation);
                            placecdObject.transform.localScale = item.scale;

                            Undo.RegisterCreatedObjectUndo(placecdObject, "Undo PrefabBrush");
                        }

                        placecdObjectList.Clear();
                        drawPointList.Clear();
                        drawNormalPointList.Clear();

                        isMouseDown = false;
                        GUIUtility.hotControl = 0;
                        e.Use();
                    }
                    break;
            }
        }
    }

    private void AddDrawPoint(Vector3 mouseWorldPosition)
    {
        var position = mouseWorldPosition;
        var normal = Vector3.up;

        if (isSnapY)
        {
            position = GetRayCastHitPoint(mouseWorldPosition);
        }

        drawPointList.Add(position);
        drawNormalPointList.Add(normal);
    }

    private void ProcecssKeyInput(SceneView sceneView, Event e)
    {
        if (e.keyCode == KeyCode.None)
            return;

        switch (e.keyCode)
        {
            case KeyCode.LeftShift:
                {
                    if (e.type == EventType.KeyDown)
                    {
                        isStraightMode = true;
                    }
                    else if (e.type == EventType.KeyUp)
                    {
                        isStraightMode = false;
                    }
                }
                break;
        }
    }

    private void RefreshBrushData()
    {
        autoPropBatchFolderDataSO = new SerializedObject(autoPropBatchFolderData);
        /*
        reorderableList = new ReorderableList(autoPropBatchFolderDataSO, autoPropBatchFolderDataSO.FindProperty("elementDataList"), true, true, true, true);

        reorderableList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Element List");
        reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            rect.y += 2f;
            GUIContent objectLabel = new GUIContent($"Item {index}");
            EditorGUI.PropertyField(rect, reorderableList.serializedProperty.GetArrayElementAtIndex(index), objectLabel, true);
        };
        reorderableList.elementHeightCallback = (index) =>
        {
            return GetHeight(reorderableList.serializedProperty.GetArrayElementAtIndex(index));
        };
        */
    }

    private float GetHeight(SerializedProperty brushElement)
    {
        var isExpended = brushElement.isExpanded;

        var height = EditorGUIUtility.singleLineHeight;

        if (isExpended)
        {
            height += EditorGUIUtility.singleLineHeight * 3;
        }

        return height;
    }

    public void CreatePrefab(Vector3 position)
    {
        var elementData = autoPropBatchFolderData.GetRandomElement();

        var placePosition = position + elementData.GetPosition();
        var placeRotation = elementData.GetRotation();
        var placeScale = elementData.GetScale();

        var placeObject = (GameObject)PrefabUtility.InstantiatePrefab(elementData.prefab) as GameObject;
        placeObject.transform.SetLocalPositionAndRotation(placePosition, placeRotation);
        placeObject.transform.localScale = placeScale;

        Undo.RegisterCreatedObjectUndo(placeObject, "AutoPropBatch Brush");
    }

    private Vector3 GetWorldPosition(SceneView sceneView)
    {
        Vector3 mousePosition = Event.current.mousePosition;

        float mult = 1;
#if UNITY_5_4_OR_NEWER
        mult = EditorGUIUtility.pixelsPerPoint;
#endif

        mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y * mult;
        mousePosition.x *= mult;

        Vector3 worldPosition = sceneView.camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f;

        return worldPosition;
    }

    private Vector3 GetRayCastHitPoint(Vector3 point)
    {
        Vector2 origin = point;
        origin += CheckDrawableTile() ? Vector2.up * (tilemapBrushSize * 2f) : Vector2.zero;
        RaycastHit2D hit = Physics2D.Raycast(origin, snapRayDirection, snapDistanceY + tilemapBrushSize * 2f);

        return hit.collider != null ? hit.point : point;
    }

    private void DrawTile(Vector3 mouseWorldPosition)
    {
        if (!CheckDrawableTile())
            return;

        var center = drawTilemap.WorldToCell(mouseWorldPosition); //= mouseWorldPosition.FloorInt()

        if (tilemapBrushSize > 1)
        {
            for (var x = -tilemapBrushSize * 0.5f; x < tilemapBrushSize * 0.5f; ++x)
            {
                for (var y = -tilemapBrushSize * 0.5f; y < tilemapBrushSize * 0.5f; ++y)
                {
                    var worldPoint = center + new Vector3(x + 0.5f,y + 0.5f);
                    var cellPoint = drawTilemap.WorldToCell(worldPoint);

                    drawTilemap.SetTile(cellPoint, autoPropBatchFolderData.TerrainTile);
                }
            }
        }
        else
        {
            drawTilemap.SetTile(center, autoPropBatchFolderData.TerrainTile);
        }

        tilemapCollider2D.ProcessTilemapChanges();
        compositeCollider2D.GenerateGeometry();
    }

    private bool CheckDrawableTile()
    {
        return (useTileMapTerrain && autoPropBatchFolderData.TerrainTile != null
            && tilemapCollider2D != null && compositeCollider2D != null);
    }
}
