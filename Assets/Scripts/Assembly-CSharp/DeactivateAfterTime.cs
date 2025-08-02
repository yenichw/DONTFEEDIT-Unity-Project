using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
	[Header("Timer Settings")]
	public float delay = 5f;

	[Header("GameObjects To Deactivate")]
	public GameObject[] objectsToDeactivate;

	private void Start()
	{
		Invoke("DeactivateObjects", delay);
	}

	private void DeactivateObjects()
	{
		GameObject[] array = objectsToDeactivate;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: false);
			}
		}
	}
}
