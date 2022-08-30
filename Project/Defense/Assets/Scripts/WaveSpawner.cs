using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour {

	public static int killCount = 0;

	public Roundbundle[] rounds;

	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;

	public GameManager gameManager;

	private int waveIndex = 0;

	[SerializeField]
	private GameObject startButton;
	[SerializeField]
	private GameObject roundTextPanel;

    void Update ()
    {
		if (killCount != 0)
			if (killCount == rounds[waveIndex].count)
			{
				PlayerStats.Rounds++;
				if (PlayerStats.Rounds<rounds.Length)
					NextWave();
			}
    }
    public void Restart()
    {
		Roundbundle round = rounds[waveIndex];
		round.count = 0;
	}
	private void NextWave()
    {
		if (waveIndex < rounds.Length)
		{
			startButton.SetActive(true);
			Roundbundle round = rounds[waveIndex];
			round.count = 0;
			killCount = 0;
		}
		waveIndex++;		
	}
	public void StartSpawnWave()
    {
		startButton.SetActive(false);
		StartCoroutine(RoundStartUi());
		StartCoroutine(SpawnWave());
	}

	IEnumerator SpawnWave ()
	{
		Roundbundle round = rounds[waveIndex];
		round.count = 0;
		for (int i = 0; i < round.rounds.Length; i++)
		{
			round.count += round.rounds[i].count;
			for (int j = 0; j < round.rounds[i].count; j++)
            {
				SpawnEnemy(round.rounds[i].enemy.enemy);
				yield return new WaitForSeconds(1f / round.rounds[i].rate);
			}
		}
	}

	void SpawnEnemy (GameObject enemy)
	{
		if (!enemy.GetComponent<EnemyMovement>().moveMode)
			Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
		else
			Instantiate(enemy, spawnPoint.position+new Vector3(0,10,0), spawnPoint.rotation);
	}
	IEnumerator RoundStartUi()
    {
		roundTextPanel.SetActive(true);
		roundTextPanel.GetComponent<TextMeshProUGUI>().text = $"Round {waveIndex+1}";
		yield return new WaitForSeconds(3f);
		roundTextPanel.SetActive(false);
	}
}
