using System.Collections;
using UnityEngine;

public class DeactiveObject : MonoBehaviour
{
	public GameObject objectToActivate;

	public float delayTime = 3f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StartCoroutine(ActivateObjectAfterDelay());
		}
	}

	private IEnumerator ActivateObjectAfterDelay()
	{
		yield return new WaitForSeconds(delayTime);
		if (objectToActivate != null)
		{
			objectToActivate.SetActive(value: false);
			Debug.Log("Activated: " + objectToActivate.name);
		}
	}
}
