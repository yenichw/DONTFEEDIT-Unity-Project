using System.Collections;
using UnityEngine;

public class ActivateAndDestroy : MonoBehaviour
{
	public float delayTime = 5f;

	public GameObject targetObject;

	private void Start()
	{
		StartCoroutine(ActivateThenDestroy());
	}

	private IEnumerator ActivateThenDestroy()
	{
		yield return new WaitForSeconds(delayTime);
		if (targetObject != null)
		{
			targetObject.SetActive(value: true);
		}
		Object.Destroy(base.gameObject);
	}
}
