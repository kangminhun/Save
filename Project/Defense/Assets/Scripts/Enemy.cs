using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {

	public EnemyScriptable scriptable;
	public bool boss = false;
	[HideInInspector]
	public float speed;
	[HideInInspector]
	public bool slowing;
	[SerializeField]
	private float health;
	[SerializeField]
	private Transform body;

	public int worth = 50;

	public GameObject deathEffect;

	[Header("Unity Stuff")]
	public Image healthBar;

	private bool isDead = false;
	[HideInInspector]
	public Color startColor;
	private float slow;

	public bool attackMode;
	public int damege;
    void Start ()
	{
		startColor = body.GetComponent<MeshRenderer>().material.color;
		speed = scriptable.speed;
		health = scriptable.health;
	}
    public void TakeDamage (float amount)
	{
		health -= amount;

		healthBar.fillAmount = health / scriptable.health;
		if(healthBar.fillAmount>0.7)
        {
			healthBar.color = Color.green;
		}
		else if(healthBar.fillAmount > 0.3)
        {
			healthBar.color = Color.yellow;
		}
		else
        {
			healthBar.color = Color.red;
		}

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}
	public void Fire(int damage,float time)
    {
		StartCoroutine(StartFire(damage, time));
    }
	public void OffFire()
    {
		body.GetComponent<MeshRenderer>().material.color = startColor;
	}
	public void Slow (float pct)
	{
		speed = scriptable.speed * (1f - pct);
		//레이저에 맞고 있는동안 슬로우
	}
	public void StopSlow()
    {
		if (slow < 0)
		{
			body.GetComponent<MeshRenderer>().material.color = startColor;
			slowing = false;
		}
	}
	public void SlowBulletHit(float slowPower, float slowTime)
    {
		StartCoroutine(StartSlow(slowPower, slowTime));
		//탄에 맞으면 슬로우
		//스킬 슬로우
	}
	void Die ()
	{
		isDead = true;

		PlayerStats.Money += worth;

		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

		WaveSpawner.killCount++;

		Destroy(gameObject);
	}
    IEnumerator StartSlow(float slowPower, float slowTime)
    {
		while (slowTime > 0)
		{
			speed = scriptable.speed * (1f - slowPower);
			slowing = true;
			body.GetComponent<MeshRenderer>().material.color = Color.blue;
			slowTime -= Time.deltaTime;
			yield return null;
			slow = slowTime;
		}
	}
	IEnumerator StartFire(int damage,float time)
    {
		while (time > 0)
		{
			yield return new WaitForSeconds(1f);
			TakeDamage(damage);
			body.GetComponent<MeshRenderer>().material.color = Color.red;
			time -= 1;
		}
		if (time <= 0)
			OffFire();
	}
}
