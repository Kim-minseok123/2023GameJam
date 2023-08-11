using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class Light2DAnimation : MonoBehaviour
{
    private Light2D light2D;

    [SerializeField]
    private float lifeTime = 1f;
    private float currentLifeTime = 0f;

    [SerializeField]
    private float curveTime = 1f;
    private float currentCurveTime = 0f;

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private bool isLooped = false;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        currentCurveTime = 0f;
        currentLifeTime = 0f;
    }

    private void Update()
    {
        currentCurveTime += Time.deltaTime;
        currentLifeTime += Time.deltaTime;

        var intensity = curve.Evaluate(currentCurveTime / curveTime);
        light2D.intensity = intensity;

        if (currentLifeTime / lifeTime > 1f)
        {
            if (isLooped)
            {
                currentCurveTime = 0f;
                currentLifeTime = 0f;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

}
