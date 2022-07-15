using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLookAt : Interact
{
    Quaternion startRotation;
    public GameObject me;
    public GameObject gameCanvas;
    public Transform point_One;
    public Transform point_Two;
    public Transform point_Three;
    public bool cameraMove;
    private GameObject player;
    private Camera playerCamera;
    public void Start()
    {
        startRotation = me.transform.rotation;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        gameCanvas = other.GetComponent<ChatManager>().uiCanvas.gameObject.transform.Find("GaBaBoGame").gameObject;
        base.OnTriggerEnter(other);
    }
    public void OnTriggerStay(Collider other)
    {
        Vector3 dir = other.transform.position - me.transform.position;
        //수정 코드
        Quaternion tempQue = Quaternion.Lerp(me.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);
        me.transform.rotation = Quaternion.Euler(new Vector3(0, tempQue.eulerAngles.y, 0));
        player = other.gameObject;
    }
    protected override void OnTriggerExit(Collider other)
    {
        me.transform.rotation = startRotation;
        cameraMove = false;
        base.OnTriggerExit(other);
    }

    protected override void InteractClick()
    {
        player.GetComponent<PlayerAnimator>().agent.ResetPath();
        player.GetComponentInChildren<Animator>().SetBool("move", false);
        playerCamera = Camera.main;
        playerCamera.transform.parent = player.transform;
        playerCamera.cullingMask = playerCamera.cullingMask & ~(1 << LayerMask.NameToLayer("Player")); ;
        player.transform.Find("cameraArm").gameObject.SetActive(false);
        cameraMove = true;
        PlayerAnimator.shop = true;
        gameCanvas.SetActive(true);
        GetComponentInChildren<Animator>().SetBool("insa", true);
        OffActiveImage();
    }
    public void FixedUpdate()
    {
        if (cameraMove)
        {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, point_Two.position, Time.deltaTime * 2f);
            playerCamera.transform.LookAt(point_Three.position );
        }
    }
}
