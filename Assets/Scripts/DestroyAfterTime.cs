using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
	[SerializeField] private float delay;
	[SerializeField] private bool needsToBeOffScreen;

	private CircleCollider2D circleCollider;

	// Start is called before the first frame update
	private void Start()
	{
		if (needsToBeOffScreen)
		{
			circleCollider = GetComponentInChildren<CircleCollider2D>();

			StartCoroutine(DestroyAfterDelay(delay));
		}
		else
		{
			Destroy(gameObject, delay);
		}
	}

	private IEnumerator DestroyAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);

		while (true)
		{
			if (circleCollider == null) { break; }

			Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

			// It's outside the screen in at least one direction
			if (
				viewportPosition.x < 0 - circleCollider.bounds.extents.x || viewportPosition.x > 1 + circleCollider.bounds.extents.x ||
				viewportPosition.y < 0 - circleCollider.bounds.extents.y || viewportPosition.y > 1 + circleCollider.bounds.extents.y
			)
			{
				Destroy(gameObject);
			}

			// Wait for the next second
			yield return null;
		}
	}
}
