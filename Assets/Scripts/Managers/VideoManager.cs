using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
	/// <summary>
	/// Static instance of this class.
	/// </summary>
	public static VideoManager Instance { get; private set; }


	[SerializeField] private VideoPlayer videoPlayer;

	[SerializeField] private VideoClip bloodSplatterClip;
	[SerializeField] private VideoClip nullReferenceExceptionClip;


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


	public void PlayBloodSplatterClip()
	{
		videoPlayer.clip = bloodSplatterClip;
		videoPlayer.Play();
	}

	public void PlayNullReferenceExceptionClip()
	{
		videoPlayer.clip = nullReferenceExceptionClip;
		videoPlayer.Play();
	}
}