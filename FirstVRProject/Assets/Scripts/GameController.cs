using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private bool gameOver;
	private bool restart;

	void Start () {
		gameOver = false;
		restart = false;
	}

	void Update () {
		if (gameOver) {
			restart = true;
		}
		if (restart) {
			Debug.Log ("restarting now...");
			SceneManager.LoadScene ("Test2");
		}
	}

	public void GameOver () {
		gameOver = true;
	}
}
