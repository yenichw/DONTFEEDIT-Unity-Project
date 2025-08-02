using UnityEngine;

public class PlayerZoomController : MonoBehaviour
{
	public Camera playerCamera;

	public float zoomFOV = 30f;

	public float zoomSpeed = 5f;

	public float zoomDelay = 0.5f;

	public float idleThreshold = 0.01f;

	public float normalFOV = 60f;

	private bool isZooming;

	private bool isStandingStill;

	private Vector3 lastPosition;

	private float zoomTimer;

	private void Start()
	{
		lastPosition = base.transform.position;
		playerCamera.fieldOfView = normalFOV;
	}

	private void Update()
	{
		isStandingStill = (base.transform.position - lastPosition).magnitude <= idleThreshold;
		if (Input.GetMouseButton(1) && isStandingStill)
		{
			zoomTimer += Time.deltaTime;
			if (zoomTimer >= zoomDelay)
			{
				isZooming = true;
			}
		}
		else
		{
			zoomTimer = 0f;
			isZooming = false;
		}
		if (isZooming)
		{
			playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, Time.deltaTime * zoomSpeed);
		}
		else
		{
			playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, normalFOV, Time.deltaTime * zoomSpeed);
		}
		lastPosition = base.transform.position;
	}
}
