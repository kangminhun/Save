using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour {

	public string menuSceneName = "MainMenu";

	public string nextLevel = "Level02";

	public int levelToUnlock = 15;

	public SceneFader sceneFader;

	public bool lastLevel;
	public void Continue ()
	{
		int previousStep = PlayerPrefs.GetInt("levelReached");
		if (previousStep < levelToUnlock)
			PlayerPrefs.SetInt("levelReached", levelToUnlock);
		if (!lastLevel)
			sceneFader.FadeTo(nextLevel);
	}

	public void Menu ()
	{
		sceneFader.FadeTo(menuSceneName);
	}

}
