using UnityEngine;

public class SwapGameObjectsAfterDelay : MonoBehaviour
{
	[Header("Timing")]
	public float delay = 5f;

	[Header("Deactivate These")]
	public GameObject[] objectsToDisable;

	[Header("Activate These")]
	public GameObject[] objectsToEnable;

	private bool hasSwapped;

	private float timer;

	private void Update()
	{
		if (!hasSwapped)
		{
			timer += Time.deltaTime;
			if (timer >= delay)
			{
				SwapObjects();
				hasSwapped = true;
			}
		}
	}

	private void SwapObjects()
	{
		GameObject[] array = objectsToDisable;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: false);
			}
		}
		array = objectsToEnable;
		foreach (GameObject gameObject2 in array)
		{
			if (gameObject2 != null)
			{
				gameObject2.SetActive(value: true);
			}
		}
	}
}
