using UnityEngine;

public class ForcePlayerLookAt : MonoBehaviour
{
	public float lookDuration = 2f;

	public FirstPersonController playerController;

	public Transform playerCamera;

	private float timer;

	private bool isLooking;

	private void OnEnable()
	{
		StartLooking();
	}

	private void Update()
	{
		if (isLooking)
		{
			timer += Time.deltaTime;
			Quaternion b = Quaternion.LookRotation((base.transform.position - playerCamera.position).normalized);
			playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, b, Time.deltaTime * 5f);
			if (timer >= lookDuration)
			{
				StopLooking();
			}
		}
	}

	private void StartLooking()
	{
		if (playerController != null)
		{
			playerController.enabled = false;
		}
		isLooking = true;
		timer = 0f;
	}

	private void StopLooking()
	{
		if (playerController != null)
		{
			playerController.enabled = true;
		}
		isLooking = false;
	}
}
