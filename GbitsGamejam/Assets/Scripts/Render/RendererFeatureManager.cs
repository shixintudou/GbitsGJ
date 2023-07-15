using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RendererFeatureManager : MonoBehaviour
{
    public ForwardRendererData rendererData;

    void Start()
    {
        // ShakeForSeconds(1.0f);
        ColorInvertForSeveralTimes(10);
    }

    // 可调用函数
    // ShakeForSeconds : 屏幕晃动数秒
    // ColorInvertForSeveralTimes : 屏幕颜色反转多次
    // SetOldTVActive : 设置老电视效果是否激活
    // TransitionForSeconds : 屏幕扰动+RGB转场数秒

    public void ShakeForSeconds(float seconds)
    {
        rendererData.rendererFeatures[0].SetActive(true);
        StartCoroutine(ShakeForSecondsCoroutine(seconds));
    }

    IEnumerator ShakeForSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        rendererData.rendererFeatures[0].SetActive(false);
    }

    public void ColorInvertForSeveralTimes(int times)
    {
        rendererData.rendererFeatures[1].SetActive(true);
        StartCoroutine(ColorInvertForSeveralTimesCoroutine(times));
    }

    IEnumerator ColorInvertForSeveralTimesCoroutine(int times)
    {
        for (int i = 0; i < times; i++)
        {
            yield return new WaitForSeconds(0.05f);
            rendererData.rendererFeatures[1].SetActive(true);
            yield return new WaitForSeconds(0.05f);
            rendererData.rendererFeatures[1].SetActive(false);
        }
    }

    public void SetOldTVActive(bool value)
    {
        rendererData.rendererFeatures[2].SetActive(value);
    }

    public void TransitionForSeconds(float seconds)
    {
        rendererData.rendererFeatures[3].SetActive(true);
        StartCoroutine(TransitionForSecondsCoroutine(seconds));
    }

    IEnumerator TransitionForSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        rendererData.rendererFeatures[4].SetActive(false);
    }
}