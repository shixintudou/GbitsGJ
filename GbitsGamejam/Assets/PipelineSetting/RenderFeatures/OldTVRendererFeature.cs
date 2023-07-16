using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OldTVRendererFeature : ScriptableRendererFeature
{
    public OldTVRenderPass renderPass;

    [System.Serializable]
    public class OldTVSettings
    {
        public ComputeShader computeShader;
        public enum OldTVMode
        {
            Type1,
            Type2,
        }
        public OldTVMode oldTVMode = OldTVMode.Type1;
        [Header("Vignette")]
        [Range(0, 3)]
        public float OldTVIntensity = 0.06f;
        // smoothness, roundness
        [Range(0, 1)]
        public float smoothness = 0.5f;
        [Range(0, 1)]
        public float roundness = 0.5f;
        [Header("Noise")]
        [Range(0, 1)]
        public float noiseSpeed = 0.8f;
        [Range(0, 1)]
        public float noiseFading = 0.5f;

    }
    public OldTVSettings settings = new OldTVSettings();

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(renderPass);
    }
    public override void Create()
    {
        renderPass = new OldTVRenderPass(settings);
    }
}


