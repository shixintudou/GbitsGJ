using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LineRendererFeature : ScriptableRendererFeature
{
    public LineRenderPass renderPass;

    [System.Serializable]
    public class LineSettings
    {
        public ComputeShader computeShader;
        public bool useGray = true;
        [Range(0, 1)]
        public float LineIntensity = 0.15f;
        [Header("Vignette")]
        [Range(0, 3)]
        public float VignetteIntensity = 1.0f;
        [Range(0, 1)]
        public float VignetteSmoothness = 1.0f;
        [Range(0, 1)]
        public float VignetteRoundness = 1.0f;
    }

    public LineSettings settings = new LineSettings();

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(renderPass);
    }
    public override void Create()
    {
        renderPass = new LineRenderPass(settings);
    }
}


