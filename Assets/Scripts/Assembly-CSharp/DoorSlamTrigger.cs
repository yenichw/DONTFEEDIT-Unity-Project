using UnityEngine;

public class DoorSlamTrigger : MonoBehaviour
{
	[Header("Door Settings")]
	public Transform doorToClose;

	public Vector3 closedRotation = new Vector3(0f, 0f, 0f);

	public float slamSpeed = 720f;

	public AudioSource slamSound;

	private bool isTriggered;

	private Quaternion targetRotation;

	private void Start()
	{
		targetRotation = Quaternion.Euler(closedRotation);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isTriggered && other.CompareTag("Player"))
		{
			isTriggered = true;
			if (slamSound != null)
			{
				slamSound.Play();
			}
		}
	}

	private void Update()
	{
		if (isTriggered && doorToClose != null)
		{
			doorToClose.rotation = Quaternion.RotateTowards(doorToClose.rotation, targetRotation, slamSpeed * Time.deltaTime);
		}
	}
}
