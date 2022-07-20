using UnityEngine;

public class NPCLookAt : Interact
{
    public GameObject me;
    public GameObject gameCanvas;
    public GameObject tempChatCanvas;
    public GameObject previewCanvas;
    public Transform point_One;
    public Transform point_Two;
    public Transform point_Three;
    public bool cameraMove;
    private GameObject player;
    private Camera playerCamera;
    public void Start()
    {
        previewCanvas = GameObject.Find("PreviewCanvas").gameObject;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameCanvas = other.GetComponent<ChatManager>().uiCanvas.gameObject.transform.Find("GaBaBoGame").gameObject;
            tempChatCanvas = other.GetComponent<ChatManager>().uiCanvas.gameObject;
            base.OnTriggerEnter(other);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 dir = other.transform.position - me.transform.position;
            //수정 코드
            Quaternion tempQue = Quaternion.Lerp(me.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);
            me.transform.rotation = Quaternion.Euler(new Vector3(0, tempQue.eulerAngles.y, 0));
            player = other.gameObject;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        cameraMove = false;
        base.OnTriggerExit(other);
    }

    protected override void InteractClick()
    {

        //ui 비활성화

        previewCanvas.SetActive(false);
        tempChatCanvas.transform.Find("X_Button").gameObject.SetActive(false);
        tempChatCanvas.transform.Find("MessageNotice").gameObject.SetActive(false);

        //플레이어 이동제어

        PlayerAnimator.shop = true;
        if (player.GetComponent<PlayerAnimator>().agent.enabled)
            player.GetComponent<PlayerAnimator>().agent.ResetPath();
        player.GetComponentInChildren<Animator>().SetBool("move", false);

        //카메라

        playerCamera = Camera.main;
        playerCamera.transform.parent = player.transform;
        playerCamera.cullingMask = playerCamera.cullingMask & ~(1 << LayerMask.NameToLayer("Player"));
        //player.transform.Find("cameraArm").gameObject.SetActive(false);
        cameraMove = true;

        //NPC Animation
        GetComponentInChildren<Animator>().SetBool("insa", true);

        OffActiveImage();
    }
    public void FixedUpdate()
    {
        if (cameraMove)
        {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, point_Two.position, Time.deltaTime * 2f);
            playerCamera.transform.LookAt(point_Three.position );
            if(Vector3.Distance(playerCamera.transform.position, point_Two.position)<=0.2f)
            {
                gameCanvas.SetActive(true);
            }
        }
    }
}
