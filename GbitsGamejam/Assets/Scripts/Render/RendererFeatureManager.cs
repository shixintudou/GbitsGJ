using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RendererFeatureManager : MonoBehaviour
{
    public ForwardRendererData rendererData;
    // Start is called before the first frame update
    void Start()
    {
        ShakeForSeconds(1.0f);
    }

    void ShakeForSeconds(float seconds)
    {
        rendererData.rendererFeatures[0].SetActive(true);
        StartCoroutine(ShakeForSecondsCoroutine(seconds));
    }

    IEnumerator ShakeForSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        rendererData.rendererFeatures[0].SetActive(false);
    }
}