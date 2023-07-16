using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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

    Texture2D _noiseTexture;
    RenderTexture _trashFrame1;
    RenderTexture _trashFrame2;

    void UpdateNoiseTexture(int frame, int noiseTextureWidth, int noiseTextureHeight, float stripLength)
    {
        int frameCount = Time.frameCount;
        if (frameCount % frame != 0)
        {
            return;
        }

        _noiseTexture = new Texture2D(noiseTextureWidth, noiseTextureHeight, TextureFormat.ARGB32, false);
        _noiseTexture.wrapMode = TextureWrapMode.Clamp;
        _noiseTexture.filterMode = FilterMode.Point;

        _trashFrame1 = new RenderTexture(Screen.width, Screen.height, 0);
        _trashFrame2 = new RenderTexture(Screen.width, Screen.height, 0);
        _trashFrame1.hideFlags = HideFlags.DontSave;
        _trashFrame2.hideFlags = HideFlags.DontSave;

        Color32 color = new Color(Random.value, Random.value, Random.value, Random.value);

        for (int y = 0; y < _noiseTexture.height; y++)
        {
            for (int x = 0; x < _noiseTexture.width; x++)
            {
                //随机值若大于给定strip随机阈值，重新随机颜色
                if (UnityEngine.Random.value > stripLength)
                {
                    color = new Color(Random.value, Random.value, Random.value, Random.value);
                }
                //设置贴图像素值
                _noiseTexture.SetPixel(x, y, color);
            }
        }

        _noiseTexture.Apply();

        var bytes = _noiseTexture.EncodeToPNG();
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

            int kernel;

            if (settings.lineMode == LineRendererFeature.LineSettings.LineMode.Vertical)
            {
                kernel = computeShader.FindKernel("Line");
            }
            else
            {
                kernel = computeShader.FindKernel("LineHorizontal");
            }

            UpdateNoiseTexture(settings.frequncy, settings.noiseTextureWidth, settings.noiseTextureHeight, settings.stripeLength);

            cmd.SetComputeFloatParam(settings.computeShader, "_LineIntensity", settings.LineIntensity);
            cmd.SetComputeFloatParam(settings.computeShader, "_VignetteIntensity", settings.VignetteIntensity);
            cmd.SetComputeFloatParam(settings.computeShader, "_VignetteSmoothness", settings.VignetteSmoothness);
            cmd.SetComputeFloatParam(settings.computeShader, "_VignetteRoundness", settings.VignetteRoundness);

            cmd.SetComputeTextureParam(settings.computeShader, kernel, "_NoiseTex", _noiseTexture);
            cmd.SetComputeFloatParam(settings.computeShader, "_HorizontalIntensity", settings.HorizontalIndensity);
            cmd.SetComputeFloatParam(settings.computeShader, "_StripColorAdjustIndensity", settings.StripColorAdjustIndensity);
            cmd.SetComputeFloatParam(settings.computeShader, "_StripColorAdjustColor", settings.StripColorAdjustColor);

            cmd.SetComputeIntParam(settings.computeShader, "_UseGray", settings.useGray ? 1 : 0);
            cmd.SetComputeVectorParam(settings.computeShader, "_BufferSize", new Vector4(width, height, 1.0f / width, 1.0f / height));
            cmd.SetComputeTextureParam(settings.computeShader, kernel, "_MainTex", originID);
            cmd.SetComputeTextureParam(settings.computeShader, kernel, "Result", targetID);

            cmd.DispatchCompute(settings.computeShader, kernel, width / 8, height / 8, 1);

            cmd.Blit(targetID, renderingData.cameraData.renderer.cameraColorTarget);

            cmd.ReleaseTemporaryRT(originID);
            cmd.ReleaseTemporaryRT(targetID);

        }
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}