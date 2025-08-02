using UnityEngine;

public class WeaponSway : MonoBehaviour
{
	[Header("Sway Settings")]
	public float swayAmount = 0.02f;

	public float smoothAmount = 6f;

	public float maxSwayAmount = 0.06f;

	private Vector3 initialPosition;

	private void Start()
	{
		initialPosition = base.transform.localPosition;
	}

	private void Update()
	{
		float axis = Input.GetAxis("Mouse X");
		float axis2 = Input.GetAxis("Mouse Y");
		float x = Mathf.Clamp((0f - axis) * swayAmount, 0f - maxSwayAmount, maxSwayAmount);
		float y = Mathf.Clamp((0f - axis2) * swayAmount, 0f - maxSwayAmount, maxSwayAmount);
		Vector3 vector = new Vector3(x, y, 0f);
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, initialPosition + vector, Time.deltaTime * smoothAmount);
	}
}
