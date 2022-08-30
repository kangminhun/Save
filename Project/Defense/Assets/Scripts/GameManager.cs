using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool GameIsOver;

	public GameObject gameOverUI;
	public GameObject completeLevelUI;
	public WaveSpawner spawner;
	public List<GameObject> turretList = new List<GameObject>();
	void Start ()
	{
		GameIsOver = false;
		spawner = GetComponent<WaveSpawner>();
		//Cursor.lockState = CursorLockMode.Confined;
	}

	// Update is called once per frame
	void Update () {
		if (GameIsOver)
			return;

		if (PlayerStats.Lives <= 0)
		{
			EndGame();
		}
		if(PlayerStats.Rounds>= spawner.rounds.Length)
        {
			NextGame();
			PlayerPrefs.SetInt("PlayerPoint", 200);
			PlayerStats.Rounds = 0;
		}
	}

	void EndGame()
	{
		GameIsOver = true;
		gameOverUI.SetActive(true);
	}
	void NextGame()
    {
		GameIsOver = true;
		completeLevelUI.SetActive(true);
	}

}
