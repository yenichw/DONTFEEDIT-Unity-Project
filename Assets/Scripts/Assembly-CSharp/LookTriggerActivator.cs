using UnityEngine;

public class LookTriggerActivator : MonoBehaviour
{
	[Header("References")]
	public Transform playerCamera;

	public Transform targetDirection;

	public GameObject objectToActivate;

	[Header("Settings")]
	public float activationAngle = 15f;

	public bool onlyTriggerOnce = true;

	private bool hasTriggered;

	private bool playerInsideTrigger;

	private void Update()
	{
		if (playerInsideTrigger && (!hasTriggered || !onlyTriggerOnce))
		{
			Vector3 normalized = (targetDirection.position - playerCamera.position).normalized;
			if (Vector3.Angle(playerCamera.forward, normalized) < activationAngle)
			{
				objectToActivate.SetActive(value: true);
				hasTriggered = true;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerInsideTrigger = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerInsideTrigger = false;
		}
	}
}
