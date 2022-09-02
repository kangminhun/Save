using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	public float speed = 70f;

	public float explosionRadius = 0f;

	[Header("SlowTower")]
	public float slowRadius = 0f;
	public float slowPower;
	public float slowTime;

	public GameObject impactEffect;
	private int damage;
	public void Seek (Transform _target)
	{
		target = _target;
	}
	public void Damage(int bulletDamage)
    {
		damage = bulletDamage;
	}

	// Update is called once per frame
	void Update () {

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);

	}

	void HitTarget ()
	{
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		if (explosionRadius > 0f)
		{
			Explode();
		}
		else if (slowRadius > 0f)
		{
			Slowing();
		}
		else
		{
			Damage(target);
		}

		Destroy(gameObject);
	}
	void Slowing()
    {
		Collider[] colliders = Physics.OverlapSphere(transform.position, slowRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "EnemyBody")
			{
				Slow(collider.transform.parent);
				Damage(collider.transform.parent);
			}
		}
	}
	void Explode ()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "EnemyBody")
			{
				Damage(collider.transform.parent);
			}
		}
	}
	void Slow(Transform enemy)
    {
		Enemy e = enemy.GetComponent<Enemy>();

		if (e != null)
		{
			e.SlowBulletHit(slowPower, slowTime);
		}

	}
	void Damage (Transform enemy)
	{
		Enemy e = enemy.GetComponent<Enemy>();
		if (e != null)
		{
			e.TakeDamage(damage);
		}
		
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
