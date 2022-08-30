using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScenario : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textHighScore;
    private void Awake()
    {
        textHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.Home))

            {
                Application.Quit();
            }
        }
    }

    public void BtnClickGameStart()
    {
        SceneManager.LoadScene("02Game");
    }
    public void BtnClickGameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
#if UNITY_ANDROID
        Application.Quit();
#endif
    }
}
