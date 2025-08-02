using UnityEngine;

public class InitialForce : MonoBehaviour
{
	[Header("Force Settings")]
	public Vector3 force = new Vector3(0f, 5f, 0f);

	public ForceMode forceMode = ForceMode.Impulse;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.AddForce(force, forceMode);
	}
}
