using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private Vector3[] enemySpawn;

    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnRepeat;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnRepeat);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemy.Length - 1);
        int randomSpawn = Random.Range(0, enemySpawn.Length);
        Instantiate(enemy[randomEnemy], enemySpawn[randomSpawn], Quaternion.identity);
    }
}
