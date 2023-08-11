using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum StatusType
{
    None = 0,
    /// <summary>
    /// 체력
    /// </summary>
    HP = 100,
    /// <summary>
    /// 최대 체력
    /// </summary>
    MaxHP = 101,
    /// <summary>
    /// 체력 회복량
    /// </summary>
    RecoverHP = 102,
    /// <summary>
    /// 체력 흡수량
    /// </summary>
    AbsorptionHP = 103,

    /// <summary>
    /// 마나
    /// </summary>
    MP = 200,
    /// <summary>
    /// 최대 마나
    /// </summary>
    MaxMP = 201,
    /// <summary>
    /// 마나 회복량
    /// </summary>
    RecoverMP = 202,
    /// <summary>
    /// 마나 흡수량
    /// </summary>
    AbsorptionMP = 203,
    /// <summary>
    /// 마나 흡수량
    /// </summary>
    SkillSpendMP = 204,


    /// <summary>
    /// 최소 공격력
    /// </summary>
    MinAttackPower = 300,
    /// <summary>
    /// 최대 공격력
    /// </summary>
    MaxAttackPower = 301,
    /// <summary>
    /// 스킬 공격력
    /// </summary>
    SkillAttackPower = 302,

    /// <summary>
    /// 치명타 확률
    /// </summary>
    CriticalPercent = 400,
    /// <summary>
    /// 치명타 공격력
    /// </summary>
    CriticalAttackPower = 401,


    /// <summary>
    /// 방어력
    /// </summary>
    Defence = 500,
    /// <summary>
    /// 상태 이상 내성
    /// </summary>
    DefenceCrowd = 501,
    /// <summary>
    /// 속성 내성
    /// </summary>
    DefenceElemental = 502,

    /// <summary>
    /// 공격 속도
    /// </summary>
    AttackSpeed = 600,
    /// <summary>
    /// 공격 사거리
    /// </summary>
    AttackDistance = 601,

    /// <summary>
    /// 이동 속도
    /// </summary>
    MoveSpeed = 700,
    /// <summary>
    /// 점프력
    /// </summary>
    JumpPower = 701,
    /// <summary>
    /// 점프력
    /// </summary>
    SkillCoolTime = 702,

    /// <summary>
    /// 건강
    /// </summary>
    Health = 800,
    /// <summary>
    /// 최대 건강
    /// </summary>
    MaxHealth = 801,

    /// <summary>
    /// 컨디션
    /// </summary>
    Condition = 900,
    /// <summary>
    /// 최대 컨디션
    /// </summary>
    MaxCondition = 901,
}
