using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrection : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverCanvas;
    public void ResurrectionButtonOn()
    {
        PlayerStats.Lives = 20;
        GameManager.GameIsOver = false;
        gameOverCanvas.SetActive(false);
        gameObject.SetActive(false);
    }
}
