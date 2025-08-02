using UnityEngine;

public class EmissiveSphere : MonoBehaviour
{
	public Vector3 TargetPosOffset;

	public float TravelDuration;

	private Vector3 initialPos;

	private float travelTimer;

	private void Awake()
	{
		initialPos = base.transform.position;
	}

	private void Update()
	{
		base.transform.position = Vector3.Lerp(initialPos, initialPos + TargetPosOffset, travelTimer / TravelDuration);
		travelTimer += Time.deltaTime;
		if (travelTimer > TravelDuration)
		{
			travelTimer = 0f;
		}
	}
}
