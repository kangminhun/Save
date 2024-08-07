using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportTower : MonoBehaviour
{
	public GameManager gameManager;
	public int range;
    [SerializeField]
	private GameObject target;
	private Turret targetTurret;
    private TurretHp targetHp;
    [SerializeField]
	private GameObject[] turrets;
    private List<GameObject> myList = new List<GameObject>();

    [Header("타워 종류")]
    public bool powerUp;
    public bool shieldUp;
    public bool healing;
    public bool watch;

    [Header("업그레이드 정도")]
    [Range(0,10)]
    public float shieldCount;
    public int upgradepuset;
    public int healingPower;

    private float contdown;
    private GameObject[] targets;
    private void Update()
    {
        if (!watch)
            UpdateTarget_Turret();
        else
            UpdateTarget_Enemy();
    }
    public void UpdateTarget_Turret()
	{
        myList = gameManager.turretList;
        turrets = new GameObject[myList.Count];
        myList.Remove(this.gameObject);
        for (int i = 0; i < myList.Count; i++)
        {
            turrets[i] = myList[i];
        }

        for (int j = 0; j < turrets.Length; j++)
        {
            if (turrets[j] != null)
            {
                float distanceToTurret = Vector3.Distance(transform.position, turrets[j].transform.position);
                if (distanceToTurret <= range)
                {
                    target = turrets[j];
                    targetTurret = target.GetComponent<Turret>();
                    targetHp = target.GetComponent<TurretHp>();
                    if (targetTurret != null)
                    {
                        if (powerUp)
                        {
                            Supporting(upgradepuset);
                            targetTurret.UpColor(Color.red);
                        }
                        else if(shieldUp)
                        {
                            Shielder(shieldCount);
                            targetTurret.UpColor(Color.blue);
                        }
                        else if(healing)
                        {
                            Healer(healingPower);
                        }
                    }
                }
            }
        }
    }
    void UpdateTarget_Enemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        targets = enemies;
        Color targetColor;
        if (enemies != null )
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].GetComponentInChildren<Renderer>().material.color.a != 1)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
                    if (distanceToEnemy <= range)
                    {
                        target = enemies[i];
                        targetColor = target.GetComponentInChildren<Renderer>().material.color;
                        targetColor.a = 0.95f;
                        target.GetComponentInChildren<Renderer>().material.color = targetColor;
                    }
                    else
                    {
                        enemies[i].GetComponentInChildren<Renderer>().material.color = enemies[i].GetComponentInChildren<Enemy>().startColor;
                    }
                }
            }
        }
    }
    private void Supporting(int upgrad)
    {
		int damege= targetTurret.startDamege;
		int damageOverTime= targetTurret.startDamageOverTime;
        if (targetTurret.useLaser)
        {
            targetTurret.damageOverTime = damageOverTime + (damageOverTime * upgrad / 100);
        }
        else
        {
            targetTurret.DamegeUp(damege + (damege * upgrad / 100));
        }
    }
    private void Shielder(float shield)
    {
        targetHp.shield = shield*10;
    }
    private void Healer(int heal)
    {
        if (targetHp.hp < 100)
        {
            if (contdown <= 0)
            {
                if(targetHp.hp+heal<100)
                {
                    targetHp.hp += heal;
                    targetTurret.UpColor(Color.green);
                }    
                else
                {
                    targetHp.hp = 100;
                    targetTurret.OriginColor();
                }
                contdown = 10f;
            }
            contdown -= Time.deltaTime;
        }
    }
    public void SupportingEnd()
    {
        if (!watch)
        {
            for (int i = 0; i < turrets.Length; i++)
            {
                if (turrets[i] != null)
                {
                    float distanceToTurret = Vector3.Distance(transform.position, turrets[i].transform.position);
                    if (distanceToTurret <= range)
                    {
                        target = turrets[i];
                        targetTurret = target.GetComponent<Turret>();
                        targetHp = target.GetComponent<TurretHp>();
                        if (targetTurret != null)
                        {
                            PowerOrigin();
                            targetTurret.OriginColor();
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponentInChildren<Renderer>().material.color = targets[i].GetComponentInChildren<Enemy>().startColor;
            }
        }

    }
    public void PowerOrigin()
    {
        targetTurret.DamegeUp(targetTurret.startDamege);
        targetTurret.damageOverTime = targetTurret.startDamageOverTime;
        targetHp.shield = targetHp.startShield;
    }
}
