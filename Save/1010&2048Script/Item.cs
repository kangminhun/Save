using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itmeName;
    public Sprite itemImage;
    public Type type;
    public enum Type { InvenItem, Item }
}
