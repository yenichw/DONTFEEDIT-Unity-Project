using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(RetroPostProcessEffectRenderer), PostProcessEvent.AfterStack, "Retro Effects", true)]
public sealed class RetroPostProcessEffect : PostProcessEffectSettings
{
	[Tooltip("Whether the pixelation effect uses a fixed vertical resolution or a pixel size multiplier value.")]
	public BoolParameter UsesFixedResolution = new BoolParameter
	{
		value = false
	};

	[UnityEngine.Min(1f)]
	[Tooltip("The value used to scale the width and height of each pixel")]
	public IntParameter PixelScale = new IntParameter
	{
		value = 4
	};

	[UnityEngine.Min(1f)]
	[Tooltip("The vertical resolution of the image output by the pixelation effect.")]
	public IntParameter FixedVerticalResolution = new IntParameter
	{
		value = 480
	};

	[Range(0f, 1f)]
	[Tooltip("Determines the range of the color palette applied to the image.")]
	public FloatParameter ColorDepth = new FloatParameter
	{
		value = 0.15f
	};

	[DisplayName("Dither Pattern")]
	[Tooltip("The pattern used to implement ordered dithering.")]
	public TextureParameter DitherPattern = new TextureParameter
	{
		value = null,
		defaultState = TextureParameterDefault.None
	};

	[Tooltip("The scale multiplier for the dither pattern (the dither pattern size is also automatically scaled according to the pixelation effect scaling).")]
	public IntParameter DitherPatternScale = new IntParameter
	{
		value = 1
	};

	[Range(0f, 1f)]
	[Tooltip("The threshold used to control the range of colors that are affected by dithering.")]
	public FloatParameter DitherThreshold = new FloatParameter
	{
		value = 0.75f
	};

	[Range(0f, 1f)]
	[Tooltip("The intensity of the dithering effect.")]
	public FloatParameter DitherIntensity = new FloatParameter
	{
		value = 0.15f
	};
}
