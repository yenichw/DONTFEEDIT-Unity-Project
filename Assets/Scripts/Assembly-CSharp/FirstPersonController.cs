using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
	[Header("Movement")]
	public float walkSpeed = 4f;

	public float gravity = -9.81f;

	public bool canMove = true;

	[Header("Mouse Look")]
	public Transform playerCamera;

	public float mouseSensitivity = 2f;

	public float verticalLookLimit = 80f;

	public float smoothTime = 0.1f;

	public bool canLook = true;

	[Header("Headbob")]
	public float bobSpeed = 14f;

	public float bobAmount = 0.05f;

	[Header("Footsteps")]
	public AudioSource footstepSource;

	public AudioClip[] grassFootstepClips;

	public AudioClip[] woodFootstepClips;

	public AudioClip[] stoneFootstepClips;

	public float stepInterval = 0.5f;

	private CharacterController controller;

	private Vector3 velocity;

	private Vector3 originalCamPos;

	private float stepCycle;

	private float nextStep;

	private Vector3 lastPosition;

	private bool isGroundedCustom;

	private float xRotation;

	private Vector2 currentMouseDelta;

	private Vector2 currentMouseDeltaVelocity;

	public LayerMask groundLayer;

	private void Start()
	{
		controller = GetComponent<CharacterController>();
		originalCamPos = playerCamera.localPosition;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		lastPosition = base.transform.position;
	}

	private void Update()
	{
		if (canLook)
		{
			LookAround();
		}
		if (canMove)
		{
			Move();
			HandleHeadbob();
			HandleFootsteps();
		}
	}

	private void LookAround()
	{
		currentMouseDelta = Vector2.SmoothDamp(target: new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")), current: currentMouseDelta, currentVelocity: ref currentMouseDeltaVelocity, smoothTime: smoothTime);
		xRotation -= currentMouseDelta.y * mouseSensitivity;
		xRotation = Mathf.Clamp(xRotation, 0f - verticalLookLimit, verticalLookLimit);
		playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		base.transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
	}

	private void Move()
	{
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		Vector3 vector = base.transform.right * axis + base.transform.forward * axis2;
		controller.Move(vector * walkSpeed * Time.deltaTime);
		if (controller.isGrounded && velocity.y < 0f)
		{
			velocity.y = -2f;
		}
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}

	private void HandleHeadbob()
	{
		if (controller.velocity.magnitude < 0.1f || !controller.isGrounded)
		{
			float y = Mathf.Sin(Time.time * bobSpeed) * bobAmount * 0.05f;
			playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, originalCamPos + new Vector3(0f, y, 0f), Time.deltaTime * bobSpeed);
			return;
		}
		float x = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
		float y2 = Mathf.Sin(Time.time * bobSpeed * 2f) * bobAmount;
		Vector3 b = originalCamPos + new Vector3(x, y2, 0f);
		playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, b, Time.deltaTime * bobSpeed);
	}

	private void HandleFootsteps()
	{
		isGroundedCustom = controller.isGrounded;
		float num = (base.transform.position - lastPosition).magnitude / Time.deltaTime;
		if (!isGroundedCustom || num < 0.7f)
		{
			lastPosition = base.transform.position;
			return;
		}
		stepCycle += Time.deltaTime;
		if (stepCycle > nextStep)
		{
			nextStep = stepCycle + stepInterval;
			PlayFootstep();
		}
		lastPosition = base.transform.position;
	}

	private void PlayFootstep()
	{
		if (footstepSource == null)
		{
			return;
		}
		Collider surfaceUnderPlayer = GetSurfaceUnderPlayer();
		if (!(surfaceUnderPlayer == null))
		{
			if (surfaceUnderPlayer.CompareTag("Grass"))
			{
				PlayFootstepClips(grassFootstepClips);
			}
			else if (surfaceUnderPlayer.CompareTag("Wood"))
			{
				PlayFootstepClips(woodFootstepClips);
			}
			else if (surfaceUnderPlayer.CompareTag("Stone"))
			{
				PlayFootstepClips(stoneFootstepClips);
			}
		}
	}

	private void PlayFootstepClips(AudioClip[] footstepClips)
	{
		if (footstepClips.Length != 0)
		{
			int num = Random.Range(0, footstepClips.Length);
			footstepSource.PlayOneShot(footstepClips[num]);
		}
	}

	private Collider GetSurfaceUnderPlayer()
	{
		float maxDistance = 2f;
		if (Physics.Raycast(base.transform.position + Vector3.up * 0.1f, Vector3.down, out var hitInfo, maxDistance))
		{
			return hitInfo.collider;
		}
		return null;
	}
}
