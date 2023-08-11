using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingPopup : UIBasePopup
{
    [System.Serializable]
    public class ContentGroup
    {
        public GameObject content;

        public void SetActive(bool isActive)
        {
            content.SetActive(isActive);
        }
    }

    [SerializeField]
    private string defaultViewCategory;

    private string currentViewCategory;

    [SerializeField]
    private SerializableDictionary<string, ContentGroup> contentGroups;

    public override void Init(UIData uiData)
    {
        foreach (var group in contentGroups.Values)
        {
            group.SetActive(false);
        }

        ChangeCategory(defaultViewCategory);
    }

    public void OnSelectCategory(string category)
    {
        ChangeCategory(category);
    }

    private void ChangeCategory(string category)
    {
        if (!string.IsNullOrEmpty(currentViewCategory))
        {
            contentGroups[currentViewCategory].SetActive(false);
        }

        contentGroups[category].SetActive(true);
        currentViewCategory = category;
    }

    public override void Close()
    {
        SaveLoadSystem.Instance.Save();
        base.Close();
    }

    public override void BeginClose()
    {
        base.BeginClose();
    }

}
