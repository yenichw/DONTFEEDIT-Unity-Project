using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public sealed class RetroPostProcessEffectRenderer : PostProcessEffectRenderer<RetroPostProcessEffect>
{
	public override void Render(PostProcessRenderContext context)
	{
		PropertySheet propertySheet = context.propertySheets.Get(Shader.Find("Retro 3D Shader Pack/Post-Process"));
		if ((bool)base.settings.UsesFixedResolution)
		{
			propertySheet.properties.SetInt("_PixelScale", -1);
			propertySheet.properties.SetInt("_FixedVerticalResolution", base.settings.FixedVerticalResolution);
		}
		else
		{
			propertySheet.properties.SetInt("_PixelScale", base.settings.PixelScale);
			propertySheet.properties.SetInt("_FixedVerticalResolution", -1);
		}
		propertySheet.properties.SetFloat("_SourceRenderWidth", context.width);
		propertySheet.properties.SetFloat("_SourceRenderHeight", context.height);
		propertySheet.properties.SetFloat("_ColorDepth", base.settings.ColorDepth);
		if ((bool)base.settings.DitherPattern.value)
		{
			propertySheet.properties.SetTexture("_DitherPattern", base.settings.DitherPattern);
			propertySheet.properties.SetInt("_DitherPatternScale", base.settings.DitherPatternScale);
			propertySheet.properties.SetFloat("_DitherThreshold", base.settings.DitherThreshold);
			propertySheet.properties.SetFloat("_DitherIntensity", base.settings.DitherIntensity);
		}
		context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
	}
}
