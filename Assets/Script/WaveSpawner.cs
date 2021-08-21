using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING, WAITING, COUNTING
    };

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public Transform enemyPrefab;
        public int enemyCount;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoint;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float seachCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;

        if (spawnPoint.Length == 0)
        {
            Debug.LogError("Error! No spawn point reference");
        }
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if(EnemyIsAlives() == false)
            {
                // Begin a new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                // Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            // Game finished, code now, game is restart
            nextWave = 0;
            Debug.Log("All waves completed! Now Looping...");
        }
        else
        {
            nextWave++;
        }       
    }

    bool EnemyIsAlives()
    {
        seachCountdown -= Time.deltaTime;
        if(seachCountdown <= 0f)
        {
            seachCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            //if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.waveName);
        state = SpawnState.SPAWNING;

        // Spawn
        for(int i = 0; i < _wave.enemyCount; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        // Spawn enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
