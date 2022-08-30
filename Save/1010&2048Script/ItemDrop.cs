using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemDrop : MonoBehaviour
{
    public Item item;
    [SerializeField]
    List<GameObject> items = new List<GameObject>();
    int itemIndex;
    GameObject activeItem;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            switch(item.type)
            {
                case (Item.Type)0://인벤토리에서 사용되는 템
                    Inventory userInv = other.GetComponentInChildren<Inventory>();
                    if (userInv != null) 
                        userInv.Additme(item);
                    break;
                case (Item.Type)1://바로 사용되는 템
                    activeItem.GetComponent<ActiveItem>().Interact(other.gameObject);
                    break;
            }
        }
    }
}
