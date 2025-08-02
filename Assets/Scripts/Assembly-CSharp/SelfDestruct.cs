using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
	public float timeBeforeDestruction = 5f;

	public ParticleSystem destructionEffect;

	public AudioSource destructionSound;

	private void Start()
	{
		StartCoroutine(SelfDestructCoroutine());
	}

	private IEnumerator SelfDestructCoroutine()
	{
		yield return new WaitForSeconds(timeBeforeDestruction);
		if (destructionEffect != null)
		{
			Object.Instantiate(destructionEffect, base.transform.position, Quaternion.identity);
		}
		if (destructionSound != null)
		{
			destructionSound.Play();
			yield return new WaitForSeconds(destructionSound.clip.length);
		}
		Object.Destroy(base.gameObject);
	}
}
