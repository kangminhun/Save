using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOpen : MonoBehaviour
{
    public GameObject shopCanvas;
    public Text pointText;
    public CreateShopUI shopUI;
    public Texture testImg;
    public void Start()
    {
        List<GoodsDataStr> tempList = GoodsDataManager.Instance.GoodsList;
        for (int i = 0; i < tempList.Count; i++)
        {
            GoodsDataStr tempData = tempList[i];
            shopUI.GoodsData(tempData.point, tempData.name, testImg);
        }
    }
    public void ShopCanvasOff()
    {
        shopCanvas.SetActive(false);
        PlayerAnimator.shop = false;
    }
    public void Update()
    {
        if (PointScore.instance.scoreValue > 0)
            pointText.text = GetThousandCommaText(PointScore.instance.scoreValue).ToString();
        else
            pointText.text = "0";
    }
    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
}
