using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CctvView : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] cameras;
    public GameObject cctvCanvas;
    public GameObject cameraCanvas;
    public Text text;
    private string colorNameText;
    private GameObject player;
    private PhotonView pvw;
    private bool canvasOnOff;
    // 시작시 초기화 및 기본 세팅
    void Start()
    {
        CctvOn(0);
        cctvCanvas.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        pvw = player.GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)&&pvw.IsMine&&!MiniGM.Instance.Ending)
            TabButton();
        switch (player.GetComponent<PlayerStatus>().AtcColor)
        {
            case 0:
                colorNameText = "빨";
                break;
            case 1:
                colorNameText = "주";
                break;
            case 2:
                colorNameText = "노";
                break;
            case 3:
                colorNameText = "초";
                break;
            case 4:
                colorNameText = "파";
                break;
            case 5:
                colorNameText = "남";
                break;
            case 6:
                colorNameText = "보";
                break;
        }
        text.text = MiniGM.Instance.IsGameStart.ToString()+"\n"+player.GetComponent<Player>().killCount+"\n"+ colorNameText;
    }
    //tab키 누르면 작동
    void TabButton()
    {
        if (!canvasOnOff)
            OpenWindow();
        else
            CloseWindow();
    }
    // Canvas 켜기
    void OpenWindow()
    {
        Cursor.lockState = CursorLockMode.None;
        cctvCanvas.SetActive(true);
        cameraCanvas.SetActive(true);
        player.transform.Find("cameraArm").gameObject.SetActive(false);
        canvasOnOff = true;
    }
    // Canvas 끄기
    void CloseWindow()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CctvOn(0);
        cctvCanvas.SetActive(false);
        cameraCanvas.SetActive(false);
        player.transform.Find("cameraArm").gameObject.SetActive(true);
        canvasOnOff = false;
    }
    // Camera On/Off 세팅
    public void CctvOn(int cctvNum)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(false);
            cameras[cctvNum].SetActive(true);
        }
    }

}
