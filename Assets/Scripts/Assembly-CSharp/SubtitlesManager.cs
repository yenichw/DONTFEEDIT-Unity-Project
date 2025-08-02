using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SubtitlesManager : MonoBehaviour
{
	[Serializable]
	public struct SubtitleLine
	{
		[TextArea(3, 10)]
		public string text;

		public float duration;
	}

	public TextMeshProUGUI subtitleText;

	public bool autoStart;

	private Coroutine subtitleCoroutine;

	public SubtitleLine[] subtitles;

	private void Start()
	{
		if (autoStart)
		{
			PlaySubtitles();
		}
	}

	public void PlaySubtitles()
	{
		if (subtitleCoroutine != null)
		{
			StopCoroutine(subtitleCoroutine);
		}
		subtitleCoroutine = StartCoroutine(ShowSubtitles());
	}

	public void StopSubtitles()
	{
		if (subtitleCoroutine != null)
		{
			StopCoroutine(subtitleCoroutine);
		}
		subtitleText.text = "";
	}

	private IEnumerator ShowSubtitles()
	{
		for (int i = 0; i < subtitles.Length; i++)
		{
			subtitleText.text = subtitles[i].text;
			yield return new WaitForSeconds(subtitles[i].duration);
		}
		subtitleText.text = "";
	}
}
