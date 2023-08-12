using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData", order = 0)]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private string characterName;
    public string CharacterName => characterName;

    [SerializeField]
    private string job;
    public string Job => job;

    [SerializeField]
    [TextArea]
    private string context;
    public string Context => context;

    [SerializeField]
    private Sprite skillIcon;
    public Sprite SkillIcon => skillIcon;


    [SerializeField]
    private string skillName;
    public string SkillName => skillName;

    [SerializeField]
    [TextArea]
    private string skillContext;
    public string SkillContext => skillContext;

}
