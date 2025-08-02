using UnityEngine;

public class PlayerLevitator : MonoBehaviour
{
	[Header("Levitation Settings")]
	public Transform player;

	public float levitationHeight = 3f;

	public float levitationSpeed = 2f;

	private Vector3 targetPosition;

	private bool isLevitating;

	private CharacterController controller;

	private void OnEnable()
	{
		if (player == null)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
			if (gameObject != null)
			{
				player = gameObject.transform;
			}
		}
		if (player != null)
		{
			controller = player.GetComponent<CharacterController>();
			if (controller != null)
			{
				controller.enabled = false;
			}
			targetPosition = player.position + Vector3.up * levitationHeight;
			isLevitating = true;
		}
		else
		{
			Debug.LogWarning("Player reference is missing and no object tagged 'Player' was found.");
		}
	}

	private void Update()
	{
		if (isLevitating && !(player == null))
		{
			player.position = Vector3.MoveTowards(player.position, targetPosition, levitationSpeed * Time.deltaTime);
			if (Vector3.Distance(player.position, targetPosition) < 0.01f)
			{
				isLevitating = false;
			}
		}
	}
}
