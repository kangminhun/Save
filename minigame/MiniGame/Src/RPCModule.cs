using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RPCModule : MonoBehaviour
{
    PlayerStatus ps;
    public Renderer playerRenderer;
    private Material maskMaterial;
    private List<Color> colors = new List<Color>();
    PhotonView pvw;

    private void Start()
    {
        ps = GetComponent<PlayerStatus>();
        pvw = GetComponent<PhotonView>();

        maskMaterial = playerRenderer.materials[2];

        colors.Add(Color.red);
        colors.Add(MakeColor("#FF5F15"));
        colors.Add(Color.yellow);
        colors.Add(Color.green);
        colors.Add(MakeColor("#00FFFF"));
        colors.Add(MakeColor("#202A44"));
        colors.Add(MakeColor("#800080"));

        pvw.Owner.TagObject = this.transform.gameObject;
    }
    private Color MakeColor(string hexCode)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hexCode, out color);
        return color;
    }

    [PunRPC]
    void Test(string a, string b)
    {
        Debug.Log("test : " + a + b);
    }

    /// <summary>
    /// 공격 색상과 방어 색상 설정
    /// </summary>
    /// <param name="atcColor"> 공격 색상</param>
    /// <param name="defColor"> 방어 색상</param>
    [PunRPC]
    void SetColor(int colorCount)
    {
        ps.DefColor = colorCount;
        maskMaterial.color = colors[colorCount];
    }

    /// <summary>
    /// 공격 색상 변경
    /// </summary>
    /// <param name="atcColor"> 공격 색상</param>
    [PunRPC]
    void UpdateColor(int atcColor)
    {
        ps.AtcColor = atcColor;
        string test = PhotonNetwork.NetworkingClient.NickName;

        if (pvw.IsMine)
        {
            Debug.Log($"{test} 공격 색상 : {atcColor}");
        }
    }

    [PunRPC]
    public void ChanageColor(int atcColor)
    {
        ps.AtcColor = atcColor;
        //해당 코드 사용 시, transform이 다수에 맞춰 동기화되는 현상이 나타난다.
        //pvw.RPC("UpdateColor", RpcTarget.All, atcColor);
    }

    private void OnDestroy()
    {
        Destroy(maskMaterial);
    }
}
