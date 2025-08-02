using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostExposureTrigger : MonoBehaviour
{
	[Header("Post-Processing Volumes")]
	public PostProcessVolume dayVolume;

	public PostProcessVolume nightVolume;

	[Header("Vignette Settings")]
	public float vignetteIntensityOffset = 0.5f;

	public float vignetteSmoothnessOffset = 0.5f;

	public float transitionDuration = 1f;

	private Vignette currentVignette;

	private float originalVignetteIntensity;

	private float originalVignetteSmoothness;

	private float targetVignetteIntensity;

	private float targetVignetteSmoothness;

	private bool playerInside;

	private Coroutine transitionCoroutine;

	private void Start()
	{
		UpdateActiveVolume();
	}

	private void Update()
	{
		UpdateActiveVolume();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerInside = true;
			StartTransition();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerInside = false;
			StartTransition();
		}
	}

	private void UpdateActiveVolume()
	{
		PostProcessVolume postProcessVolume = null;
		if (dayVolume != null && dayVolume.enabled)
		{
			postProcessVolume = dayVolume;
			Debug.Log("Day volume is active.");
		}
		else if (nightVolume != null && nightVolume.enabled)
		{
			postProcessVolume = nightVolume;
			Debug.Log("Night volume is active.");
		}
		else
		{
			Debug.Log("No volume is active.");
		}
		if (postProcessVolume != null && postProcessVolume.profile != null)
		{
			if (postProcessVolume.profile.TryGetSettings<Vignette>(out var outSetting))
			{
				Debug.Log("Vignette settings found in active volume.");
				if (currentVignette != outSetting)
				{
					currentVignette = outSetting;
					originalVignetteIntensity = currentVignette.intensity.value;
					originalVignetteSmoothness = currentVignette.smoothness.value;
					StartTransition();
				}
			}
			else
			{
				Debug.Log("No vignette settings found in active volume.");
			}
		}
		else
		{
			Debug.Log("No active volume found.");
		}
	}

	private void StartTransition()
	{
		if (transitionCoroutine != null)
		{
			StopCoroutine(transitionCoroutine);
		}
		targetVignetteIntensity = (playerInside ? (originalVignetteIntensity + vignetteIntensityOffset) : originalVignetteIntensity);
		targetVignetteSmoothness = (playerInside ? (originalVignetteSmoothness + vignetteSmoothnessOffset) : originalVignetteSmoothness);
		transitionCoroutine = StartCoroutine(TransitionVignette());
	}

	private IEnumerator TransitionVignette()
	{
		Debug.Log("Starting vignette transition...");
		float elapsedTime = 0f;
		float startIntensity = currentVignette.intensity.value;
		float startSmoothness = currentVignette.smoothness.value;
		while (elapsedTime < transitionDuration)
		{
			currentVignette.intensity.value = Mathf.Lerp(startIntensity, targetVignetteIntensity, elapsedTime / transitionDuration);
			currentVignette.smoothness.value = Mathf.Lerp(startSmoothness, targetVignetteSmoothness, elapsedTime / transitionDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		currentVignette.intensity.value = targetVignetteIntensity;
		currentVignette.smoothness.value = targetVignetteSmoothness;
		Debug.Log($"Vignette Transition Complete. Intensity: {currentVignette.intensity.value}, Smoothness: {currentVignette.smoothness.value}");
	}
}
