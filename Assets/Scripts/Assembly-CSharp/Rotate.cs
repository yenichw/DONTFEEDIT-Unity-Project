using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Vector3 RotationSpeed;

	private void Update()
	{
		base.transform.Rotate(RotationSpeed * Time.deltaTime);
	}
}
