using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;
	private Node targetNode;
	[SerializeField]
	private Transform body;
	public bool moveMode;
	[HideInInspector]
	public bool attack;
	GameObject[] targetTurret;
	private float count;
	void Start()
	{
		enemy = GetComponent<Enemy>();
		if (!moveMode)
		{
			target = Waypoints.points[0];
			body.transform.LookAt(Waypoints.points[0]);
		}
		else
		{
			target = FlyWayPoint.points[0];
			body.transform.LookAt(FlyWayPoint.points[0]);
		}
		targetTurret = GameObject.FindGameObjectsWithTag("Node");
	}

	void Update()
	{
		UpdateTarget();
		if (!enemy.attackMode)
		{
			Vector3 dir = target.position - transform.position;
			transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

			if (Vector3.Distance(transform.position, target.position) <= 0.4f)
			{
				if (!moveMode)
					GetNextWaypoint();
				else
					GetNextFlyWaypoint();
			}
			if (!enemy.slowing)
				enemy.speed = enemy.scriptable.speed;
			else
				enemy.StopSlow();
		}
		else
        {
			if (!attack)
            {
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

				if (Vector3.Distance(transform.position, target.position) <= 0.4f)
				{
					if (!moveMode)
						GetNextWaypoint();
					else
						GetNextFlyWaypoint();
				}
				if (!enemy.slowing)
					enemy.speed = enemy.scriptable.speed;
				else
					enemy.StopSlow();
			}
			else
            {
				if (targetNode != null)
				{
					body.transform.LookAt(targetNode.gameObject.transform);
					if (count <= 0)
					{
						AttackOn(targetNode);
						count = 3f;
					}
					count -= Time.deltaTime;
				}
				if (targetNode.turret==null)
				{
					if (!moveMode)
						body.transform.LookAt(Waypoints.points[wavepointIndex]);
					else
						body.transform.LookAt(FlyWayPoint.points[wavepointIndex]);
					attack = false;
				}
			}
        }
	}
	void UpdateTarget()
    {
		float shortestDistance = Mathf.Infinity;
		Node nearestNode = null;
		foreach (GameObject turret in targetTurret)
		{
			float distanceToTurret = Vector3.Distance(transform.position, turret.transform.position);
			if (distanceToTurret < shortestDistance)
			{
				shortestDistance = distanceToTurret;
				nearestNode = turret.GetComponent<Node>();
			}
		}
		if (nearestNode != null && shortestDistance <= 15f&& nearestNode.turret!=null)
        {
			targetNode = nearestNode;
			attack = true;
		}
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}

		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
		body.transform.LookAt(Waypoints.points[wavepointIndex]);
	}
	void GetNextFlyWaypoint()
    {
		if (wavepointIndex >= FlyWayPoint.points.Length - 1)
		{
			EndPath();
			return;
		}

		wavepointIndex++;
		target = FlyWayPoint.points[wavepointIndex];
		body.transform.LookAt(FlyWayPoint.points[wavepointIndex]);
	}
	//Á×Áö¾Ê°í µµÂøÇßÀ»½Ã
	void EndPath()
	{
		PlayerStats.Lives--;
		WaveSpawner.killCount++;
		Destroy(gameObject);
	}
	void AttackOn(Node targetOj)
    {
		targetOj.ToDamage(enemy.damege);
	}
}
