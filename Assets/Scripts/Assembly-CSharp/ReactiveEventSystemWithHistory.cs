using UnityEngine;

public class ReactiveEventSystemWithHistory : MonoBehaviour
{
	[Header("References")]
	public Transform player;

	public GameObject[] scareObjects;

	public AudioClip[] creepySounds;

	public AudioSource audioSource;

	[Header("Behavior Triggers")]
	public float idleThreshold = 6f;

	public float distanceTrigger = 3f;

	[Header("Behavior History Triggers")]
	public int feedThreshold = 5;

	public int refuseThreshold = 5;

	public int nightFeedThreshold = 3;

	private float idleTimer;

	private Vector3 lastPosition;

	private bool eventTriggered;

	private void Start()
	{
		lastPosition = player.position;
		EvaluateLongTermBehavior();
	}

	private void Update()
	{
		if (!eventTriggered)
		{
			TrackIdleTime();
			CheckProximity();
			if (DayTracker.Instance.fedToday && Random.value < 0.1f)
			{
				TriggerCreepyEvent("Fed today reaction");
			}
			if (!DayTracker.Instance.fedToday && Random.value < 0.2f)
			{
				TriggerCreepyEvent("Refused today reaction");
			}
		}
	}

	private void TrackIdleTime()
	{
		if (Vector3.Distance(lastPosition, player.position) < 0.01f)
		{
			idleTimer += Time.deltaTime;
			if (idleTimer > idleThreshold)
			{
				TriggerCreepyEvent("Idle too long");
			}
		}
		else
		{
			idleTimer = 0f;
			lastPosition = player.position;
		}
	}

	private void CheckProximity()
	{
		GameObject[] array = scareObjects;
		foreach (GameObject gameObject in array)
		{
			if (Vector3.Distance(player.position, gameObject.transform.position) < distanceTrigger && Random.value < 0.3f)
			{
				TriggerCreepyEvent("Close to object");
			}
		}
	}

	private void EvaluateLongTermBehavior()
	{
		if (DayTracker.Instance.totalFeeds >= feedThreshold)
		{
			TriggerCreepyEvent("You've been feeding it too much...");
		}
		if (DayTracker.Instance.totalRefusals >= refuseThreshold)
		{
			TriggerCreepyEvent("The hole remembers your defiance...");
		}
		if (DayTracker.Instance.nightFeeds >= nightFeedThreshold)
		{
			TriggerCreepyEvent("Night feeds have changed something...");
		}
	}

	private void TriggerCreepyEvent(string reason)
	{
		Debug.Log("CREEPY EVENT TRIGGERED: " + reason);
		eventTriggered = true;
		if (scareObjects.Length != 0)
		{
			int num = Random.Range(0, scareObjects.Length);
			scareObjects[num].SetActive(value: true);
		}
		if ((bool)audioSource && creepySounds.Length != 0)
		{
			audioSource.PlayOneShot(creepySounds[Random.Range(0, creepySounds.Length)]);
		}
	}
}
