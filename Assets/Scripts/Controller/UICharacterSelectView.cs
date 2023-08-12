using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectView : UIBaseView
{
    [System.Serializable]
    public class Slot
    {
        public CharacterData characterData;
        public UICharacterSelectSlot playerSlot;
    }

    [SerializeField]
    private Transform[] pivots;

    [SerializeField]
    private List<Slot> characterSlots;

    [SerializeField]
    private float rotateTime = 0.25f;


    [SerializeField]
    private UIBaseText nameLabelText;

    [SerializeField]
    private UIBaseText infomationNameLabelText;
    [SerializeField]
    private UIBaseText infomationDescriptionText;
    [SerializeField]
    private UIBaseText infomationJobLabelText;

    [SerializeField]
    private GameObject skillDescriptionObject;
    [SerializeField]
    private UIBaseImage skillIconImage;
    [SerializeField]
    private UIBaseText skillNameText;
    [SerializeField]
    private UIBaseText skillDescriptionText;

    private int selectIndex = 0;

    private bool isSelection = false;
    private bool isShowSkill = false;

    public override void Init(UIData uiData)
    {

    }


    protected override void Start()
    {
        RotateSlots();
    }

    public void Update()
    {
        if (GameManager.Instance.isPaused != true) { return; }
        if (Input.GetKeyDown(KeyCode.A) && !isSelection)
        {
            Prev();
            isSelection = true;  // Ű�� �������ٰ� ǥ��
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isSelection)
        {
            Next();
            isSelection = true;  // Ű�� �������ٰ� ǥ��
        }

        // A�� D Ű�� �������� �� isclick�� �ٽ� false�� ����
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isSelection = false;
        }
    }

    public void Next()
    {
        selectIndex = (int)Mathf.Repeat(selectIndex + 1, characterSlots.Count);
        RotateSlots();
    }

    public void Prev()
    {
        selectIndex = (int)Mathf.Repeat(selectIndex - 1, characterSlots.Count);
        RotateSlots();
    }

    private void RotateSlots()
    {
        for (var i = 0; i < characterSlots.Count; ++i)
        {
            var slot = characterSlots[(int)Mathf.Repeat(selectIndex + i, characterSlots.Count)];
            slot.playerSlot.MoveTo(pivots[i].position, rotateTime);
            if (i == 0)
            {
                slot.playerSlot.PlayTween("Show");
            }
            else
            {
                slot.playerSlot.PlayTween("Hide");
            }
        }
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        var characterData = characterSlots[selectIndex].characterData;
        var nameText = $"{characterData.CharacterName} ({characterData.Job})";

        nameLabelText.SetText(nameText);

        infomationNameLabelText.SetText(characterData.CharacterName);
        infomationJobLabelText.SetText(characterData.Job);
        infomationDescriptionText.SetText(characterData.Context);

        skillIconImage.SetImage(characterData.SkillIcon);
        skillNameText.SetText(characterData.SkillName);
        skillDescriptionText.SetText(characterData.SkillContext);
    }

    public void ShowSkill()
    {
        skillDescriptionObject.SetActive(true);
        Debug.Log("s");
    }

    public void HideSkill()
    {
        skillDescriptionObject.SetActive(false);
    }

    public string GetSelectName()
    {
        return characterSlots[selectIndex].characterData.KeyName;
    }
}
