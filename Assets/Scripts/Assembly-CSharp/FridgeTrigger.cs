using System.Collections;
using UnityEngine;

public class FridgeTrigger : MonoBehaviour
{
	public Animator fridgeAnimator;

	public AudioSource fridgeAudioSource;

	public AudioClip fridgeOpenSound;

	public float animationDelay = 1f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StartCoroutine(PlayFridgeAnimationWithDelay());
		}
	}

	private IEnumerator PlayFridgeAnimationWithDelay()
	{
		yield return new WaitForSeconds(animationDelay);
		if ((bool)fridgeAnimator)
		{
			fridgeAnimator.Play("OpenFridge");
		}
	}

	public void PlayFridgeSound()
	{
		if ((bool)fridgeAudioSource && (bool)fridgeOpenSound)
		{
			fridgeAudioSource.PlayOneShot(fridgeOpenSound);
		}
	}
}
