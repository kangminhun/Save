using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum part { Hair_Item, Items_Item, Bow_Item, Staking_Item, Top_Item, Pants_Item, Shoes_ltem, Skin_Item }
public class PrefabNameCopy : MonoBehaviour
{
    public ShopManual shop;
    public GameObject[] copyGameObject;
    public part Part;
    public void OnValidate()
    {
        shop = GetComponent<ShopManual>();
        switch (Part)
        {
            case (part)0:
                for (int i = 0; i < copyGameObject.Length; i++)
                {
                    shop.Hair_Item[i] = copyGameObject[i].name;
                }
                break;
            case (part)1:
                for (int i = 0; i < copyGameObject.Length; i++)
                {
                    shop.Items_Item[i] = copyGameObject[i].name;
                }
                break;
            case (part)2:
                for (int i = 0; i < copyGameObject.Length; i++)
                {
                    shop.Bow_Item[i] = copyGameObject[i].name;
                }
                break;
            case (part)3:
                for (int i = 0; i < copyGameObject.Length; i++)
                {
                    shop.Staking_Item[i] = copyGameObject[i].name;
                }
                break;
            case (part)4:
                for (int i = 0; i < copyGameObject.Length; i++)
                {
                    shop.Top_Item[i] = copyGameObject[i].name;
                }
                break;
            case (part)5:
                for (int i = 0; i < copyGameObject.Length; i++)
                {
                    shop.Pants_Item[i] = copyGameObject[i].name;
                }
                break;
            case (part)6:
                for (int i = 0; i < copyGameObject.Length; i++)
                {
                    shop.Shoes_ltem[i] = copyGameObject[i].name;
                }
                break;
            case (part)7:
                for (int i = 0; i < copyGameObject.Length; i++)
                {
                    shop.Skin_Item[i] = copyGameObject[i].name;
                }
                break;
        }
    }
}
