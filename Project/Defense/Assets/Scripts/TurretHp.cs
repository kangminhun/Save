using UnityEngine;
using UnityEngine.UI;

public class TurretHp : MonoBehaviour
{
    public Canvas hpcanvas;
    public Image hpBar;
    public float shield;
    [HideInInspector]
    public float startShield;
    public float hp;
    public void Awake()
    {
        Canvas Bar = Instantiate(hpcanvas, transform.position + new Vector3(0, 3, 0), Quaternion.Euler(new Vector3(35, 0, 0)));
        hpBar = Bar.GetComponentInChildren<Image>();
        Bar.transform.parent = gameObject.transform;
        startShield = shield;
    }
	private void Update()
	{
		UpdataHpbar();
	}
	public void UpdataHpbar()
	{
		hpBar.fillAmount = hp / 100f;
		if (hpBar.fillAmount > 0.7)
		{
			hpBar.color = Color.green;
		}
		else if (hpBar.fillAmount > 0.3)
		{
			hpBar.color = Color.yellow;
		}
		else
		{
			hpBar.color = Color.red;

		}

	}
}
