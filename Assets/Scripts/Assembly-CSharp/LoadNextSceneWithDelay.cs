using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneWithDelay : MonoBehaviour
{
	public float delayTime = 5f;

	private void Start()
	{
		StartCoroutine(LoadSceneAfterDelay(delayTime));
	}

	private IEnumerator LoadSceneAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		int num = SceneManager.GetActiveScene().buildIndex + 1;
		if (num < SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(num);
		}
		else
		{
			Debug.LogError("No next scene found in the build settings.");
		}
	}
}
