using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
	public enum TimeOfDay
	{
		Day = 0,
		Night = 1
	}

	public TimeOfDay currentTime;

	[Header("Timer Settings")]
	public float dayDuration = 90f;

	private float timer;

	[Header("Lighting")]
	public Light sunLight;

	public Color dayColor = new Color(1f, 0.95f, 0.8f);

	public Color nightColor = new Color(0.2f, 0.3f, 0.6f);

	public float dayIntensity = 1.2f;

	public float nightIntensity = 0.3f;

	public float transitionSpeed = 0.5f;

	[Header("Ambient Sounds")]
	public AudioSource ambientDay;

	public AudioSource ambientNight;

	[Range(0f, 1f)]
	public float dayVolume = 1f;

	[Range(0f, 1f)]
	public float nightVolume = 1f;

	public float audioFadeSpeed = 0.5f;

	[Header("Skyboxes")]
	public Material daySkybox;

	public Material nightSkybox;

	[Header("Environment Colors")]
	public Color ambientDayColor = new Color(0.65f, 0.65f, 0.7f);

	public Color ambientNightColor = new Color(0.1f, 0.1f, 0.15f);

	public Color fogDayColor = new Color(0.85f, 0.9f, 1f);

	public Color fogNightColor = new Color(0.05f, 0.05f, 0.1f);

	public float fogDayDensity = 0.002f;

	public float fogNightDensity = 0.008f;

	private void Start()
	{
		timer = dayDuration;
		SetToDay();
	}

	private void Update()
	{
		if (currentTime == TimeOfDay.Day)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				SwitchToNight();
			}
		}
		UpdateLighting();
		UpdateAmbientAudio();
		UpdateEnvironmentColors();
	}

	private void SwitchToNight()
	{
		currentTime = TimeOfDay.Night;
		RenderSettings.skybox = nightSkybox;
		DynamicGI.UpdateEnvironment();
		if (!ambientNight.isPlaying)
		{
			ambientNight.Play();
		}
		timer = dayDuration;
	}

	public void SetToDay()
	{
		currentTime = TimeOfDay.Day;
		timer = dayDuration;
		sunLight.color = dayColor;
		sunLight.intensity = dayIntensity;
		RenderSettings.skybox = daySkybox;
		DynamicGI.UpdateEnvironment();
		if (!ambientDay.isPlaying)
		{
			ambientDay.Play();
		}
	}

	private void UpdateLighting()
	{
		Color b = ((currentTime == TimeOfDay.Day) ? dayColor : nightColor);
		float b2 = ((currentTime == TimeOfDay.Day) ? dayIntensity : nightIntensity);
		sunLight.color = Color.Lerp(sunLight.color, b, Time.deltaTime * transitionSpeed);
		sunLight.intensity = Mathf.Lerp(sunLight.intensity, b2, Time.deltaTime * transitionSpeed);
	}

	private void UpdateAmbientAudio()
	{
		float b = ((currentTime == TimeOfDay.Day) ? dayVolume : 0f);
		float b2 = ((currentTime == TimeOfDay.Night) ? nightVolume : 0f);
		ambientDay.volume = Mathf.Lerp(ambientDay.volume, b, Time.deltaTime * audioFadeSpeed);
		ambientNight.volume = Mathf.Lerp(ambientNight.volume, b2, Time.deltaTime * audioFadeSpeed);
	}

	private void UpdateEnvironmentColors()
	{
		Color b = ((currentTime == TimeOfDay.Day) ? ambientDayColor : ambientNightColor);
		Color b2 = ((currentTime == TimeOfDay.Day) ? fogDayColor : fogNightColor);
		float b3 = ((currentTime == TimeOfDay.Day) ? fogDayDensity : fogNightDensity);
		RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, b, Time.deltaTime * transitionSpeed);
		RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, b2, Time.deltaTime * transitionSpeed);
		RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, b3, Time.deltaTime * transitionSpeed);
	}
}
