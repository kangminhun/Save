using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string levelToLoad = "MainLevel";

	public SceneFader sceneFader;

	public void Play ()
	{
		sceneFader.FadeTo(levelToLoad);
	}
	public void Shop()
    {
		sceneFader.FadeTo("Shop");
	}
	public void InventoryOpen()
    {
		Dontdestory.instance.inventory.OpenWindow();
	}
	public void Quit ()
	{
		Debug.Log("Exciting...");
		Application.Quit();
	}

}
