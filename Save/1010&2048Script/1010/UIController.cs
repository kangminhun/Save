using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    private StageController stageController;

    [Header("InGame")]
    [SerializeField]
    private Text textCurrentScore;
    [SerializeField]
    private Text textHighScore;
    [SerializeField]
    private UIPausePanelAnimation pausePanel;

    [Header("GameOver")]
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private Screenshot screenshot;
    [SerializeField]
    private Image imageScreenshot;
    [SerializeField]
    private Text textResultScore;
    private void Update()
    {
        textCurrentScore.text = "<color=#FFFFFF>"+"현재점수"+"</color>"+ GetScoreInt(stageController.CurrentSocre).ToString();
        textHighScore.text = "<color=#FFFFFF>" + "최고점수" + "</color>" + GetScoreInt(stageController.HighScore).ToString();
    }
    public string GetScoreInt(int data)
    {
        return string.Format("{0:#,###}", data);
    }
    public void BtnClickPause()
    {
        pausePanel.OnAppear();
    }
    public void BtnClickHome()
    {
        SceneManager.LoadScene("01Main");
    }
    public void BtnClickRestart()
    {
        SceneManager.LoadScene("02Game");
    }
    public void BtnClickPlay()
    {
        pausePanel.OnDisappear();
    }
    public void GameOver()
    {
        imageScreenshot.sprite = screenshot.ScreenshotToSprite();
        textResultScore.text = GetScoreInt(stageController.CurrentSocre).ToString();

        panelGameOver.SetActive(true);
    }
}
