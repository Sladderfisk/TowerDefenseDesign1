using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
	/// <summary>
	/// Static instance of this class.
	/// </summary>
	public static SceneTransitions Instance { get; private set; }


	[SerializeField] private AnimationClip fadeOutAnimation;

	private Animator sceneTransitionsAnimator;


	private void OnEnable()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnDisable()
	{
		Instance = null;
	}


	private void Start()
	{
		sceneTransitionsAnimator = GetComponent<Animator>();
	}

	public void TransitionToScene(int buildIndex)
	{
		sceneTransitionsAnimator.SetTrigger("fade out");
		StartCoroutine(SceneLoader.LoadSceneAfterDelay(buildIndex, fadeOutAnimation.length));
	}
}
