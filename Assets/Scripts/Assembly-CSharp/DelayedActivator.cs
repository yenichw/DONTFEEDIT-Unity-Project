using System.Collections;
using UnityEngine;

public class DelayedActivator : MonoBehaviour
{
	[SerializeField]
	private GameObject targetGameObject;

	[SerializeField]
	private float activationDelay = 2f;

	private void Start()
	{
		if (targetGameObject == null)
		{
			Debug.LogError("Target GameObject is not assigned.");
		}
		else
		{
			StartCoroutine(ActivateAfterDelay());
		}
	}

	private IEnumerator ActivateAfterDelay()
	{
		yield return new WaitForSeconds(activationDelay);
		targetGameObject.SetActive(value: true);
		Debug.Log($"{targetGameObject.name} has been activated after {activationDelay} seconds.");
	}
}
