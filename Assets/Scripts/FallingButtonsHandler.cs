using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class FallingButtonsHandler : MonoBehaviour
{
	/// <summary>
	/// Static instance of this class.
	/// </summary>
	public static FallingButtonsHandler Instance { get; private set; }


	[SerializeField] private float minDelay;
	[SerializeField] private float maxDelay;

	[SerializeField] private float minX;
	[SerializeField] private float maxX;

	[SerializeField] private GameObject[] buttonPrefabs;

	[SerializeField] private VideoPlayer cameraVideoPlayer;

	[SerializeField] private GameObject starParticleSystem;

	[SerializeField] private AudioSource musicAudioScorce;

	[SerializeField] private Canvas canvas;


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


	// Start is called before the first frame update
	private void Start()
	{
		foreach (GameObject buttonPrefab in buttonPrefabs)
		{
			StartCoroutine(SpawnButtonAfterDelay(buttonPrefab, Random.Range(minDelay, maxDelay)));
		}
	}

	private IEnumerator SpawnButtonAfterDelay(GameObject buttonPrefab, float delay)
	{
		yield return new WaitForSeconds(delay);

		if (canvas != null)
		{
			GameObject button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, canvas.transform);

			Vector3 spawnPosition = Camera.main.ViewportToScreenPoint(new Vector2(Random.Range(minX, maxX), 1.1f)) - new Vector3(Screen.width, Screen.height, 0) / 2;

			//Vector2 referenceResolution = canvas.GetComponent<CanvasScaler>().referenceResolution;
			/*spawnPosition.x *= (referenceResolution.x / 2) / Camera.main.orthographicSize;
			spawnPosition.y *= (referenceResolution.y / 2) / (Camera.main.orthographicSize * Camera.main.aspect);*/

			button.transform.localPosition = spawnPosition;

			if (button.tag == "PlayButton")
			{
				button.GetComponent<Button>().onClick.AddListener(Play);
			}
			else if (button.tag == "ExitButton")
			{
				button.GetComponent<Button>().onClick.AddListener(Exit);
			}

			StartCoroutine(DestroyButtonAfterItLeavesScreen(button, buttonPrefab));
		}
	}

	private IEnumerator DestroyButtonAfterItLeavesScreen(GameObject button, GameObject buttonPrefab)
	{
		while (true)
		{
			// Wait for next frame
			yield return null;

			if (button != null && Camera.main.ScreenToViewportPoint(button.transform.position).y < -0.15)
			{
				Destroy(button);

				StartCoroutine(SpawnButtonAfterDelay(buttonPrefab, Random.Range(minDelay, maxDelay)));

				break;
			}
		}
	}

	public void Play()
	{
		StartCoroutine(PlaySceneTransition());
	}

	public IEnumerator PlaySceneTransition()
	{
		Destroy(canvas.gameObject);
		Destroy(starParticleSystem);

		cameraVideoPlayer.Play();

		StartCoroutine(FadeOutMusic((float)cameraVideoPlayer.clip.length));
		yield return new WaitForSeconds((float)cameraVideoPlayer.clip.length + 0.2f);
		
		// After the clip has ended load the game scene
		SceneManager.LoadScene(1);
	}

	private IEnumerator FadeOutMusic(float duration)
	{
		while (true)
		{
			yield return null;

			musicAudioScorce.volume -= Time.deltaTime / duration;

			if (musicAudioScorce.volume < 0)
			{
				break;
			}
		}
	}

	public void Exit()
	{
		Application.Quit();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;

		Gizmos.DrawLine(Camera.main.ViewportToWorldPoint(new Vector2(minX, 0)), Camera.main.ViewportToWorldPoint(new Vector2(minX, 1)));
		Gizmos.DrawLine(Camera.main.ViewportToWorldPoint(new Vector2(maxX, 0)), Camera.main.ViewportToWorldPoint(new Vector2(maxX, 1)));
	}
}
