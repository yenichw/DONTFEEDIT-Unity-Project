using UnityEngine;

public class AudioCleaner : MonoBehaviour
{
	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("PersistentAudio");
		if (gameObject != null)
		{
			AudioSource component = gameObject.GetComponent<AudioSource>();
			if (component != null)
			{
				component.Stop();
			}
		}
		else
		{
			Debug.LogWarning("PersistentAudio object not found!");
		}
	}
}
