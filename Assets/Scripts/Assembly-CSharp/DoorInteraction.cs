using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
	[Header("*** Door Settings ***")]
	public float rotationAngle = 90f;

	public float rotationSpeed = 3.5f;

	public float interactionRange = 4f;

	public KeyCode interactionKey = KeyCode.E;

	[Header("*** Audio Settings ***")]
	public AudioClip openSound;

	public AudioClip closeSound;

	[Header("*** Rotation Axis ***")]
	public bool rotateOnX;

	public bool rotateOnY = true;

	public bool rotateOnZ;

	private Transform door;

	private AudioSource audioSource;

	private Quaternion closedRotation;

	private Quaternion openRotation;

	private bool isOpen;

	private bool isInteracting;

	private Transform player;

	private Camera mainCamera;

	[Header("*** Interaction Prompt Settings ***")]
	public GameObject interactionTextObject;

	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		GameObject gameObject2 = GameObject.FindGameObjectWithTag("MainCamera");
		door = base.transform;
		if (gameObject == null || gameObject2 == null)
		{
			Debug.LogError("Player or MainCamera not found! Check your tags.");
			return;
		}
		player = gameObject.transform;
		mainCamera = gameObject2.GetComponent<Camera>();
		closedRotation = door.rotation;
		openRotation = CalculateOpenRotation();
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = base.gameObject.AddComponent<AudioSource>();
		}
		if (interactionTextObject != null)
		{
			interactionTextObject.SetActive(value: false);
		}
		else
		{
			Debug.LogWarning("InteractionTextObject is not assigned in the inspector!");
		}
	}

	private void Update()
	{
		if (mainCamera == null || player == null || door == null)
		{
			Debug.LogError("References are missing! Ensure all required objects are assigned.");
			return;
		}
		Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
		Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * interactionRange, Color.red, 1f);
		bool flag = false;
		if (Physics.Raycast(ray, out var hitInfo, interactionRange) && hitInfo.transform == door)
		{
			flag = true;
			if (interactionTextObject != null && !interactionTextObject.activeSelf)
			{
				interactionTextObject.SetActive(value: true);
			}
			if (Input.GetKeyDown(interactionKey) && !isInteracting)
			{
				isOpen = !isOpen;
				PlayDoorSound(isOpen);
				StartCoroutine(RotateDoor(isOpen));
			}
		}
		if (!flag && interactionTextObject != null && interactionTextObject.activeSelf)
		{
			interactionTextObject.SetActive(value: false);
		}
	}

	private Quaternion CalculateOpenRotation()
	{
		Vector3 axis = Vector3.zero;
		if (rotateOnX)
		{
			axis = Vector3.right;
		}
		if (rotateOnY)
		{
			axis = Vector3.up;
		}
		if (rotateOnZ)
		{
			axis = Vector3.forward;
		}
		return closedRotation * Quaternion.AngleAxis(rotationAngle, axis);
	}

	private IEnumerator RotateDoor(bool open)
	{
		isInteracting = true;
		if (open)
		{
			openRotation = CalculateOpenRotation();
		}
		Quaternion targetRotation = (open ? openRotation : closedRotation);
		while (Quaternion.Angle(door.rotation, targetRotation) > 0.01f)
		{
			door.rotation = Quaternion.Lerp(door.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			yield return null;
		}
		door.rotation = targetRotation;
		isInteracting = false;
	}

	private void PlayDoorSound(bool opening)
	{
		if (audioSource != null)
		{
			audioSource.clip = (opening ? openSound : closeSound);
			audioSource.Play();
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (mainCamera != null)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(mainCamera.transform.position, mainCamera.transform.position + mainCamera.transform.forward * interactionRange);
		}
	}
}
