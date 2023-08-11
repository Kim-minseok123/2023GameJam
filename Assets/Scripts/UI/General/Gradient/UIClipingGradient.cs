using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIClipingGradient : MonoBehaviour
{
    public Color startColor = Color.white;
    public Color endColor = Color.white;

    [SerializeField]
    private Image[] clipCells;

    [SerializeField]
    [ReadOnly]
    private List<Color> clipColors;

    private void LateUpdate()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        clipColors.Clear();

        for (var i = 0; i < clipCells.Length; ++i)
        {
            var color = Color.Lerp(startColor, endColor, (i / (float)clipCells.Length));
            clipColors.Add(color);
            clipCells[i].color = color;
        }
    }

}

