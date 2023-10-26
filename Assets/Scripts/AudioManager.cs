using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	/// <summary>
	/// Static instance of this class.
	/// </summary>
	public static AudioManager Instance { get; private set; }


	[SerializeField] private AudioSource musicAudioSource;
	[SerializeField] private AudioSource sfxAudioSource;

	[SerializeField] private AudioClip gameOverClip;
	[SerializeField] private AudioClip månsScreamClip;


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


	private static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

			yield return null;
		}

		audioSource.Stop();
		audioSource.volume = startVolume;
	}

	private void PlayClip(AudioClip clip)
	{
		sfxAudioSource.clip = clip;
		sfxAudioSource.Play();
	}

	public void PlayGameOverClip()
	{
		PlayClip(gameOverClip);

		// Fade out the music
		StartCoroutine(FadeOut(musicAudioSource, 0.5f));
	}

	public void PlayMånsScreamClip()
	{
		PlayClip(månsScreamClip);

		// Disable the music
		musicAudioSource.Stop();
	}
}
