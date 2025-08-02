using UnityEngine;

public class FloatingArrow : MonoBehaviour
{
	public float amplitude = 0.5f;

	public float speed = 2f;

	private Vector3 startPosition;

	private void Start()
	{
		startPosition = base.transform.position;
	}

	private void Update()
	{
		float y = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
		base.transform.position = new Vector3(startPosition.x, y, startPosition.z);
	}
}
