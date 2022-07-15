using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New GoodsData", menuName = "GoodsData")]
public class ItemInformation : ScriptableObject
{
    public int price;
    public Texture goodsImage;
    public string goodsName;
}
