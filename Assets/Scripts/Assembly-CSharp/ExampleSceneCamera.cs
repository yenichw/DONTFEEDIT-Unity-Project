using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(Camera))]
public class ExampleSceneCamera : MonoBehaviour
{
	public PostProcessVolume RetroPostProcessVolume;

	public float MouseSensitivity = 100f;

	public float VerticalClampAngle = 80f;

	public float MoveSpeed = 5f;

	private RetroPostProcessEffect postProcessEffect;

	private Vector2 MouseLookRotation;

	private void Awake()
	{
		Vector3 eulerAngles = base.transform.localRotation.eulerAngles;
		MouseLookRotation.x = eulerAngles.x;
		MouseLookRotation.y = eulerAngles.y;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Start()
	{
		RetroPostProcessVolume.profile.TryGetSettings<RetroPostProcessEffect>(out postProcessEffect);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		UpdateMouseLook();
		UpdateMovement();
		UpdatePostProcessEffects();
	}

	private void UpdateMouseLook()
	{
		if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.None)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		Vector2 vector = new Vector2(Input.GetAxis("Mouse X"), 0f - Input.GetAxis("Mouse Y"));
		MouseLookRotation.x += vector.y * MouseSensitivity * Time.deltaTime;
		MouseLookRotation.y += vector.x * MouseSensitivity * Time.deltaTime;
		MouseLookRotation.x = Mathf.Clamp(MouseLookRotation.x, 0f - VerticalClampAngle, VerticalClampAngle);
		Quaternion rotation = Quaternion.Euler(MouseLookRotation.x, MouseLookRotation.y, 0f);
		base.transform.rotation = rotation;
	}

	private void UpdateMovement()
	{
		Vector3 zero = Vector3.zero;
		if (Input.GetKey(KeyCode.W))
		{
			zero += Vector3.forward;
		}
		if (Input.GetKey(KeyCode.S))
		{
			zero += Vector3.back;
		}
		if (Input.GetKey(KeyCode.A))
		{
			zero += Vector3.left;
		}
		if (Input.GetKey(KeyCode.D))
		{
			zero += Vector3.right;
		}
		if (Input.GetKey(KeyCode.E))
		{
			zero += Vector3.up;
		}
		if (Input.GetKey(KeyCode.Q))
		{
			zero += Vector3.down;
		}
		float num = 1f;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			num = 2f;
		}
		if (zero != Vector3.zero)
		{
			base.transform.Translate(zero.normalized * MoveSpeed * num * Time.deltaTime);
		}
	}

	private void UpdatePostProcessEffects()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			postProcessEffect.enabled.value = !postProcessEffect.enabled.value;
		}
	}
}
