using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScenario2048 : MonoBehaviour
{
    [SerializeField]
    private Image imageMatrix;
    [SerializeField]
    private TextMeshProUGUI textMatrix;
    [SerializeField]
    private Sprite[] spritesMatrix;

    private int matrixIndex = 0;
    public void OnCilckGameStart()
    {
        PlayerPrefs.SetInt("BlockCount", matrixIndex + 3);
        SceneManager.LoadScene("2048Game");
    }
    public void OnCilckGameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }
    public void OnClickLeft()
    {
        matrixIndex = matrixIndex > 0 ? matrixIndex - 1 : spritesMatrix.Length - 1;

        imageMatrix.sprite = spritesMatrix[matrixIndex];
        textMatrix.text = spritesMatrix[matrixIndex].name;
    }
    public void OnClickRight()
    {
        matrixIndex = matrixIndex < spritesMatrix.Length - 1 ? matrixIndex + 1 : 0;

        imageMatrix.sprite = spritesMatrix[matrixIndex];
        textMatrix.text = spritesMatrix[matrixIndex].name;
    }
}
