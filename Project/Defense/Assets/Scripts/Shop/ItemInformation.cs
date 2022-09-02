using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInformation : MonoBehaviour
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private TextMeshProUGUI itemName;
    private int itemPrice;
    [SerializeField]
    private Image itemImg;
    [SerializeField]
    private TextMeshProUGUI explanationText;
    [SerializeField]
    private ShopPoint shopPoint;
    void Start()
    {
        itemName.text = item.itemName;
        itemPrice = item.price;
        itemImg.sprite = item.itemImg;
        explanationText.text = item.explanation;
    }
    public void Buy()
    {
        if (shopPoint.point - itemPrice >= 0)
        {
            int point = shopPoint.point - itemPrice;
            shopPoint.point = point;
            PlayerPrefs.SetInt("PlayerPoint", shopPoint.point);
            Dontdestory.instance.inventory.AddItem(item);
            Debug.Log("구매완료");
        }
        else
            Debug.Log("돈 부족");
    }
}
