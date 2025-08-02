using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingBlender : MonoBehaviour
{
	public PostProcessVolume dayVolume;

	public PostProcessVolume nightVolume;

	public DayNightCycle timeCycle;

	public float blendSpeed = 0.5f;

	private void Update()
	{
		if (timeCycle.currentTime == DayNightCycle.TimeOfDay.Day)
		{
			dayVolume.weight = Mathf.Lerp(dayVolume.weight, 1f, Time.deltaTime * blendSpeed);
			nightVolume.weight = Mathf.Lerp(nightVolume.weight, 0f, Time.deltaTime * blendSpeed);
		}
		else
		{
			dayVolume.weight = Mathf.Lerp(dayVolume.weight, 0f, Time.deltaTime * blendSpeed);
			nightVolume.weight = Mathf.Lerp(nightVolume.weight, 1f, Time.deltaTime * blendSpeed);
		}
	}
}
