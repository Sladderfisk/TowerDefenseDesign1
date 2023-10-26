using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseTextHandler : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI killedByText;


	// Start is called before the first frame update
	private void Start()
	{
		killedByText.text = killedByText.text + PlayerPrefs.GetString("killed_by") + '.';
	}
}
