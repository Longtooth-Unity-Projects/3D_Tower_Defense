using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Range(0,50)]
    [SerializeField] private int poolSize = 5;

    [Range(0.1f, 30.0f)]
    [SerializeField] private float spawnDelay = 1.0f;

    [SerializeField] GameObject enemyPrefab;

    private GameObject[] enemyPool;

    private void Awake()
    {
        PopulatePool();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        enemyPool = new GameObject[poolSize];
        for(int i = 0; i < enemyPool.Length; ++i)
        {
            enemyPool[i] = Instantiate(enemyPrefab, transform);
            enemyPool[i].SetActive(false);
        }
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < enemyPool.Length; i++)
        {
            if (enemyPool[i].activeInHierarchy == false)
            {
                enemyPool[i].SetActive(true);
                return;
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
