using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaBaBoGame : MonoBehaviour
{
    public GameUi mainUi;
    private Camera playerCamera;
    private GameObject player;
    private GameObject npc;
    public NPCLookAt npclookat;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        npc = GameObject.Find("cat");
        npclookat = npc.GetComponent<NPCLookAt>();
        playerCamera = Camera.main;
    }
    public void GameStart()
    {
        playerCamera.transform.position = npclookat.point_One.position;
        npclookat.cameraMove = false;
        gameObject.SetActive(false);
        mainUi.UiOn(1);
    }
    public void End()
    {

        //ui활성화

        npclookat.previewCanvas.SetActive(true);
        npclookat.tempChatCanvas.transform.Find("X_Button").gameObject.SetActive(true);
        npclookat.tempChatCanvas.transform.Find("MessageNotice").gameObject.SetActive(true);

        //카메라

        player.transform.Find("cameraArm").gameObject.SetActive(true);
        playerCamera.transform.parent = player.transform.Find("cameraArm").transform.Find("ButtonCamera");
        playerCamera.transform.localPosition = new Vector3(0,0,0);
        playerCamera.transform.localRotation = Quaternion.identity;
        playerCamera.cullingMask |= 1 << LayerMask.NameToLayer("Player");

        //게임 ui 끄기

        mainUi.gameObject.SetActive(false);

        //player 이동 제한 해제

        npclookat.cameraMove = false;
        PlayerAnimator.shop = false;
    }
}
