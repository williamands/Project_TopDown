using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private GameObject enemySpawnEffect;
    [SerializeField] private float WaitForSeconds;

    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        GameObject effect = Instantiate(enemySpawnEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        yield return new WaitForSeconds(WaitForSeconds);

        int randomEnemy = Random.Range(0, enemy.Length - 1);
        Instantiate(enemy[randomEnemy], transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
