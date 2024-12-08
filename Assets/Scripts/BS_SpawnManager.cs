using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] powerUpPrefabs;

    [SerializeField]
    public GameObject bossEnemyPrefab;


    BS_Player_Controller playerController;

    float spawnRange = 6f;
    int enemyCount;
    int waveNumber = 1;
    int bossRound = 5;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<BS_Player_Controller>();
    }
    public int WaveCount
    {
        get
        {
            return waveNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.IsGameOver)
        {
            GameLoop();
        }
    }
    void SpawnPowerUp(int spawnCount)
    {
        if (spawnCount > 5)
        {
            spawnCount = 5;
        }

        int randomIndex = Random.Range(0, powerUpPrefabs.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(powerUpPrefabs[randomIndex], GenerateSpawnPosition(), powerUpPrefabs[randomIndex].transform.rotation);
        }
    }
    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPosX, 0.5f, spawnPosZ);
    }
    void GameLoop()
    {
        enemyCount = FindObjectsOfType<EnemyProgressionAi>().Length;

        if (enemyCount == 0)
        {
            if (waveNumber % bossRound == 0)
            {
                bossWave(waveNumber);
            }
            else
            {
                SpawnEnemy(waveNumber);
            }
            SpawnPowerUp(waveNumber);
            waveNumber++;
        }
    }
    void bossWave(int currentRound)
    {

        GameObject boss = Instantiate(bossEnemyPrefab, GenerateSpawnPosition(), bossEnemyPrefab.transform.rotation);
    }
    void SpawnEnemy(int spawnCount)
    {
        if (spawnCount % 3 == 0)
        {
            spawnCount -= 1;
            Instantiate(bossEnemyPrefab, GenerateSpawnPosition(), bossEnemyPrefab.transform.rotation);
        }

        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(bossEnemyPrefab, GenerateSpawnPosition(), bossEnemyPrefab.transform.rotation);
        }
    }
}
