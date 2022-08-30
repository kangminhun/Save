using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSlot : MonoBehaviour
{
    public List<Item> items;
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;
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
            print("������ ���� �� �ֽ��ϴ�.");
    }
    public void RemoveItem(Item _item)
    {
        if (items.Count > 0)
        {
            items.Remove(_item);
            FreshSlot();
        }
        else
            print("����� �������� �����ϴ�..");
    }
    public void Click(int num)
    {
        if (slots[num].item != null)
        {
            Dontdestory.instance.inventory.AddItem(slots[num].item);
            RemoveItem(slots[num].item);
        }
    }
}
