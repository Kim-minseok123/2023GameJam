using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    protected bool useLerp = true;
    [SerializeField]
    protected float lerpDamping = 0.5f;

    public void SetTarget(Transform target)
    {
        this.target = target;
        if (target != null)
        {
            transform.position = GetTargetPosition();
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            var targetPos = GetTargetPosition();
            if (useLerp)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * lerpDamping);
            }
            else
            {
                transform.position = targetPos;
            }
        }
    }

    private Vector3 GetTargetPosition()
    {
        return target.position + offset;
    }

}
