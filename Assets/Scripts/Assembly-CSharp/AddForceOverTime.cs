using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddForceOverTime : MonoBehaviour
{
	[Header("Force Settings")]
	public Vector3 force = new Vector3(0f, 5f, 0f);

	public ForceMode forceMode = ForceMode.Impulse;

	[Header("Timing")]
	public float interval = 1f;

	private float timer;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= interval)
		{
			rb.AddForce(force, forceMode);
			timer = 0f;
		}
	}
}
