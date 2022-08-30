using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hit : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
       switch (other.tag)
        {
            case "Player":
                if (other.GetComponent<PlayerStatus>().DefColor == this.GetComponentInParent<PlayerStatus>().AtcColor
                     & MiniGM.Instance.IsGameStart)
                {
                    PhotonView myOwner = this.GetComponentInParent<PhotonView>();
                    if (this.GetComponentInParent<PhotonView>().IsMine)
                    {
                        myOwner.RPC("AddKill", RpcTarget.All, other.GetComponent<Player>().killCount);
                        myOwner.RPC("ChanageColor", RpcTarget.All, other.GetComponent<PlayerStatus>().AtcColor);
                        other.GetComponent<PhotonView>().RPC("MovePrison", RpcTarget.All);
                    }
                }
                else
                    return;
                break;
            case "GameStartButton":
                if (!MiniGM.Instance.IsGameStart & 
                    PhotonNetwork.PlayerList.Length >= 2)
                {
                    Player.maxUser = PhotonNetwork.PlayerList.Length;
                    if (PhotonNetwork.IsMasterClient)
                    {
                        MiniGM.Instance.StartTaleGame();
                        MiniGM.Instance.spawn.StartSpawn();
                    }
                }
                break;
        }
    }
}
