using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="item",menuName ="item")]
public class Item : ScriptableObject
{
    public int price;
    public Sprite itemImg;
    public string itemName;
    public float cooldown;
    public enum Ability
    {
        ice,fire,damage
    }
    public Ability ability;
    [TextArea(5, 10)]
    public string explanation;
}
