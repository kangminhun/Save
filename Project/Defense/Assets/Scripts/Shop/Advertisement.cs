using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advertisement : MonoBehaviour
{
    [SerializeField]
    private ShopPoint point;
    public void PointUp()
    {
        point.point += 100;
        PlayerPrefs.SetInt("PlayerPoint", point.point);
        int num = PlayerPrefs.GetInt("PlayerPoint");
        Debug.Log(num);
        gameObject.SetActive(false);
    }

}
