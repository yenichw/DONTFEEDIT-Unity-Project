using UnityEngine;

public class FridgeSoundManager : MonoBehaviour
{
	public AudioSource fridgeAudioSource;

	public AudioClip fridgeOpenSound;

	public void PlayFridgeSound()
	{
		if ((bool)fridgeAudioSource && (bool)fridgeOpenSound)
		{
			fridgeAudioSource.PlayOneShot(fridgeOpenSound);
		}
	}
}
