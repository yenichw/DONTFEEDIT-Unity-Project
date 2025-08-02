using UnityEngine;

public class HoleInteraction : MonoBehaviour
{
	[Header("References")]
	public GameObject interactionUI;

	public GameObject interactPrompt;

	public GameObject player;

	public GameObject fleshObject;

	public GameObject crosshair;

	public MonoBehaviour cameraController;

	public MonoBehaviour movementController;

	public float interactionDistance = 2f;

	private bool playerInRange;

	private bool uiActive;

	private void Update()
	{
		if (Vector3.Distance(player.transform.position, base.transform.position) <= interactionDistance && fleshObject.activeInHierarchy)
		{
			playerInRange = true;
			interactPrompt.SetActive(!uiActive);
			if (Input.GetKeyDown(KeyCode.E) && !uiActive)
			{
				OpenUI();
			}
		}
		else
		{
			playerInRange = false;
			interactPrompt.SetActive(value: false);
			if (interactionUI.activeSelf)
			{
				CloseUI();
			}
		}
	}

	public void OpenUI()
	{
		interactionUI.SetActive(value: true);
		uiActive = true;
		Time.timeScale = 0f;
		if (cameraController != null)
		{
			cameraController.enabled = false;
		}
		if (movementController != null)
		{
			movementController.enabled = false;
		}
		if (crosshair != null)
		{
			crosshair.SetActive(value: false);
		}
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		interactPrompt.SetActive(value: false);
	}

	public void CloseUI()
	{
		interactionUI.SetActive(value: false);
		uiActive = false;
		Time.timeScale = 1f;
		if (cameraController != null)
		{
			cameraController.enabled = true;
		}
		if (movementController != null)
		{
			movementController.enabled = true;
		}
		if (crosshair != null)
		{
			crosshair.SetActive(value: true);
		}
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void OnChooseYes()
	{
		FeedManager.Instance.PlayerChoseToFeed();
		DayTracker.Instance.RegisterFeed();
		CloseUI();
	}

	public void OnChooseNo()
	{
		FeedManager.Instance.PlayerChoseToRefuse();
		DayTracker.Instance.RegisterRefusal();
		if (fleshObject != null)
		{
			fleshObject.SetActive(value: false);
		}
		CloseUI();
	}
}
