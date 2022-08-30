using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController2048 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textCurrentScore;
    [SerializeField]
    private TextMeshProUGUI textHighScore;
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private Board board;
    public void UpdateCurrentScore(int score)
    {
        textCurrentScore.text = score.ToString();
    }
    public void UpdateHighScore(int score)
    {
        textHighScore.text = score.ToString();
    }
    public void OnClickGoToMain()
    {
        board.OnGameOver();
        SceneManager.LoadScene("2048Main");
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene("2048Game");
    }
    public void OnGameOver()
    {
        panelGameOver.SetActive(true);
    }
}
