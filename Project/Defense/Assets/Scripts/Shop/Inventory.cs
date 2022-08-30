using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;
    [SerializeField]
    private GameObject main;
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
    private void Awake()
    {
        FreshSlot();
    }
    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }
    public void AddItem(Item _item)
    {
        
        if (items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
            Debug.Log("슬롯이 가득 차 있습니다.");
    }
    public void RemoveItem(Item _item)
    {
        if (items.Count > 0)
        {
            items.Remove(_item);
            FreshSlot();
        }
        else
            Debug.Log("사용할 아이템이 없습니다..");
    }
    public void OpenWindow()
    {
        main.SetActive(true);
    }
    public void CloseWindow()
    {
        main.SetActive(false);
    }
    public void Click(int num)
    {
        if (slots[num].item != null)
        {
            Dontdestory.instance.mainSlot.AddItem(slots[num].item);
            RemoveItem(slots[num].item);
        }
    }
}
