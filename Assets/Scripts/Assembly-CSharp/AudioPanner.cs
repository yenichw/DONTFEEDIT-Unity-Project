using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPanner : MonoBehaviour
{
	public float panSpeed = 1f;

	public float maxPanRange = 0.8f;

	public float changeInterval = 3f;

	private AudioSource audioSource;

	private float targetPan;

	private float timer;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		PickNewTargetPan();
	}

	private void Update()
	{
		timer += Time.deltaTime;
		audioSource.panStereo = Mathf.Lerp(audioSource.panStereo, targetPan, Time.deltaTime * panSpeed);
		if (timer >= changeInterval)
		{
			PickNewTargetPan();
			timer = 0f;
		}
	}

	private void PickNewTargetPan()
	{
		targetPan = Random.Range(0f - maxPanRange, maxPanRange);
		changeInterval = Random.Range(1.5f, 4f);
	}
}
