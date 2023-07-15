using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorInvertRendererFeature : ScriptableRendererFeature
{
    public ColorInvertRenderPass renderPass;

    [System.Serializable]
    public class ColorInvertSettings
    {
        public ComputeShader computeShader;
    }

    public ColorInvertSettings settings = new ColorInvertSettings();

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(renderPass);
    }
    public override void Create()
    {
        renderPass = new ColorInvertRenderPass(settings);
    }
}


