using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInventory : MonoBehaviour
{
    public List<Item> items;
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;
    private GameObject[] enemys;
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
    private void Awake()
    {
        if (Dontdestory.instance.mainSlot.items != null)
            items = Dontdestory.instance.mainSlot.items;
        FreshSlot();
    }
    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
            slots[i].skillOn = false;
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }
    //public void AddItem(Item _item)
    //{
    //    if (items.Count < slots.Length)
    //    {
    //        items.Add(_item);
    //        FreshSlot();
    //    }
    //    else
    //        print("슬롯이 가득 차 있습니다.");
    //}
    //public void RemoveItem(Item _item)
    //{
    //    if (items.Count > 0)
    //    {
    //        items.Remove(_item);
    //        FreshSlot();
    //    }
    //    else
    //        print("사용할 아이템이 없습니다..");
    //}
    public void SkillCooldown(Slot slot)
    {
        slot.skillOn = true;
        slot.cooldownImg.gameObject.SetActive(true);
        slot.cooldownImg.fillAmount = 1;
        Debug.Log(slot.skillOn);
    }
    public void Click(int num)
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (slots[num].item != null )
        { 
            switch (slots[num].item.ability)
            {
                case Item.Ability.ice:
                    if (!slots[num].skillOn)
                    {
                        Debug.Log("아이템 속성 얼음");
                        for (int i = 0; i < enemys.Length; i++)
                        {
                            enemys[i].GetComponent<Enemy>().SlowBulletHit(1, 10);
                        }
                        SkillCooldown(slots[num]);
                    }
                    break;
                case Item.Ability.fire:
                    if (!slots[num].skillOn)
                    {
                        Debug.Log("아이템 속성 불");
                        for (int i = 0; i < enemys.Length; i++)
                        {
                            enemys[i].GetComponent<Enemy>().Fire((int)enemys[i].GetComponent<Enemy>().scriptable.health*2/100, 12);
                        }
                        SkillCooldown(slots[num]);
                    }
                    break;
                case Item.Ability.damage:
                    if (!slots[num].skillOn)
                    {
                        Debug.Log("아이템 속성 데미지");
                        for (int i = 0; i < enemys.Length; i++)
                        {
                            enemys[i].GetComponent<Enemy>().TakeDamage(1000);
                        }
                        SkillCooldown(slots[num]);
                    }
                    break;
            }
        }
    }
}
