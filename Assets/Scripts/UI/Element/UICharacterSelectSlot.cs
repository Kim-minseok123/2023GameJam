using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelectSlot : MonoBehaviour
{
    private UITweenAnimator tweenAnimator;

    private Coroutine moveCoRoutine;

    private void Awake()
    {
        tweenAnimator = GetComponent<UITweenAnimator>();
    }

    public void MoveTo(Vector3 targetPosition, float moveTime = 0.2f)
    {
        if (moveCoRoutine != null)
        {
            StopCoroutine(moveCoRoutine);
        }

        moveCoRoutine = StartCoroutine(CoMoveToTarget(targetPosition, moveTime));
    }

    public void PlayTween(string key)
    {
        tweenAnimator.PlayAnimation(key);
    }

    IEnumerator CoMoveToTarget(Vector3 targetPosition, float moveTime)
    {
        var currentTime = 0f;
        var startPosition = transform.position;

        while (currentTime <= moveTime)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, currentTime / moveTime);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

}
