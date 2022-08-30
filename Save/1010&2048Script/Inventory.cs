using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> items;
    [SerializeField]
    private Slot[] slots;
    [SerializeField]
    private Transform slotParent;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
    private void Awake()
    {
        FreshSlot();
    }
    //Slot 셋팅
    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i<slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }
    //아이템 추가 코드(순차적으로)
    public void Additme(Item item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(item);
            FreshSlot();
            // 아이템을 추가 한 뒤 다시 셋팅
        }
        else
            Debug.Log("가득참");
    }
    //아이템 삭제(순차적으로)
    public void RemoveItem(Item item)
    {
        if (items.Count > 0)
        {
            items.Remove(item);
            FreshSlot();
        }
        else
            Debug.Log("아이템이 없습니다");
    }
    public void ItemReset()
    {
        items.Clear();
        FreshSlot();
    }
}
