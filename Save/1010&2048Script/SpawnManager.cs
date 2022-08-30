using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager: MonoBehaviour
{
    public Vector3 prisonPosition;
    public GameObject[] responsePosition;

    public void StartSpawn()
    {
        int num = Random.Range(0, 7);
        List<int> numArr = new List<int>();
        Debug.Log("StartSpawn");
        if (!MiniGM.Instance.IsGameStart & PhotonNetwork.IsMasterClient)
        {
            Dictionary<int, Photon.Realtime.Player> players = PhotonNetwork.CurrentRoom.Players;
            List<int> keys = new List<int>(players.Keys);
            for(int i=0; i < keys.Count;)
            {
                if (numArr.Contains(num))
                {
                    num = Random.Range(0, 7);
                }
                else
                {
                    numArr.Add(num);
                    GameObject player = players[keys[i]].TagObject as GameObject;
                    player.GetComponent<PhotonView>().RPC("MoveStart", RpcTarget.All, responsePosition[num].transform.position);
                    i++;
                }
            }
        }
    }
}
