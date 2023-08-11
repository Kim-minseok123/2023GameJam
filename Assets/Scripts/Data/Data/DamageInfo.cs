using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public struct DamageInfo
{
    public GameObject owner;
    public string ownerTag;

    public string identityKey;

    public bool isHit;
    public bool isCritical;
    public bool isKnockBack;
    public bool isKill;
    public bool isSturn;
    public bool isReSpawn;

    public float damage;

    public Vector3 hitPoint;
    public Vector3 hitNormal;
    public Vector3 hitDirection;

    public float stunTime;

    public float knockBackPower;
    public AnimationCurve knockBackXAxisCurve;
    public AnimationCurve knockBackYAxisCurve;
    public float knockBackTime;
}