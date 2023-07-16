using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LineRenderPass : ScriptableRenderPass
{
    private LineRendererFeature.LineSettings settings;
    private RenderTexture LineRT;
    private ComputeShader computeShader;
    public static int originID;
    public static int targetID;
    public LineRenderPass(LineRendererFeature.LineSettings settings)
    {
        this.settings = settings;
        this.computeShader = settings.computeShader;
        this.renderPassEvent = RenderPassEvent.AfterRendering;
    }

    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {
        ConfigureInput(ScriptableRenderPassInput.Depth | ScriptableRenderPassInput.Normal | ScriptableRenderPassInput.Color);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        var cmd = CommandBufferPool.Get();

        using (new ProfilingScope(cmd, profilingSampler))
        {
            int width = renderingData.cameraData.cameraTargetDescriptor.width;
            int height = renderingData.cameraData.cameraTargetDescriptor.height;

            RenderTextureDescriptor descriptor1 = new RenderTextureDescriptor(width, height, 0);
            descriptor1.colorFormat = RenderTextureFormat.RGB111110Float;
            descriptor1.sRGB = false;
            descriptor1.useMipMap = true;
            descriptor1.enableRandomWrite = true;
            descriptor1.autoGenerateMips = false;
            RenderTextureDescriptor descriptor2 = new RenderTextureDescriptor(width, height, 0);
            descriptor2.colorFormat = RenderTextureFormat.ARGBFloat;
            descriptor2.sRGB = false;
            descriptor2.useMipMap = true;
            descriptor2.enableRandomWrite = true;
            descriptor2.autoGenerateMips = false;

            cmd.GetTemporaryRT(originID, descriptor1);
            cmd.GetTemporaryRT(targetID, descriptor2);

            cmd.SetComputeFloatParam(settings.computeShader, "_LineIntensity", settings.LineIntensity);
            cmd.SetComputeFloatParam(settings.computeShader, "_VignetteIntensity", settings.VignetteIntensity);
            cmd.SetComputeFloatParam(settings.computeShader, "_VignetteSmoothness", settings.VignetteSmoothness);
            cmd.SetComputeFloatParam(settings.computeShader, "_VignetteRoundness", settings.VignetteRoundness);
            cmd.SetComputeIntParam(settings.computeShader, "_UseGray", settings.useGray ? 1 : 0);
            cmd.SetComputeVectorParam(settings.computeShader, "_BufferSize", new float4(width, height, 1.0f / width, 1.0f / height));
            cmd.SetComputeTextureParam(settings.computeShader, 0, "_MainTex", originID);
            cmd.SetComputeTextureParam(settings.computeShader, 0, "Result", targetID);

            cmd.DispatchCompute(settings.computeShader, 0, width / 8, height / 8, 1);

            cmd.Blit(targetID, renderingData.cameraData.renderer.cameraColorTarget);

            cmd.ReleaseTemporaryRT(originID);
            cmd.ReleaseTemporaryRT(targetID);

        }
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}