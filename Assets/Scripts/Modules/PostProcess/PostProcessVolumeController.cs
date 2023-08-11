using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#if URP
[RequireComponent(typeof(Volume))]
public class PostProcessVolumeController : MonoBehaviour
{
    private Volume volume;

    [SerializeField]
    private AnimationCurve weightCurve;

    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private bool isUnscaledTime = false;

    private void Awake()
    {
        volume = GetComponent<Volume>();
    }

    private void Start()
    {
        if (isUnscaledTime)
        {
            StartCoroutine(UpdateWeightByUnscaledTime());
        }
        else
        {
            StartCoroutine(UpdateWeightByScaledTime());
        }
    }

    IEnumerator UpdateWeightByScaledTime()
    {
        float lerpTime = 0f;
        var waitForSec = new WaitForSeconds(Time.fixedDeltaTime);

        while (lerpTime <= lifeTime)
        {
            volume.weight = weightCurve.Evaluate(lerpTime / lifeTime);
            lerpTime += Time.fixedDeltaTime;
            yield return waitForSec;
        }

        Destroy(this.gameObject);
    }

    IEnumerator UpdateWeightByUnscaledTime()
    {
        float lerpTime = 0f;
        var waitForSec = new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);

        while (lerpTime <= lifeTime)
        {
            volume.weight = weightCurve.Evaluate(lerpTime / lifeTime);
            lerpTime += Time.fixedUnscaledDeltaTime;
            yield return waitForSec;
        }

        Destroy(this.gameObject);
    }

}
#endif