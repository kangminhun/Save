using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public string menuSceneName = "MainMenu";

	public SceneFader sceneFader;

	public WaveSpawner waveSpawner;
	public void Retry ()
	{
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
		waveSpawner.Restart();
	}

	public void Menu ()
	{
		sceneFader.FadeTo(menuSceneName);
	}

}
