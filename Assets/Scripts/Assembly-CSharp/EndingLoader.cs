using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingLoader : MonoBehaviour
{
	public float delayBeforeEnding = 5f;

	[Header("Scene Names")]
	public string fleshMonsterScene = "Ending_FleshMonster";

	public string becomeHoleScene = "Ending_Hole";

	public string ascensionScene = "Ending_Ascension";

	public string spiderScene = "Ending_Spider";

	public string fallbackScene = "Ending_Unresolved";

	private void Start()
	{
		StartCoroutine(DecideEnding());
	}

	private IEnumerator DecideEnding()
	{
		yield return new WaitForSeconds(delayBeforeEnding);
		DayTracker instance = DayTracker.Instance;
		if (instance.totalRefusals > instance.totalFeeds)
		{
			SceneManager.LoadScene(fleshMonsterScene);
		}
		else if (instance.dayFeeds >= 7 && instance.totalRefusals < 5)
		{
			SceneManager.LoadScene(becomeHoleScene);
		}
		else if (instance.nightFeeds > instance.dayFeeds)
		{
			SceneManager.LoadScene(ascensionScene);
		}
		else if (instance.totalFeeds == 5 && instance.totalRefusals == 5)
		{
			SceneManager.LoadScene(spiderScene);
		}
		else
		{
			SceneManager.LoadScene(fallbackScene);
		}
	}
}
