using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles loading of scenes for buttons as they cannot use the SceneManagement class.
/// </summary>
public class SceneLoader : MonoBehaviour
{
	/// <summary>
	/// Loads the selected scene.
	/// </summary>
	/// <param name="buildIndex">The scene by build index to load</param>
	public static void LoadScene(int buildIndex)
	{
		SceneManager.LoadScene(buildIndex);
	}

	/// <summary>
	/// Loads the scene after a delay.
	/// </summary>
	/// <param name="buildIndex">The scene by build index to load</param>
	/// <param name="delay">The delay in seconds</param>
	public static IEnumerator LoadSceneAfterDelay(int buildIndex, float delay)
	{
		yield return new WaitForSeconds(delay);

		LoadScene(buildIndex);
	}
}