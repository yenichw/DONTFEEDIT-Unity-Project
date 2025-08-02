using UnityEngine;

public class ActivateAfterTime : MonoBehaviour
{
	[Header("Timer Settings")]
	public float delay = 5f;

	[Header("GameObjects To Activate")]
	public GameObject[] objectsToActivate;

	private void Start()
	{
		Invoke("ActivateObjects", delay);
	}

	private void ActivateObjects()
	{
		GameObject[] array = objectsToActivate;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: true);
			}
		}
	}
}
