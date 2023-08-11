using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIResolutionSetting : MonoBehaviour
{
    [System.Serializable]
    public class ScreenModeGroup
    {
        public string name;
        public FullScreenMode screenMode;
    }

    [SerializeField]
    private List<Vector2Int> resolutionList = new List<Vector2Int>();

    [SerializeField]
    private TMP_Dropdown dropDownMenu;

    [SerializeField]
    private SerializableDictionary<FullScreenMode, ScreenModeGroup> screenModeTable;

    [SerializeField]
    private UIBaseText screenModeText;
    private int screenModeIndex;

    private List<FullScreenMode> screenKeyList = new List<FullScreenMode>();

    private void Awake()
    {
        var currentResolution = SaveLoadSystem.Instance.SaveLoadData.screenResolution;
        var menuIndex = resolutionList.FindIndex(x => x == currentResolution);
        dropDownMenu.SetValueWithoutNotify(menuIndex);

        screenKeyList = screenModeTable.Keys.ToList();
        var screenModeGroup = screenModeTable[Screen.fullScreenMode];
        screenModeText.SetText(screenModeGroup.name);
        screenModeIndex = screenKeyList.FindIndex(item => item == Screen.fullScreenMode);
    }

    public void ResetSetting()
    {
        var currentResolution = resolutionList[1];
        var menuIndex = resolutionList.FindIndex(x => x == currentResolution);
        dropDownMenu.SetValueWithoutNotify(menuIndex);
        SaveLoadSystem.Instance.SaveLoadData.screenResolution = currentResolution;

        var screenModeGroup = screenModeTable[FullScreenMode.FullScreenWindow];
        screenModeText.SetText(screenModeGroup.name);
        screenModeIndex = screenKeyList.FindIndex(item => item == FullScreenMode.FullScreenWindow);
        SaveLoadSystem.Instance.SaveLoadData.screenMode = FullScreenMode.FullScreenWindow;

        UpdateScreen();
    }


    public void ChangeResolution(int index)
    {
        var resolution = resolutionList[index];
        SaveLoadSystem.Instance.SaveLoadData.screenResolution = resolution;
        UpdateScreen();
    }

    public void ChangeScreenMode(int direction)
    {
        screenModeIndex = (int)Mathf.Repeat(screenModeIndex + direction, screenKeyList.Count);

        var screenKey = screenKeyList[screenModeIndex];
        var screenModeGroup = screenModeTable[screenKey];
        screenModeText.SetText(screenModeGroup.name);
        SaveLoadSystem.Instance.SaveLoadData.screenMode = screenKey;

        UpdateScreen();
    }

    private void UpdateScreen()
    {
        var screenMode = SaveLoadSystem.Instance.SaveLoadData.screenMode;
        var resolution = SaveLoadSystem.Instance.SaveLoadData.screenResolution;

        Screen.SetResolution(resolution.x, resolution.y, screenMode);
        SaveLoadSystem.Instance.Save();
    }

}
