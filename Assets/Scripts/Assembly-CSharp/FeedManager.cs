using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedManager : MonoBehaviour
{
	public static FeedManager Instance;

	[Header("References")]
	public GameObject fleshObject;

	public DayNightCycle timeSystem;

	[Header("Feedback")]
	public AudioSource feedSound;

	public AudioSource refuseSound;

	public GameObject holeVFX;

	public AudioSource sceneTransitionSound;

	[Header("Scene Transition")]
	public float delayBeforeSceneLoad = 3f;

	public float soundDelay = 1f;

	public Image fadeImage;

	private bool actionTaken;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
		if (fadeImage != null)
		{
			fadeImage.gameObject.SetActive(value: false);
		}
	}

	public void PlayerChoseToFeed()
	{
		if (!actionTaken)
		{
			actionTaken = true;
			Debug.Log("Fed the hole.");
			if ((bool)feedSound)
			{
				feedSound.Play();
			}
			if ((bool)holeVFX)
			{
				holeVFX.SetActive(value: true);
			}
			if (fleshObject != null)
			{
				fleshObject.SetActive(value: false);
			}
			DayTracker.Instance.RegisterFeed();
			StartCoroutine(DelayedSceneLoad());
		}
	}

	public void PlayerChoseToRefuse()
	{
		if (!actionTaken)
		{
			actionTaken = true;
			Debug.Log("Refused to feed the hole.");
			if ((bool)refuseSound)
			{
				refuseSound.Play();
			}
			if (fleshObject != null)
			{
				fleshObject.SetActive(value: false);
			}
			DayTracker.Instance.RegisterRefusal();
			StartCoroutine(DelayedSceneLoad());
		}
	}

	private IEnumerator DelayedSceneLoad()
	{
		yield return new WaitForSeconds(soundDelay);
		if (sceneTransitionSound != null)
		{
			sceneTransitionSound.Play();
		}
		FirstPersonController firstPersonController = Object.FindObjectOfType<FirstPersonController>();
		if (firstPersonController != null)
		{
			firstPersonController.enabled = false;
		}
		if (fadeImage != null)
		{
			fadeImage.gameObject.SetActive(value: true);
			StartCoroutine(FadeOut());
		}
		yield return new WaitForSeconds(delayBeforeSceneLoad);
		int num = SceneManager.GetActiveScene().buildIndex + 1;
		if (num < SceneManager.sceneCountInBuildSettings)
		{
			Debug.Log("Loading next scene (index " + num + ")");
			SceneManager.LoadScene(num);
		}
		else
		{
			Debug.LogWarning("No next scene found in Build Settings!");
		}
	}

	private IEnumerator FadeOut()
	{
		float elapsedTime = 0f;
		Color startColor = fadeImage.color;
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);
		while (elapsedTime < delayBeforeSceneLoad)
		{
			elapsedTime += Time.deltaTime;
			fadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / delayBeforeSceneLoad);
			yield return null;
		}
		fadeImage.color = targetColor;
	}
}
