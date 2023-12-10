using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public Button startButton;
	public Button quitButton;

	private void Start()
	{
		startButton.onClick.AddListener(startGame);
		quitButton.onClick.AddListener(quitGame);
	}

	public void startGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void quitGame()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

}
