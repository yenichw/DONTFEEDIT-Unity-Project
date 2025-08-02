using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFade : MonoBehaviour
{
	private AudioSource audioSource;

	[Header("Settings")]
	public float fadeDuration = 2f;

	public bool playOnStart;

	public bool fadeInOnStart;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(FadeIn());
		if (playOnStart)
		{
			audioSource.volume = 0f;
			audioSource.Play();
			if (fadeInOnStart)
			{
				StartCoroutine(FadeIn());
			}
		}
	}

	public IEnumerator FadeIn(float targetVolume = 0.15f)
	{
		float startVolume = audioSource.volume;
		for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
		{
			audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / fadeDuration);
			yield return null;
		}
		audioSource.volume = targetVolume;
	}

	public IEnumerator FadeOut(float targetVolume = 0f)
	{
		float startVolume = audioSource.volume;
		for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
		{
			audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / fadeDuration);
			yield return null;
		}
		audioSource.volume = targetVolume;
		if (targetVolume <= 0f)
		{
			audioSource.Stop();
		}
	}
}
