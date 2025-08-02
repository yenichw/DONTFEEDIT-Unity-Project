using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseAndTrigger : MonoBehaviour
{
	[Header("References")]
	public Transform player;

	public GameObject triggerObject;

	public MonoBehaviour playerControllerScript;

	[Header("Chase Settings")]
	public float chaseSpeed = 4f;

	public float triggerDistance = 3f;

	private NavMeshAgent agent;

	private bool isChasing;

	private bool playerFrozen;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player")?.transform;
		}
		if (triggerObject != null)
		{
			triggerObject.SetActive(value: false);
		}
		if (agent != null)
		{
			agent.speed = chaseSpeed;
		}
	}

	private void Update()
	{
		if (!(player == null) && !(agent == null))
		{
			agent.SetDestination(player.position);
			isChasing = true;
			if (!playerFrozen && isChasing)
			{
				FreezePlayer();
			}
			if (Vector3.Distance(base.transform.position, player.position) <= triggerDistance && triggerObject != null)
			{
				triggerObject.SetActive(value: true);
			}
		}
	}

	private void FreezePlayer()
	{
		if (playerControllerScript != null)
		{
			playerControllerScript.enabled = false;
			playerFrozen = true;
		}
	}

	public void UnfreezePlayer()
	{
		if (playerControllerScript != null)
		{
			playerControllerScript.enabled = true;
			playerFrozen = false;
		}
	}
}
