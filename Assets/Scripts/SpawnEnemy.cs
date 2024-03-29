using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
  public GameObject enemyPrefab;
  public float spawnInterval = 2; // seconds between enemies
  public int maxEnemies = 20;	// max amount of enemies per wave
}


public class SpawnEnemy : MonoBehaviour {
	public GameObject[] waypoints;
	public GameObject testEnemyPrefab;
	public Wave[] waves;
	public int timeBetweenWaves = 5; // time in seconds before next wave

	private GameManagerBehaviour gameManager;

	private float lastSpawnTime;
	private int enemiesSpawned = 0; // track how many enemies spawned

	// Use this for initialization
	void Start () {
		lastSpawnTime = Time.time;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		int currentWave = gameManager.Wave;
		if (currentWave < waves.Length)
		{
			float timeInterval = Time.time - lastSpawnTime;
			float spawnInterval = waves[currentWave].spawnInterval;
			if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || (enemiesSpawned != 0 && timeInterval > spawnInterval)) && 
			(enemiesSpawned < waves[currentWave].maxEnemies))
			{
				lastSpawnTime = Time.time;
				GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefab);
				newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
				enemiesSpawned++;
			}
			if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				gameManager.Wave++;
				gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
				enemiesSpawned = 0;
				lastSpawnTime = Time.time;
			}
		}
		else
		{
			gameManager.gameOver = true;
			GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
			gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
		}
	}
}
