using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyscript;
    public GameObject enemyPrefab;
    public float spawnInterval = 5.0f;
    [SerializeField] private float nextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
        enemyscript.enemymove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyscript.enemymove == true)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemyRandomly();

                nextSpawnTime = Time.time + spawnInterval;
            }
        }
        else if (enemyscript.enemymove == false)
        {
            nextSpawnTime = float.MaxValue;
        }

    }

    void SpawnEnemyRandomly()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
