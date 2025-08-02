using System.Collections;
using UnityEngine;

public class PlayerStunWithCameraShake : MonoBehaviour
{
	[Header("Settings")]
	public float delayBeforeStun = 1f;

	public float stunDuration = 2f;

	public float shakeIntensity = 0.2f;

	public float shakeDuration = 0.5f;

	private FirstPersonController playerController;

	private Camera mainCamera;

	private Vector3 originalCamPos;

	private void Start()
	{
		playerController = Object.FindObjectOfType<FirstPersonController>();
		mainCamera = Camera.main;
		if (mainCamera != null)
		{
			originalCamPos = mainCamera.transform.localPosition;
		}
		StartCoroutine(StunSequence());
	}

	private IEnumerator StunSequence()
	{
		yield return new WaitForSeconds(delayBeforeStun);
		FreezePlayer(freeze: true);
		StartCoroutine(CameraShake());
		yield return new WaitForSeconds(stunDuration);
		FreezePlayer(freeze: false);
	}

	private void FreezePlayer(bool freeze)
	{
		if (playerController != null)
		{
			playerController.canMove = !freeze;
			playerController.canLook = !freeze;
		}
	}

	private IEnumerator CameraShake()
	{
		float elapsed = 0f;
		while (elapsed < shakeDuration)
		{
			Vector3 vector = Random.insideUnitSphere * shakeIntensity;
			mainCamera.transform.localPosition = originalCamPos + vector;
			elapsed += Time.deltaTime;
			yield return null;
		}
		mainCamera.transform.localPosition = originalCamPos;
	}
}
