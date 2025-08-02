using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenee : MonoBehaviour
{
	public string sceneName;

	private void Start()
	{
		SceneManager.LoadScene(sceneName);
	}
}
