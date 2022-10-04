using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ColorManager : MonoBehaviour
{

    private Dictionary<string, int> userColor = new Dictionary<string, int>();
    private Dictionary<int, bool> colorLive = new Dictionary<int, bool>();
    private List<string> gamePlayIds = new List<string>();
    private List<int> colorArr;
    private PhotonView obw;
    private int liveUser = 0;

    public int LiveUser
    {
        get { return liveUser; }
        set { liveUser = value; }
    }

    void Start()
    {
        obw = this.GetComponent<PhotonView>();
    }

    //외부에서 RPC 실행 할 때, 편하게
    public void GMRPC(string methodName, RpcTarget target)
    {
        obw.RPC(methodName, target);
    }

    public void GMRPC(string methodName, RpcTarget target, params object[] parameters)
    {
        obw.RPC(methodName, target, parameters);
    }

    public void StartColorGame()
    {
        if (PhotonNetwork.IsMasterClient) ColorSetting();
        else obw.RPC("ColorSetting", RpcTarget.MasterClient);
    }
    //색깔 생성 및 전파
    private void ColorInit()
    {
        int min = 0, max = 7;
        int currentNumber = Random.Range(min, max);
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        //게임에 참여한 Player 이름 배열 셋팅
        foreach (int count in PhotonNetwork.CurrentRoom.Players.Keys)
        {
            gamePlayIds.Add(PhotonNetwork.CurrentRoom.Players[count].NickName);
        }

        //랜덤 값 생성. 겹치지 않는 0~6;
        for (int i = 0; i < playerCount;)
        {
            if (colorLive.ContainsKey(currentNumber))
                currentNumber = Random.Range(min, max);
            else
            {
                colorLive.Add(currentNumber, false);
                userColor.Add(gamePlayIds[i], currentNumber);
                i++;
            }
        }

        //외부 전파 
        //List는 포톤 매개변수로 전달되지 않아서 Arr로 변경 후 전달
        string[] sendStr = gamePlayIds.ToArray();
        obw.RPC("SetClientColor", RpcTarget.Others, userColor);
        obw.RPC("SetLiveColor", RpcTarget.Others, colorLive);
        obw.RPC("SetUserIds", RpcTarget.Others, sendStr);
        //얼굴색변경 함수 실행
        //해당 호출은 마스터 클라이언트만 변경
        //다른 클라이언트들은 SetUserIds에 동일한 코드가 주입되어 있다.
        SetColorArr(colorLive);
        ChangeMaskColor();
    }

    //초기화
    private void ColorReset()
    {
        userColor.Clear();
        colorLive.Clear();
        gamePlayIds.Clear();
    }

    [PunRPC]
    public void ColorSetting()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("ColorSetting");
            if (userColor.Count != 0) ColorReset();
            ColorInit();
        }
    }

    //배정 된 색 전파
    [PunRPC]
    public void SetClientColor(Dictionary<string, int> dictionary)
    {
        userColor = new Dictionary<string, int>(dictionary);
    }

    //살아있는 색 전파
    [PunRPC]
    public void SetLiveColor(Dictionary<int, bool> dictionary)
    {
        colorLive = new Dictionary<int, bool>(dictionary);
        SetColorArr(colorLive);
    }
    
    private void SetColorArr(Dictionary<int, bool> dictionary)
    {
        colorArr = new List<int>(colorLive.Keys);
        colorArr.Sort();
    }

    //참여한 유저 정보 전파
    [PunRPC]
    public void SetUserIds(string[] ids)
    {
        gamePlayIds = new List<string>(ids);
        ChangeMaskColor();
    }

    //유저 캐릭터 색깔 셋팅 및 얼굴 색상 변경
    private void ChangeMaskColor()
    {
        string myName = PhotonNetwork.NetworkingClient.NickName;
        int max = colorArr.Count;
        int myAtcCol = 0;
        int userColNum = userColor[myName];
        GameObject myObj = PhotonNetwork.LocalPlayer.TagObject as GameObject;
        for(int i = 0; i < max; i++)
        {
            int colNum = colorArr[i];
            if (i < max  & colNum == userColNum)
            {
                myAtcCol = i == max-1 ? colorArr[0] : colorArr[i + 1];
                break;
            }
        }

        myObj.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.All, userColNum);
        myObj.GetComponent<PhotonView>().RPC("UpdateColor", RpcTarget.All, myAtcCol);
    }
}
