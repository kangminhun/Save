using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform target;
	private Enemy targetEnemy;

	[Header("General")]

	public float range = 15f;

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Use Sniper")]
	public bool usesniper = false;

	[Header("Use Laser")]
	public bool useLaser = false;

	public int damageOverTime = 30;
	public float slowAmount = .5f;

	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;
	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public Transform firePoint;
	public int bulletDamage;
 
	public int startDamege;
	public int startDamageOverTime;
	public Transform Head;

	[SerializeField]
	private Color[] startColor;
    private void OnValidate()
    {
		startDamege = bulletDamage;
		startDamageOverTime = damageOverTime;
		Head = partToRotate.transform.Find("Head");
	}
    private void Awake()
    {
		startColor = new Color[Head.GetComponentInChildren<Renderer>().materials.Length];
		for (int i = 0; i < startColor.Length; i++)
		{
			startColor[i] = Head.GetComponentInChildren<Renderer>().materials[i].color;
		}
	}
    void Start ()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			if(enemy.GetComponentInChildren<Renderer>().material.color.a>=0.9)
            {
				float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
				if (distanceToEnemy < shortestDistance)
				{
					shortestDistance = distanceToEnemy;
					nearestEnemy = enemy;
				}
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
		} else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
		{
			if (useLaser)
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
					impactEffect.Stop();
					impactLight.enabled = false;
				}
			}
			if(usesniper)
            {
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
				}
			}

			return;
		}

		LockOnTarget();

		if (useLaser)
		{
			Laser();
		}
		else
		{
			if(usesniper)
            {
				Sniper();
            }
			if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}
			fireCountdown -= Time.deltaTime;
		}

	}

	void LockOnTarget ()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}
	void Sniper()
    {
		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
		}
		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);
	}
	void Laser ()
	{
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		targetEnemy.Slow(slowAmount);

		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}

		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized;

		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}
	void WatchTower()
    {

    }
	void Shoot ()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
		{
			bullet.Seek(target);
			bullet.Damage(bulletDamage);
		}
	}
	public void DamegeUp(int up)
    {
		bulletDamage = up;
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
	public void UpColor(Color color)
    {
		Color[] colors = new Color[Head.GetComponentInChildren<Renderer>().materials.Length];
		for (int i = 0; i < colors.Length; i++)
        {
			colors[i] = color;
			Head.GetComponentInChildren<Renderer>().materials[i].color = colors[i];
		}
	}
	public void OriginColor()
    {
		Color[] originColor= new Color[startColor.Length];
		for (int i = 0; i < originColor.Length; i++)
		{
			originColor[i] = startColor[i];
			Head.GetComponentInChildren<Renderer>().materials[i].color = originColor[i];
		}
	}
}
