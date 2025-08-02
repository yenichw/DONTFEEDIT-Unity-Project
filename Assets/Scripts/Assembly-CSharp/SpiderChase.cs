using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class SpiderChase : MonoBehaviour
{
	[Header("Chase Settings")]
	public Transform player;

	public float stopDistance = 2f;

	[Header("Player Control")]
	public FirstPersonController playerController;

	[Header("Trigger Settings")]
	public GameObject objectToActivate;

	public UnityEvent onTrigger;

	[Header("Optional")]
	public Transform visualTarget;

	private NavMeshAgent agent;

	private bool hasTriggered;

	private Vector3 lastPlayerPosition;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		if (player != null)
		{
			lastPlayerPosition = player.position;
		}
	}

	private void Update()
	{
		if (!hasTriggered && !(player == null))
		{
			float num = Vector3.Distance(base.transform.position, player.position);
			if (num > stopDistance && Vector3.Distance(player.position, lastPlayerPosition) > 0.1f)
			{
				agent.SetDestination(player.position);
				lastPlayerPosition = player.position;
			}
			else if (num <= stopDistance)
			{
				TriggerEffect();
			}
			RotateVisuals();
		}
	}

	private void RotateVisuals()
	{
		if (!(visualTarget == null) && !(player == null))
		{
			Vector3 vector = player.position - visualTarget.position;
			vector.y = 0f;
			if (vector != Vector3.zero)
			{
				Quaternion b = Quaternion.LookRotation(vector);
				visualTarget.rotation = Quaternion.Slerp(visualTarget.rotation, b, Time.deltaTime * 5f);
			}
		}
	}

	private void TriggerEffect()
	{
		hasTriggered = true;
		agent.isStopped = true;
		if (playerController != null)
		{
			playerController.enabled = false;
		}
		if (objectToActivate != null)
		{
			objectToActivate.SetActive(value: true);
		}
		onTrigger?.Invoke();
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
