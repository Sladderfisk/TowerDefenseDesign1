using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingButtonMovement : MonoBehaviour
{
	[SerializeField] private float wobbleAmount;
	[SerializeField] private float wobbleSpeed;
	[SerializeField] private float fallSpeed;

	private float startingX;
	private float startingTime;

	// Start is called before the first frame update
	private void Start()
	{
		startingX = transform.position.x;

		startingTime = Time.time;
	}

	// Update is called once per frame
	private void Update()
	{
		transform.position = new Vector3(
			startingX + Mathf.Sin((Time.time - startingTime) * wobbleSpeed) * wobbleAmount,
			transform.position.y - fallSpeed * Time.deltaTime * 60,
			transform.position.z
		);
	}
}
