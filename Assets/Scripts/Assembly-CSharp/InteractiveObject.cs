using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
	public GameObject[] objectsToActivate;

	public GameObject[] objectsToDeactivate;

	public float interactionDistance = 3f;

	public LayerMask interactableLayer;

	public GameObject interactionTextObject;

	private Camera playerCamera;

	private void Start()
	{
		playerCamera = Camera.main;
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
		bool flag = false;
		if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out var hitInfo, interactionDistance, interactableLayer))
		{
			InteractiveObject component = hitInfo.collider.GetComponent<InteractiveObject>();
			if (component != null)
			{
				flag = true;
				if (interactionTextObject != null && !interactionTextObject.activeSelf)
				{
					interactionTextObject.SetActive(value: true);
				}
				if (Input.GetKeyDown(KeyCode.E))
				{
					component.Interact();
				}
			}
		}
		if (!flag && interactionTextObject != null && interactionTextObject.activeSelf)
		{
			interactionTextObject.SetActive(value: false);
		}
	}

	public void Interact()
	{
		ActivateObjects();
		DeactivateObjects();
	}

	private void ActivateObjects()
	{
		GameObject[] array = objectsToActivate;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: true);
				Debug.Log("Activated: " + gameObject.name);
			}
		}
	}

	private void DeactivateObjects()
	{
		GameObject[] array = objectsToDeactivate;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: false);
				Debug.Log("Deactivated: " + gameObject.name);
			}
		}
	}
}
