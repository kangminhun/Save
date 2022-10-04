using Photon.Pun;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnimManager))]
public class Player : MonoBehaviour, IPunObservable
{
    Rigidbody rigid;
    private bool jumping;
    public Transform cameraArm;
    public Transform PlayerBody;
    //스피드 함수
    public float speed;
    private float startSpeed;
    //점프 함수
    public float jumpPower = 10;
    private float startJumpPower;

    Animator animator;
    PhotonView pvw;
    public int killCount = 1;
    public static int  maxUser = 0;
    public bool Jumping
    {
        set
        {
            if(jumping != value & rigid != null)
            {
                jumping = value;
                rigid.useGravity = !jumping;
            }
        }
    }

    void Start()
    {
        startJumpPower = jumpPower;
        startSpeed = speed;
        animator = GetComponentInChildren<Animator>();
        pvw = this.GetComponent<PhotonView>();
        rigid = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        if (!pvw.IsMine)
        {
            this.transform.Find("cameraArm").gameObject.SetActive(false);
            rigid.useGravity = false;
        }
    }

    void Update()
    {
        if (pvw.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetMouseButtonDown(0))
                animator.SetBool("Attack", true);
            else if (Input.GetMouseButtonUp(0))
                animator.SetBool("Attack", false);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("Speed", 2f);
                speed = 10;
                jumpPower = 6;
            }
            else
            {
                animator.SetFloat("Speed", moveInput.magnitude);
                speed = startSpeed;
                jumpPower = startJumpPower;
            }

            if (Input.GetKeyDown("space") && !jumping)
                Jump();           
            if (moveInput.magnitude != 0)
            {
               // animator.SetFloat("Speed", moveInput.magnitude);
                Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
                Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

                PlayerBody.forward = moveDir;
                transform.position += moveDir * speed * Time.deltaTime;
            }
            LookAround();
        }
    }
    public void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1F, 70F);
        }
        else
        {
            x = Mathf.Clamp(x, 350f, 360f);
        }
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
    public void Jump()
    {
        jumping = true;
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    //땅에 닿았을때 점프 가능
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Wall"))
            jumping = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(jumping);
        }
        else
        {
            this.Jumping = (bool)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void MovePrison()
    {
        if (killCount == maxUser)
            MiniGM.Instance.IsGameStart = false;
        this.transform.position = MiniGM.Instance.spawn.prisonPosition;
        killCount = 1;
    }
    
    [PunRPC]
    public void MoveStart(Vector3 position)
    {
        if (pvw.IsMine)
        {
            this.transform.position = position;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        MiniGM.Instance.IsGameStart = true;
    }

    [PunRPC]
    public void AddKill(int num)
    {
        Debug.Log(pvw.Owner.NickName + " : " + num + " / kill : " + killCount);
        killCount += num;
        if (killCount == maxUser)
        {
            StartCoroutine("EndingCoroutine");
        }
    }
    IEnumerator EndingCoroutine()
    {
        Debug.Log("마지막");
        Dictionary<int, Photon.Realtime.Player> players = PhotonNetwork.CurrentRoom.Players;
        List<int> keys = new List<int>(players.Keys);
        GameObject winer = null;
        GameObject mine = null;
        MiniGM.Instance.Ending = true;
        for (int i = 0; i < keys.Count;i++)
        {
            GameObject player = players[keys[i]].TagObject as GameObject;
            if (player.GetComponent<Player>().killCount == maxUser)
            {
                winer = player.transform.Find("cameraArm").gameObject;
                winer.SetActive(true);
            }
            else player.transform.Find("cameraArm").gameObject.SetActive(false);
            if (player.GetComponent<PhotonView>().IsMine) mine = player.transform.Find("cameraArm").gameObject;
        }
        yield return new WaitForSeconds(10f);
        MiniGM.Instance.Ending = false;
        pvw.RPC("MovePrison", RpcTarget.All);
        if (winer != null) winer.SetActive(false);
        else Debug.Log("winer is null");
        if (mine != null) mine.SetActive(true);
        else Debug.Log("mine is null");
    }

}
