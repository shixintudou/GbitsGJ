using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TransitionRendererFeature : ScriptableRendererFeature
{
    public TransitionRenderPass renderPass;

    [System.Serializable]
    public class TransitionSettings
    {
        public ComputeShader computeShader;

        [Header("Jitter")]
        [Range(0, 25)]
        public float frequency = 0.5f;
        [Range(0, 1)]
        public float jitterIndensity = 0.06f;
        [Header("RGB Block")]
        [Range(0, 1)]
        public float rgbScale = 0.9f;
        [Range(0, 1)]
        public float blockSpeed = 0.5f;
        [Range(0, 1)]
        public float blockAmplitude = 0.1f;
        [Header("Jump")]
        [Range(0, 10)]
        public float jumpProgress = 0.0f;

        public Texture2D noiseTex;

    }

    public TransitionSettings settings = new TransitionSettings();

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(renderPass);
    }
    public override void Create()
    {
        renderPass = new TransitionRenderPass(settings);
    }
}


