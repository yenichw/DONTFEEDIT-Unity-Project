using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	private Quaternion initialRotation;

	private void Start()
	{
		initialRotation = base.transform.rotation;
	}

	private void LateUpdate()
	{
		base.transform.rotation = initialRotation;
	}
}
