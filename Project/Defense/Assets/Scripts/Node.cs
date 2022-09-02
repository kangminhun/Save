using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public enum isUpgraded { original,up1, up2, up3, up4 };
public class Node : MonoBehaviour
{

	public Color hoverColor;
	public Color notEnoughMoneyColor;
	public Vector3 positionOffset;
	public GameManager gameManager;

	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public isUpgraded upgraded;
	[HideInInspector]
	public int upgradeNum;
	[HideInInspector]
	public int upgradeCost;
	private Renderer rend;
	private Color startColor;

	BuildManager buildManager;

	private SupportTower support;
	void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;

		buildManager = BuildManager.instance;
	}

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}
    void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (turret != null)
		{
			buildManager.SelectNode(this);
			return;
		}
		if (!buildManager.CanBuild)
			return;


		BuildTurret(buildManager.GetTurretToBuild());
	}

	void BuildTurret(TurretBlueprint blueprint)
	{
		if (PlayerStats.Money < blueprint.cost)
		{
			Debug.Log("µ∑∫Œ¡∑");
			return;
		}
		if (PlayerStats.Money - blueprint.cost > 0)
		{
			PlayerStats.Money -= blueprint.cost;

			GameObject _turret = (GameObject)Instantiate(blueprint.prefab[0], GetBuildPosition(), Quaternion.identity);
			turret = _turret;

			turretBlueprint = blueprint;

			GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
			Destroy(effect, 5f);

			upgradeCost = turretBlueprint.upgradeCost;
			gameManager.turretList.Add(turret);
			support = turret.GetComponent<SupportTower>();
			if (support != null)
			{
				support.gameManager = gameManager;
			}
		}
		else
		{
			Debug.Log("µ∑∫Œ¡∑");
			return;
		}
	}

	public void UpgradeTurret()
	{
		if (PlayerStats.Money < upgradeCost)
		{
			Debug.Log("Not enough money to upgrade that!");
			return;
		}

		PlayerStats.Money -= upgradeCost;


		//Get rid of the old turret
		gameManager.turretList.Remove(turret);
		Destroy(turret);

		upgradeNum = (int)upgraded;
		upgradeNum++;

		//Build a new one
		GameObject _turret = (GameObject)Instantiate(turretBlueprint.prefab[upgradeNum], GetBuildPosition(), Quaternion.identity);
		turret = _turret;
		gameManager.turretList.Add(turret);
		support = turret.GetComponent<SupportTower>();

		GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		if (turret.GetComponent<Turret>() != null)
		{
			if (turret.GetComponent<Turret>().useLaser)
			{
				int damage = turret.GetComponent<Turret>().damageOverTime;
				damage += damage * 30 / 100;
				turret.GetComponent<Turret>().damageOverTime = damage;
			}
			else
			{
				int damage = turret.GetComponent<Turret>().bulletDamage;
				damage += damage * 30 / 100;
				turret.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().Damage(damage += damage * 30 / 100);
			}
		}

		if (support != null)
		{
			support.gameManager = gameManager;
			support.range += 5;
			if (support.powerUp)
			{
				support.upgradepuset += 10;
			}
			else if (support.shieldUp)
			{
				support.shieldCount += 1.5f;
			}
			else if (support.healing)
			{
				support.healingPower += 10;
			}
		}

		upgradeCost *= 2;

		upgraded = (isUpgraded)upgradeNum;

		Debug.Log("Turret upgraded!");
	}

	public void SellTurret()
	{
		if (upgraded == isUpgraded.original)
			PlayerStats.Money += turretBlueprint.GetSellAmount();
		else
			PlayerStats.Money += upgradeCost / 2;

		GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);
		if (support != null)
		{
			support.SupportingEnd();
		}
		gameManager.turretList.Remove(turret);
		Destroy(turret);

		turretBlueprint = null;
		if (upgraded != isUpgraded.up1)
		{
			upgraded = isUpgraded.up1;
		}
	}

	void OnMouseEnter()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (!buildManager.CanBuild)
			return;

		if (buildManager.HasMoney)
		{
			rend.material.color = hoverColor;
		}
		else
		{
			rend.material.color = notEnoughMoneyColor;
		}

	}

	void OnMouseExit()
	{
		rend.material.color = startColor;
	}
	public void ToDamage(int damage)
	{
		turret.GetComponent<TurretHp>().hp -= damage - (damage * turret.GetComponent<TurretHp>().shield / 100);

		if (turret.GetComponent<TurretHp>().hp <= 0)
		{
			GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
			Destroy(effect, 5f);

			if (support != null)
			{
				support.SupportingEnd();
			}

			BuildManager.instance.DeselectNode();
			gameManager.turretList.Remove(turret);
			Destroy(turret);
			turretBlueprint = null;
			if (upgraded != isUpgraded.up1)
			{
				upgraded = isUpgraded.up1;
			}
		}
	}
}
