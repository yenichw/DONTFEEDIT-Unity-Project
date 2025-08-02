using UnityEngine;

public class NoteInteraction : MonoBehaviour
{
	[Header("Interaction")]
	public float interactDistance = 3f;

	public LayerMask noteLayer;

	public GameObject pickupPrompt;

	public GameObject noteUIPanel;

	[Header("References")]
	public FirstPersonController playerController;

	public Transform cameraTransform;

	public AudioSource audioSource;

	public AudioClip pickupSound;

	private bool isNoteOpen;

	private GameObject currentNote;

	private void Update()
	{
		if (isNoteOpen)
		{
			return;
		}
		if (Physics.Raycast(new Ray(cameraTransform.position, cameraTransform.forward), out var hitInfo, interactDistance, noteLayer) && hitInfo.collider.CompareTag("Note"))
		{
			currentNote = hitInfo.collider.gameObject;
			pickupPrompt.SetActive(value: true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				ShowNote();
			}
		}
		else
		{
			pickupPrompt.SetActive(value: false);
			currentNote = null;
		}
	}

	private void ShowNote()
	{
		if (pickupSound != null)
		{
			audioSource.PlayOneShot(pickupSound);
		}
		isNoteOpen = true;
		noteUIPanel.SetActive(value: true);
		pickupPrompt.SetActive(value: false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		playerController.enabled = false;
	}

	public void CloseNote()
	{
		isNoteOpen = false;
		noteUIPanel.SetActive(value: false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		playerController.enabled = true;
	}
}
