using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPoint;

    private bool isLooped = false;

    [SerializeField]
    private float moveTime = 1f;

    private float currentMoveTime = 0f;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (startPoint == null || endPoint == null || targetTransform == null)
            return;

        Gizmos.DrawLine(startPoint.position, targetTransform.position);
        Gizmos.DrawLine(endPoint.position, targetTransform.position);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startPoint.position, 0.25f);
        Gizmos.DrawSphere(endPoint.position, 0.25f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(targetTransform.position, 0.25f);
    }

    private void FixedUpdate()
    {
        var movePosition = Vector3.zero;

        currentMoveTime += Time.deltaTime;
        var t = currentMoveTime / moveTime;
        if (isLooped)
        {
            movePosition = Vector3.Lerp(endPoint.position, startPoint.position, t);

            if (t >= 1f)
            {
                currentMoveTime = 0;
                isLooped = false;
            }
        }
        else
        {
            movePosition = Vector3.Lerp(startPoint.position, endPoint.position, t);
            if (t >= 1f)
            {
                currentMoveTime = 0;
                isLooped = true;
            }
        }

        targetTransform.position = movePosition;
    }

}
