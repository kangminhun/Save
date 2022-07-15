using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 포함 {idm name, content, point};
/// 모든 변수는 string이다.
/// </summary>
public class GoodsDataStr 
{
    public int id;
    public string name;
    public string content;
    public int point;
}

public class GoodsDataManager : MonoBehaviour
{
    private static GoodsDataManager instance;
    public static GoodsDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                GoodsDataManager obj = FindObjectOfType<GoodsDataManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    GoodsDataManager newGoodsDataManager = new GameObject("GoodsDataManager").AddComponent<GoodsDataManager>();
                    instance = newGoodsDataManager;
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private List<GoodsDataStr> goodsList = new List<GoodsDataStr>();

    /// <summary>
    /// 모든 굿즈 데이터를 가지고 있는 리스트 변수 반환
    /// </summary>
    public List<GoodsDataStr> GoodsList
    {
        get { return goodsList; }
        set { goodsList = value; }
    }

    /// <summary>
    /// 굿즈데이터를 굿즈 데이터 리스트에 추가
    /// </summary>
    public void AddGoodsData(GoodsDataStr data)
    {
        goodsList.Add(data);
        Test();
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<GoodsDataManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Test()
    {
        foreach (GoodsDataStr temp in goodsList)
        {
            Debug.Log($"id : {temp.id} | name : {temp.name} | point : {temp.point} | content : {temp.content}");
            Debug.Log(temp.id);
            Debug.Log(temp.name);
            Debug.Log(temp.point);
            Debug.Log(temp.content);
        }
    }

    public void ValueInitialization()
    {
    }
}
