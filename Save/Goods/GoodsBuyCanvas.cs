using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GoodsBuyCanvas : MonoBehaviour
{
    // Item 버튼에서 받아온 Goods 아이템 정보
    public ItemInformation item;
    // 정보를 받아서 보여줄 변수
    public RawImage goodsImage;
    public Text goodsName;
    public Text goodsPriceText;
    //phoneNumber 입력UI 정보변수
    public InputField phoneNumber;
    public int phoneNumberInt;
    // 상품 구매 완료UI 정보변수
    public GameObject purchaseRequestCanvas;
    public GameObject purchaseCompleted_Canvas;
    public RectTransform itemInformation_Camvas;
    public RectTransform itemInformation_Vetor;
    public RectTransform itemInformation_Origin;
    public Text purchaseCompleted_Text;
    // 상품 구매 실패UI
    public GameObject inputFailed_Number;
    public GameObject inputFailed_Money;

    // 임시 스크립트
    public BuyGoods buyGoods;
    private void OnEnable()
    {
        goodsImage.texture = item.goodsImage;
        goodsName.text = item.goodsName;
        if (item.price != 0)
            goodsPriceText.text = GetThousandCommaText(item.price).ToString();
        else
            goodsPriceText.text = "Free";
        Initialization();
    }
    public void Goods()
    {
        if (phoneNumber.text != "")
            phoneNumberInt = int.Parse(phoneNumber.text);
        // 구매
        if (PointScore.instance.scoreValue >= item.price && phoneNumber.text.Length == 11)
        {
            if (!inputFailed_Number.activeInHierarchy && !inputFailed_Money.activeInHierarchy)
            {
                buyGoods.BuyItem();
                purchaseRequestCanvas.SetActive(false);
                purchaseCompleted_Canvas.SetActive(true);
                itemInformation_Camvas.anchoredPosition = itemInformation_Vetor.anchoredPosition;
                purchaseCompleted_Text.text = $"<b>{PhotonNetwork.NickName}</b>님,<b>{GetPhoneNumberText(phoneNumberInt)}</b>번호로 "+"\n"+"해당 상품 구매 신청이 완료되었습니다";
                PointScore.instance.PointDown(item.price);
                //구매 성공
            }
        }
        else if (phoneNumber.text.Length != 11)
            inputFailed_Number.SetActive(true);
           //번호이상으로 실패
        else
            inputFailed_Money.SetActive(true);
           //돈 부족으로 실패
    }
    public void No()
    {
        gameObject.SetActive(false);
        itemInformation_Camvas.anchoredPosition = itemInformation_Origin.anchoredPosition;
    }
    public void InputFailedCanvasOff()
    {
        phoneNumber.text = "";
        inputFailed_Number.SetActive(false);
        inputFailed_Money.SetActive(false);
    }
    // 초기화
    public void Initialization()
    {
        phoneNumber.text = "";
        purchaseRequestCanvas.SetActive(true);
        purchaseCompleted_Canvas.SetActive(false);
    }
    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
    public string GetPhoneNumberText(int data)
    {
        return string.Format("{0:0##-####-####}", data);
    }
}
