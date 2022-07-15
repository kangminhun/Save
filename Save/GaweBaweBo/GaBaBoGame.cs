using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaBaBoGame : MonoBehaviour
{
    public GameObject game;
    public GameObject gameGM;
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
        game.SetActive(true);
        //                   도박 ㄱ???
        //if (PointScore.instance.scoreValue >= 100)
        //{
        //    PointScore.instance.PointDown(100);
        //    gameObject.SetActive(false);
        //    game.SetActive(true);
        //}
        //else
        //    return;
    }
    public void End()
    {
        player.transform.Find("cameraArm").gameObject.SetActive(true);
        playerCamera.transform.parent = player.transform.Find("cameraArm").transform.Find("ButtonCamera");
        playerCamera.transform.position = player.transform.Find("cameraArm").transform.Find("ButtonCamera").transform.position;
        playerCamera.transform.LookAt(player.transform.Find("cameraArm"));
        playerCamera.cullingMask |= 1 << LayerMask.NameToLayer("Player");
        gameGM.SetActive(false);
        npclookat.cameraMove = false;
        PlayerAnimator.shop = false;
    }
}
