using UnityEngine;

[CreateAssetMenu(fileName ="Enemy",menuName ="Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public GameObject enemy;
    public float speed;
    public float health;
}
