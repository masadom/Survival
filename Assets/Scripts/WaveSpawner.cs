using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<EnemySpawn> enemies = new List<EnemySpawn>();
    public int currenttWave;
    public int waveValue;

    public List<Transform> SpawnLocations = new List<Transform>();
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    public List<GameObject> EnemiesToSpawn = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            //spawn
            if(EnemiesToSpawn.Count > 0)
            {
                int spawnIndex = Random.Range(0,SpawnLocations.Count());
                Instantiate(EnemiesToSpawn[0], SpawnLocations[spawnIndex].position,Quaternion.identity);
                EnemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer-= Time.fixedDeltaTime;
        }
    }

    public void GenerateWave()
    {
        waveValue = currenttWave * 10;
        GenerateEnemies();

        spawnInterval = waveDuration/EnemiesToSpawn.Count;//gives time between enemies
        waveTimer = waveDuration;
    }
    public void GenerateEnemies()
    {


        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].EnemyPrefab);
                waveValue -= randEnemyCost;
            }else if(waveValue - randEnemyCost <= 0)
            {
                break;
            }
        }

        EnemiesToSpawn.Clear();
        EnemiesToSpawn = generatedEnemies;
    }
}

[System.Serializable]
public class EnemySpawn
{
    public GameObject EnemyPrefab;
    public int cost;
}

