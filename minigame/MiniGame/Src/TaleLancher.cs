using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TaleLancher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [Tooltip("방 최대 인원")]
    private byte maxPlayersPerRoom = 20;
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    #region UI
    [SerializeField]
    private GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;
    [SerializeField]
    private GameObject LoginPanel;
    [SerializeField]
    private TextMeshProUGUI failedText;
    [SerializeField]
    private TMP_InputField idInputComp;
    [SerializeField]
    private TMP_InputField pwInputComp;
    private TMP_InputField curInputComp;
    private InputField currentField;
    [SerializeField]
    private Button LoginBtn;
    [SerializeField]
    private string spawnMapNum;
    #endregion
    const string playerNamePrefKey = "PlayerName";
    #endregion


    #region Private Fields
    /// <summary>
    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// </summary>
    //string gameVersion = "1";
    int hallNum = 0;
    RoomOptions RO;
    #endregion

    #region UnityFunc
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && controlPanel.activeInHierarchy)
        {
            SetFocus();
        }
        if (Input.GetKeyDown(KeyCode.Return) && controlPanel.activeInHierarchy && (curInputComp == pwInputComp || curInputComp == null))
        {
            //currentField == passText;
            LoginBtn.onClick.Invoke();
        }
    }
    #endregion

    #region MonoBehaviour CallBacks
    public override void OnConnectedToMaster()
    {
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        RoomJoinOrCreate();
    }

    public void LogInSet()
    {
        controlPanel.SetActive(true);
        progressLabel.SetActive(false);
    }

    private void SetFocus()
    {
        if (!controlPanel.activeInHierarchy)
        {
            return;
        }
        if(curInputComp == null || curInputComp == pwInputComp)
        {
            curInputComp = idInputComp;
        }
        else if(curInputComp == idInputComp)
        {
            curInputComp = pwInputComp;
        }
        curInputComp.ActivateInputField();
    }
    /// <summary>
    /// 메인 로비에서 해당 함수가 호출되는 경우는 Server가 닫혀있을 때 뿐이다.
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        if (cause.ToString() != "DisconnectByClientLogic") controlPanel.SetActive(true);
        //Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        if (cause.ToString() == "ClientTimeout")
        {
            progressLabel.SetActive(false);
            LoginPanel.SetActive(true);
            failedText.text = "서버 점검 중 입니다.";
        }
        pwInputComp.text = "";
    }


    /// <summary>
    /// 로비 접속 시
    /// 로컬 저장소에 유저 ID 저장
    /// 게임에 사용될 유저 데이터를 전부 요청한다. 요청된 데이터는 WebReq에서 자동으로 UserDataManager에 저장된다.
    /// 커스터마이징에 사용할 캐릭터 생성 ( 네트워크 연동처리가 안된 미리보기용 캐릭터이다. ) instinatedSingleCharacter();
    /// </summary>
    public override void OnJoinedLobby()
    {
        if (idInputComp.text != null)
        {
            PlayerPrefs.SetString(playerNamePrefKey, idInputComp.text);
        }
        Debug.Log("OnJoinedLobby");
        RoomJoinOrCreate();
    }

    #region RoomCallback
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(spawnMapNum);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        hallNum++;
        Debug.Log($"Room Name = {returnCode.ToString()} :: {message}");

        RoomJoinOrCreate();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        hallNum++;

        Debug.Log($"Room Name = {returnCode.ToString()} :: {message}");

        RoomJoinOrCreate();
    }
    #endregion

    #region CustomAuthentCallback
    /// <summary>
    /// 인증 실패
    /// </summary>
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        LoginPanel.SetActive(true);
        failedText.text = "입력하신 계정정보가 틀립니다. 다시 확인해주세요.";
        if (PlayerPrefs.HasKey(playerNamePrefKey) && PlayerPrefs.GetString(playerNamePrefKey) == idInputComp.text)
        {
        }
        else
        {
            idInputComp.text = "";
        }
        pwInputComp.text = "";
        controlPanel.SetActive(true);
        progressLabel.SetActive(false);
        base.OnCustomAuthenticationFailed(debugMessage);
    }

    /// <summary>
    /// 인증 성공 / AuthManager에 token 값 저장
    /// </summary>
    /// <param name="data"> token 값을 가지고 있다.</param>
    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        foreach (string tempText in data.Keys)
        {
            if (tempText == "token")
            {
                AuthManager.Instance.Token = data["token"].ToString();
            }
        }
        UserDataManager.Instance.NickName = PhotonNetwork.NickName;
        base.OnCustomAuthenticationResponse(data);
    }
    #endregion
    #endregion

    public void ConnectUI()
    {
        controlPanel.SetActive(false);
        progressLabel.SetActive(true);
        LoginPanel.SetActive(false);
        failedText.text = "";
    }

    public void RoomJoinOrCreate()
    {
        RO = new RoomOptions();
        RO.CleanupCacheOnLeave = false; // 생성한 유저가 이동 시, 캐쉬 유지
        RO.MaxPlayers = maxPlayersPerRoom;
        RO.IsVisible = true;
        string roomName = "TaleGame2";
        PhotonNetwork.JoinOrCreateRoom($"{roomName}{hallNum}", RO, TypedLobby.Default);
    }
}
