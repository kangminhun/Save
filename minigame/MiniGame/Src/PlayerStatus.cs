using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    int atcColor;
    int defColor;

    public int AtcColor
    {
        get { return atcColor; }
        set { atcColor = value; }
    }
    public int DefColor
    {
        get { return defColor; }
        set { defColor = value; }
    }
}
