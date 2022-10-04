using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGM : MonoBehaviourPunCallbacks
{
    #region Fields
    private static MiniGM instance;
    public Vector3 startPoint;
    [SerializeField]
    private Quaternion rotation;

    private bool isGameStart = false;
    private bool ending;

    private ColorManager cManager;
    public SpawnManager spawn;

    public bool IsGameStart
    {
        get { return isGameStart; }
        set { isGameStart = value; }
    }
    public bool Ending
    {
        get { return ending; }
        set { ending = value; }
    }

    #endregion

    #region UnityFunc
    public static MiniGM Instance
    {
        get
        {
            if (instance == null)
            {
                MiniGM obj = FindObjectOfType<MiniGM>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    MiniGM miniGM = new GameObject("MiniGM").AddComponent<MiniGM>();
                    instance = miniGM;
                }
            }
            return instance;
        }
       
        private set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        var objs = FindObjectsOfType<MiniGM>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }
    void  Start()
    {
        cManager = this.gameObject.AddComponent<ColorManager>();
        //로컬 플레이어 생성
        if (PlayerNetManager.LocalPlayerInstance == null)
        {
            CreatingMultipleUsers(startPoint, rotation);
        }
        else
        {
            //Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
    }
    #endregion

    #region Photon Callbacks
    //나간 플레이어 Obj 삭제 코드
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        if (IsGameStart)
        {
            GameObject outPlayer = otherPlayer.TagObject as GameObject;
            PlayerStatus outStatus = outPlayer.GetComponent<PlayerStatus>();

            Dictionary<int, Photon.Realtime.Player> players = PhotonNetwork.CurrentRoom.Players;
            List<int> keys = new List<int>(players.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                GameObject playerUp = players[keys[i]].TagObject as GameObject;
                PlayerStatus upStatus = playerUp.GetComponent<PlayerStatus>();
                if (upStatus.AtcColor == outStatus.DefColor)
                {
                    upStatus.AtcColor = outStatus.AtcColor;
                    playerUp.GetComponent<Player>().AddKill( outPlayer.GetComponent<Player>().killCount);
                }
            }
        }
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.DestroyPlayerObjects(otherPlayer);
        }
        base.OnPlayerLeftRoom(otherPlayer);
    }
    #endregion

    #region DevFunc
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void CreatingMultipleUsers(Vector3 staticPosition, Quaternion quaternion)
    {
        PhotonNetwork.Instantiate("TaleGameUser123", staticPosition, quaternion, 0);
    }

    //꼬리잡기 게임 시작할 때 호출
    public void StartTaleGame()
    {
        cManager.StartColorGame();
    }
    #endregion
}