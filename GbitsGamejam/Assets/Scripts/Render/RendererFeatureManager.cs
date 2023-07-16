using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RendererFeatureManager : MonoBehaviour
{
    public ForwardRendererData rendererData;
    public static RendererFeatureManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        // ShakeForSecondsWithIntensity(10.0f, 0.08f);
        //TransitionForSeconds(1);
    }

    // 可调用函数
    // ShakeForSeconds : 屏幕晃动数秒
    // ShakeForSecondsWithIntensity(seconds, intensity) : 屏幕晃动数秒，可以设定晃动强度
    // ColorInvertForSeveralTimes : 屏幕颜色反转多次
    // SetOldTVActive : 设置老电视效果是否激活
    // SetLineActive : 设置线条效果是否激活
    // TransitionForSeconds(secs) : 屏幕扰动+RGB转场数秒
    // TransitionForSeconds(secs, times) : 屏幕扰动+RGB转场secs秒，转场times次（用来改变转场速度）

    ShakeRendererFeature shakeRendererFeature;
    public void ShakeForSeconds(float seconds)
    {
        shakeRendererFeature = (ShakeRendererFeature)rendererData.rendererFeatures[0];
        shakeRendererFeature.SetActive(true);
        shakeRendererFeature.settings.shakeIntensity = 0.04f;
        StartCoroutine(ShakeForSecondsCoroutine(seconds));
    }

    public void ShakeForSecondsWithIntensity(float seconds, float intensity)
    {
        shakeRendererFeature = (ShakeRendererFeature)rendererData.rendererFeatures[0];
        shakeRendererFeature.SetActive(true);
        shakeRendererFeature.settings.shakeIntensity = intensity;
        StartCoroutine(ShakeForSecondsCoroutine(seconds));
    }

    IEnumerator ShakeForSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        shakeRendererFeature.SetActive(false);
        shakeRendererFeature.settings.shakeIntensity = 0.04f;
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

    public void SetLineActive(bool value)
    {
        rendererData.rendererFeatures[4].SetActive(value);
    }

    public void TransitionForSeconds(float seconds)
    {
        rendererData.rendererFeatures[3].SetActive(true);
        StartCoroutine(TransitionForSecondsCoroutine(seconds));
    }

    bool bTransition = false;
    TransitionRendererFeature transitionRendererFeature;
    float curTime = 0;
    float transitionTimes = 0;
    float transitionSecends = 0f;
    public void TransitionForSeconds(float seconds, float times)
    {
        transitionRendererFeature = (TransitionRendererFeature)rendererData.rendererFeatures[3];
        curTime = Time.time;
        transitionTimes = times;
        transitionSecends = seconds;
        rendererData.rendererFeatures[3].SetActive(true);
        bTransition = true;
        StartCoroutine(TransitionForSecondsCoroutine(seconds));
    }
    // public void TransitionForSeconds(float seconds)
    // {
    //     TransitionForSeconds(seconds, seconds);
    // }

    IEnumerator TransitionForSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        rendererData.rendererFeatures[3].SetActive(false);
        bTransition = false;
    }
    void Update()
    {
        if (bTransition)
        {
            transitionRendererFeature.settings.jumpProgress = (Time.time - curTime) * transitionTimes / transitionSecends;
        }
    }


}