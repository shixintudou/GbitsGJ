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
        public enum LineMode
        {
            Vertical,
            Horizontal,
        }
        public LineMode lineMode = LineMode.Vertical;
        public bool useGray = true;
        [Header("Vertical Line")]
        [Range(0, 1)]
        public float LineIntensity = 0.15f;
        [Header("Horizontal Line")]
        [Range(1, 10)]
        public int frequncy = 8;

        [Range(16, 256)]
        public int noiseTextureWidth = 32;
        [Range(16, 256)]
        public int noiseTextureHeight = 256;
        [Range(0, 1)]
        public float stripeLength = 0.85f;

        public float HorizontalIndensity = 0.15f;
        [Range(0, 1)]
        public float StripColorAdjustIndensity = 0.15f;
        [Range(0, 1)]
        public float StripColorAdjustColor = 0.15f;
        public Texture2D NoiseTexture;

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


