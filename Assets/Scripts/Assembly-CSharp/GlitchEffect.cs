using System.Collections;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
	[Header("Glitch Settings")]
	public float glitchDuration = 10f;

	public float initialGlitchFrequency = 0.5f;

	public float glitchIncreaseRate = 0.05f;

	public float maxGlitchFrequency = 0.1f;

	[Header("References")]
	public Camera mainCamera;

	private float glitchFrequency;

	private bool isGlitching = true;

	private float originalFOV;

	private void Start()
	{
		if (mainCamera == null)
		{
			Debug.LogError("Main Camera is not assigned!");
			return;
		}
		originalFOV = mainCamera.fieldOfView;
		glitchFrequency = initialGlitchFrequency;
		StartCoroutine(GlitchCycle());
	}

	private void Update()
	{
		if (isGlitching)
		{
			Debug.Log("Update is running!");
			if (Random.Range(0f, 1f) < Time.deltaTime / glitchFrequency)
			{
				StartGlitch();
			}
			glitchFrequency = Mathf.Max(maxGlitchFrequency, glitchFrequency - glitchIncreaseRate * Time.deltaTime);
		}
	}

	private IEnumerator GlitchCycle()
	{
		yield return new WaitForSeconds(glitchDuration);
		isGlitching = false;
		ResetEffects();
	}

	private void StartGlitch()
	{
		Debug.Log("Glitch triggered!");
		FlickerScreen();
		ApplyDistortion();
	}

	private void FlickerScreen()
	{
		Debug.Log("Flicker effect triggered!");
		Camera.main.gameObject.SetActive(value: false);
		Invoke("RestoreCamera", 0.05f);
	}

	private void RestoreCamera()
	{
		Camera.main.gameObject.SetActive(value: true);
	}

	private void ApplyDistortion()
	{
		float num = Random.Range(30f, 120f);
		mainCamera.fieldOfView = num;
		Debug.Log($"Distortion effect triggered! New FOV: {num}");
	}

	private void ResetEffects()
	{
		mainCamera.fieldOfView = originalFOV;
	}
}
