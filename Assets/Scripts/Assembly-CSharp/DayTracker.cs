using UnityEngine;

public class DayTracker : MonoBehaviour
{
	public static DayTracker Instance;

	[Header("Day Tracking")]
	public int currentDay = 1;

	[Header("Feed Tracking")]
	public int totalFeeds;

	public int totalRefusals;

	public int nightFeeds;

	public int dayFeeds;

	[HideInInspector]
	public bool fedToday;

	[Header("Night Feed Detection")]
	public string nightLightTag = "NightLight";

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void RegisterFeed()
	{
		totalFeeds++;
		fedToday = true;
		if (IsNightEnvironment())
		{
			nightFeeds++;
			Debug.Log("[DayTracker] Night feed registered.");
		}
		else
		{
			dayFeeds++;
			Debug.Log("[DayTracker] Day feed registered.");
		}
	}

	public void RegisterRefusal()
	{
		totalRefusals++;
		fedToday = false;
		Debug.Log("[DayTracker] Refusal registered.");
	}

	public void AdvanceDay()
	{
		currentDay++;
		fedToday = false;
		DebugPrintStats();
	}

	public void ResetAll()
	{
		currentDay = 1;
		totalFeeds = 0;
		totalRefusals = 0;
		nightFeeds = 0;
		dayFeeds = 0;
		fedToday = false;
	}

	public void DebugPrintStats()
	{
		Debug.Log($"[DayTracker] Current Day: {currentDay}");
		Debug.Log($"[DayTracker] Total Feeds: {totalFeeds}, Day Feeds: {dayFeeds}, Night Feeds: {nightFeeds}, Refusals: {totalRefusals}");
	}

	private bool IsNightEnvironment()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag(nightLightTag);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].activeInHierarchy)
			{
				return true;
			}
		}
		return false;
	}
}
