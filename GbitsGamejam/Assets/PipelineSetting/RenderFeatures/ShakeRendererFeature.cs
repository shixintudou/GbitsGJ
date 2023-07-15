using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShakeRendererFeature : ScriptableRendererFeature
{
    public ShakeRenderPass renderPass;

    [System.Serializable]
    public class ShakeSettings
    {
        public ComputeShader computeShader;
        [Range(0, 1)]
        public float shakeIntensity = 0.06f;
    }

    public ShakeSettings settings = new ShakeSettings();

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(renderPass);
    }
    public override void Create()
    {
        renderPass = new ShakeRenderPass(settings);
    }
}


