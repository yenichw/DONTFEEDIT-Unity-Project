using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneOnKeyPress : MonoBehaviour
{
	public KeyCode keyToPress = KeyCode.E;

	public GameObject objectToActivate;

	public float delayBeforeLoading = 2f;

	private void Update()
	{
		if (Input.GetKeyDown(keyToPress))
		{
			StartCoroutine(ActivateAndLoadScene());
		}
	}

	private IEnumerator ActivateAndLoadScene()
	{
		if (objectToActivate != null)
		{
			objectToActivate.SetActive(value: true);
		}
		yield return new WaitForSeconds(delayBeforeLoading);
		LoadNextScene();
	}

	private void LoadNextScene()
	{
		int num = SceneManager.GetActiveScene().buildIndex + 1;
		if (num < SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(num);
		}
		else
		{
			Debug.LogWarning("No more scenes to load! You are at the last scene.");
		}
	}
}
