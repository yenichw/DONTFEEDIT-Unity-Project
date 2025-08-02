using System.Collections;
using UnityEngine;

public class DelayedDoorTrigger : MonoBehaviour
{
	[Header("*** Door Settings ***")]
	public Transform doorToOpen;

	public float rotationAngle = 90f;

	public float rotationSpeed = 3.5f;

	public AudioClip doorOpenSound;

	public float delayBeforeOpening = 1f;

	private AudioSource audioSource;

	private bool doorOpened;

	private void Start()
	{
		audioSource = doorToOpen.GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = doorToOpen.gameObject.AddComponent<AudioSource>();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !doorOpened)
		{
			StartCoroutine(OpenDoorWithDelay());
		}
	}

	private void PlaySound()
	{
		if (audioSource != null && doorOpenSound != null)
		{
			audioSource.clip = doorOpenSound;
			audioSource.Play();
		}
	}

	private IEnumerator OpenDoorWithDelay()
	{
		yield return new WaitForSeconds(delayBeforeOpening);
		Quaternion openRotation = doorToOpen.rotation * Quaternion.AngleAxis(rotationAngle, Vector3.up);
		doorOpened = true;
		PlaySound();
		while (Quaternion.Angle(doorToOpen.rotation, openRotation) > 0.01f)
		{
			doorToOpen.rotation = Quaternion.Lerp(doorToOpen.rotation, openRotation, Time.deltaTime * rotationSpeed);
			yield return null;
		}
		doorToOpen.rotation = openRotation;
		Object.Destroy(base.gameObject);
	}
}
