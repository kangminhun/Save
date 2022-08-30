using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    [SerializeField]
    Image image;
    public Image cooldownImg;
    private Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.itemImg;
                image.color = new Color(1, 1, 1, 1);
            }
            else
                image.color = new Color(1, 1, 1, 0);
        }
    }
    public bool skillOn;
    private float skillcooldown;
    public void Update()
    {
        if (item != null)
        {
            if (skillOn)
            {
                if (cooldownImg.fillAmount > 0)
                {
                    skillcooldown -= Time.deltaTime;
                    cooldownImg.fillAmount = skillcooldown / item.cooldown;
                }
                else
                {
                    skillOn = false;
                    cooldownImg.gameObject.SetActive(false);
                }
            }
            else
                skillcooldown = item.cooldown;
        }
    }
}
