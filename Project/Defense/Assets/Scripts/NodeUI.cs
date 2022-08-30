using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

	public GameObject turretRangeRow;
	public GameObject ui;

	public Text upgradeCost;
	public Button upgradeButton;

	public Text sellAmount;

	private Node target;
	private GameObject rangeObjet;
	public void SetTarget (Node _target)
	{
		target = _target;

		transform.position = target.GetBuildPosition();
		if(rangeObjet!=null)
        {
			Destroy(rangeObjet);
        }
		if (target.upgraded != isUpgraded.up4)
		{
			upgradeCost.text = "$" + target.upgradeCost;
			upgradeButton.interactable = true;
		}
		else
		{
			upgradeCost.text = "DONE";
			upgradeButton.interactable = false;
		}
		if (target.upgraded == isUpgraded.original)
			sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();
		else
			sellAmount.text = "$" + target.upgradeCost/2;

		if (target.turret.GetComponent<Turret>() != null)
		{
			float range = target.turret.GetComponent<Turret>().range;
			GameObject rangeRow = Instantiate(turretRangeRow, target. turret.transform.position, Quaternion.identity);
			rangeRow.transform.localScale = new Vector3(range*2, 0.2f, range*2);
			Color oldColor = rangeRow.GetComponent<Renderer>().material.color;
			Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
			rangeRow.GetComponent<Renderer>().material.SetColor("_Color", newColor);
			rangeObjet = rangeRow;
		}
		else if(target.turret.GetComponent<SupportTower>() != null)
        {
			float range = target.turret.GetComponent<SupportTower>().range;
			GameObject rangeRow = Instantiate(turretRangeRow, target.turret.transform.position, Quaternion.identity);
			rangeRow.transform.localScale = new Vector3(range * 2, 0.2f, range * 2);
			Color oldColor = Color.green;
			Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
			rangeRow.GetComponent<Renderer>().material.SetColor("_Color", newColor);
			rangeObjet = rangeRow;
		}

		ui.SetActive(true);
	}

	public void Hide ()
	{
		ui.SetActive(false);
		Destroy(rangeObjet);
	}

	public void Upgrade ()
	{
		target.UpgradeTurret();
		BuildManager.instance.DeselectNode();
	}

	public void Sell ()
	{
		target.SellTurret();
		BuildManager.instance.DeselectNode();
	}
}
