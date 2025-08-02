using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
	[Header("Light Settings")]
	public Light flickeringLight;

	public float intensityLow = 0.2f;

	public float intensityHigh = 1f;

	[Header("Timing Settings")]
	public float minFlickerInterval = 0.1f;

	public float maxFlickerInterval = 0.5f;

	[Header("Audio Settings")]
	public AudioSource audioSource;

	public AudioClip flickerSound;

	private float nextFlickerTime;

	private float originalIntensity;

	private void Start()
	{
		originalIntensity = flickeringLight.intensity;
		ScheduleNextFlicker();
	}

	private void Update()
	{
		if (Time.time >= nextFlickerTime)
		{
			FlickerLight();
			ScheduleNextFlicker();
		}
	}

	private void FlickerLight()
	{
		float intensity = Random.Range(intensityLow, intensityHigh);
		flickeringLight.intensity = intensity;
		if (audioSource != null && flickerSound != null)
		{
			audioSource.PlayOneShot(flickerSound);
		}
		StartCoroutine(ResetLightIntensity());
	}

	private IEnumerator ResetLightIntensity()
	{
		yield return new WaitForSeconds(0.1f);
		flickeringLight.intensity = originalIntensity;
	}

	private void ScheduleNextFlicker()
	{
		nextFlickerTime = Time.time + Random.Range(minFlickerInterval, maxFlickerInterval);
	}
}
