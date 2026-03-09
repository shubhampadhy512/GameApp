
// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class wavespawner : MonoBehaviour
// {
//     [Header("Enemy")]
//     public GameObject enemyPrefab;
//     public float groundY = -2f;

//     [Header("Wave Settings")]
//     public int maxWaves = 5;

//     [Header("Manual Enemy Count Per Wave")]
//     public List<int> manualEnemiesPerWave = new List<int>();

//     [Header("Random Enemy Range")]
//     public int randomMinEnemies = 3;
//     public int randomMaxEnemies = 8;

//     private int enemiesAlive;
//     private int currentWave = 1;

//     void Start()
//     {
//         StartCoroutine(WaveLoop());
//     }

//     IEnumerator WaveLoop()
//     {
//         while (currentWave <= maxWaves)
//         {
//             int enemiesThisWave = GetEnemiesForWave();

//             SpawnWave(enemiesThisWave);

//             yield return new WaitUntil(() => enemiesAlive <= 0);

//             currentWave++;

//             yield return new WaitForSeconds(2f);
//         }

//         Debug.Log("PLAYER WINS!");
//     }

//     int GetEnemiesForWave()
//     {
//         // If manually set for this wave
//         if (currentWave - 1 < manualEnemiesPerWave.Count)
//         {
//             return manualEnemiesPerWave[currentWave - 1];
//         }

//         // Otherwise random
//         return Random.Range(randomMinEnemies, randomMaxEnemies + 1);
//     }

//     void SpawnWave(int enemyCount)
//     {
//         enemiesAlive = enemyCount;

//         Debug.Log("Wave " + currentWave + " started with " + enemyCount + " enemies.");

//         for (int i = 0; i < enemyCount; i++)
//         {
//             float xPos = (i % 2 == 0) ? -9f : 9f;
//             Vector2 spawnPos = new Vector2(xPos, groundY);

//             GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//             Damageable dmg = enemy.GetComponent<Damageable>();
//             if (dmg != null)
//             {
//                 dmg.onDeath += OnEnemyDeath;
//             }
//         }
//     }

//     void OnEnemyDeath()
//     {
//         enemiesAlive--;
//         Debug.Log("Enemy died. Remaining: " + enemiesAlive);
//     }
// }   

// using UnityEngine;
// using System.Collections;

// public class wavespawner : MonoBehaviour
// {
//     [Header("Enemy")]
//     public GameObject enemyPrefab;
//     public float groundY = -2f;

//     [Header("Spawn Range")]
//     public float leftSpawnMin = -9f;
//     public float leftSpawnMax = -6f;

//     public float rightSpawnMin = 6f;
//     public float rightSpawnMax = 9f;

//     [Header("Wave Settings")]
//     public int maxWaves = 6;

//     [Header("Difficulty")]
//     public int startEnemies = 3;
//     public int enemiesIncreasePerWave = 2;

//     [Header("Spawn Timing")]
//     public float minSpawnDelay = 0.3f;
//     public float maxSpawnDelay = 0.8f;

//     private int enemiesAlive;
//     private int currentWave = 1;

//     void Start()
//     {
//         StartCoroutine(WaveLoop());
//     }

//     IEnumerator WaveLoop()
//     {
//         while (currentWave <= maxWaves)
//         {
//             int enemiesThisWave = startEnemies + (currentWave * enemiesIncreasePerWave);

//             Debug.Log("Wave " + currentWave + " spawning " + enemiesThisWave + " enemies");

//             yield return StartCoroutine(SpawnWave(enemiesThisWave));

//             yield return new WaitUntil(() => enemiesAlive <= 0);

//             currentWave++;

//             yield return new WaitForSeconds(2f);
//         }

//         Debug.Log("PLAYER WINS!");
//     }

//     IEnumerator SpawnWave(int enemyCount)
//     {
//         enemiesAlive = enemyCount;

//         for (int i = 0; i < enemyCount; i++)
//         {
//             float spawnX;

//             if (Random.value > 0.5f)
//             {
//                 // spawn on left side
//                 spawnX = Random.Range(leftSpawnMin, leftSpawnMax);
//             }
//             else
//             {
//                 // spawn on right side
//                 spawnX = Random.Range(rightSpawnMin, rightSpawnMax);
//             }

//             Vector2 spawnPos = new Vector2(spawnX, groundY);

//             GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//             Damageable dmg = enemy.GetComponent<Damageable>();

//             if (dmg != null)
//             {
//                 dmg.onDeath += OnEnemyDeath;
//             }

//             yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
//         }
//     }

//     void OnEnemyDeath()
//     {
//         enemiesAlive--;
//         Debug.Log("Enemy died. Remaining: " + enemiesAlive);
//     }
// }
// using UnityEngine;
// using System.Collections;

// public class wavespawner : MonoBehaviour
// {
//     [Header("Enemy")]
//     public GameObject enemyPrefab;
//     public float groundY = -2f;

//     [Header("Spawn Range")]
//     public float leftSpawnMin = -9f;
//     public float leftSpawnMax = -6f;

//     public float rightSpawnMin = 6f;
//     public float rightSpawnMax = 9f;

//     [Header("Wave Settings")]
//     public int maxWaves = 6;

//     [Header("Enemy Count")]
//     public int startEnemies = 3;
//     public int enemiesIncreasePerWave = 2;

//     [Header("Difficulty Scaling")]
//     public float speedIncreasePerWave = 0.4f;
//     public int healthIncreasePerWave = 10;

//     [Header("Spawn Timing")]
//     public float minSpawnDelay = 0.3f;
//     public float maxSpawnDelay = 0.8f;

//     private int enemiesAlive;
//     private int currentWave = 1;

//     void Start()
//     {
//         StartCoroutine(WaveLoop());
//     }

//     IEnumerator WaveLoop()
//     {
//         while (currentWave <= maxWaves)
//         {
//             int enemiesThisWave = startEnemies + (currentWave * enemiesIncreasePerWave);

//             Debug.Log("Wave " + currentWave + " spawning " + enemiesThisWave);

//             yield return StartCoroutine(SpawnWave(enemiesThisWave));

//             yield return new WaitUntil(() => enemiesAlive <= 0);

//             currentWave++;

//             yield return new WaitForSeconds(2f);
//         }

//         Debug.Log("PLAYER WINS!");
//     }

//     IEnumerator SpawnWave(int enemyCount)
//     {
//         enemiesAlive = enemyCount;

//         for (int i = 0; i < enemyCount; i++)
//         {
//             float spawnX;

//             if (Random.value > 0.5f)
//                 spawnX = Random.Range(leftSpawnMin, leftSpawnMax);
//             else
//                 spawnX = Random.Range(rightSpawnMin, rightSpawnMax);

//             Vector2 spawnPos = new Vector2(spawnX, groundY);

//             GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//             // SCALE DIFFICULTY
//             Demogorgan enemyScript = enemy.GetComponent<Demogorgan>();
//             Damageable dmg = enemy.GetComponent<Damageable>();

//             if (enemyScript != null)
//                 enemyScript.walkSpeed += currentWave * speedIncreasePerWave;

//             if (dmg != null)
//             {
//                 dmg.MaxHealth += currentWave * healthIncreasePerWave;
//                 dmg.Health = dmg.MaxHealth;
//                 dmg.onDeath += OnEnemyDeath;
//             }

//             yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
//         }
//     }

//     void OnEnemyDeath()
//     {
//         enemiesAlive--;
//         Debug.Log("Enemy died. Remaining: " + enemiesAlive);
//     }
// }
// using UnityEngine;
// using System.Collections;

// public class wavespawner : MonoBehaviour
// {
//     [Header("Enemy")]
//     public GameObject enemyPrefab;
//     public float groundY = -2f;

//     [Header("Spawn Range")]
//     public float leftSpawnMin = -9f;
//     public float leftSpawnMax = -6f;

//     public float rightSpawnMin = 6f;
//     public float rightSpawnMax = 9f;

//     [Header("Wave Settings")]
//     public int maxWaves = 6;

//     [Header("Enemy Count")]
//     public int startEnemies = 3;
//     public int enemiesIncreasePerWave = 1;

//     [Header("Difficulty Scaling")]
//     public float baseSpeed = 3f;
//     public float speedDecreasePerWave = 0.12f;   // speed reduces slightly
//     public int healthIncreasePerWave = 3;        // enemies take more hits

//     [Header("Spawn Timing")]
//     public float minSpawnDelay = 0.4f;
//     public float maxSpawnDelay = 0.9f;

//     private int enemiesAlive;
//     private int currentWave = 1;

//     void Start()
//     {
//         StartCoroutine(WaveLoop());
//     }

//     IEnumerator WaveLoop()
//     {
//         while (currentWave <= maxWaves)
//         {
//             int enemiesThisWave = startEnemies + (currentWave * enemiesIncreasePerWave);

//             Debug.Log("Wave " + currentWave + " spawning " + enemiesThisWave);

//             yield return StartCoroutine(SpawnWave(enemiesThisWave));

//             yield return new WaitUntil(() => enemiesAlive <= 0);

//             currentWave++;

//             yield return new WaitForSeconds(2f);
//         }

//         Debug.Log("PLAYER WINS!");
//     }

//     IEnumerator SpawnWave(int enemyCount)
//     {
//         enemiesAlive = enemyCount;

//         for (int i = 0; i < enemyCount; i++)
//         {
//             float spawnX;

//             if (Random.value > 0.5f)
//                 spawnX = Random.Range(leftSpawnMin, leftSpawnMax);
//             else
//                 spawnX = Random.Range(rightSpawnMin, rightSpawnMax);

//             Vector2 spawnPos = new Vector2(spawnX, groundY);

//             GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//             Demogorgan enemyScript = enemy.GetComponent<Demogorgan>();
//             Damageable dmg = enemy.GetComponent<Damageable>();

//             // SPEED decreases slightly every wave
//             if (enemyScript != null)
//             {
//                 float newSpeed = baseSpeed - (currentWave * speedDecreasePerWave);
//                 enemyScript.walkSpeed = Mathf.Max(1.5f, newSpeed); // prevents enemies from becoming too slow
//             }

//             // HEALTH increases every wave
//             if (dmg != null)
//             {
//                 dmg.MaxHealth += currentWave * healthIncreasePerWave;
//                 dmg.Health = dmg.MaxHealth;
//                 dmg.onDeath += OnEnemyDeath;
//             }

//             yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
//         }
//     }

//     void OnEnemyDeath()
//     {
//         enemiesAlive--;
//     }
// }

// using System.Collections;
// using UnityEngine;

// public class WaveSpawner : MonoBehaviour
// {
//     [Header("Enemy")]
//     public GameObject enemyPrefab;

//     [Header("Spawn Area")]
//     public float minX = -8f;
//     public float maxX = 8f;
//     public float groundY = -2f;

//     [Header("Wave Settings")]
//     public int totalWaves = 6;
//     public float timeBetweenWaves = 4f;

//     [Header("Enemies Per Wave")]
//     public int baseEnemies = 3;
//     public int increasePerWave = 2;   // slightly faster increase

//     [Header("Enemy Difficulty Scaling")]
//     public float baseSpeed = 2f;
//     public float speedIncrease = 0.25f;  // slightly faster enemies

//     public int baseHealth = 2;
//     public int healthIncrease = 1;

//     int currentWave = 0;

//     void Start()
//     {
//         StartCoroutine(StartWaveSystem());
//     }

//     IEnumerator StartWaveSystem()
//     {
//         while (currentWave < totalWaves)
//         {
//             currentWave++;

//             int enemiesThisWave = baseEnemies + (currentWave * increasePerWave);

//             Debug.Log("Wave " + currentWave + " started with " + enemiesThisWave + " enemies");

//             for (int i = 0; i < enemiesThisWave; i++)
//             {
//                 SpawnEnemy();

//                 // later waves spawn enemies slightly faster
//                 float spawnDelay = Mathf.Lerp(1.2f, 0.5f, (float)currentWave / totalWaves);
//                 yield return new WaitForSeconds(Random.Range(spawnDelay * 0.7f, spawnDelay));
//             }

//             yield return new WaitForSeconds(timeBetweenWaves);
//         }

//         Debug.Log("All waves finished!");
//     }

//     void SpawnEnemy()
//     {
//         float randomX = Random.Range(minX, maxX) + Random.Range(-0.8f, 0.8f);
//         Vector2 spawnPos = new Vector2(randomX, groundY);

//         GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//         Demogorgan enemyMove = enemy.GetComponent<Demogorgan>();
//         Damageable dmg = enemy.GetComponent<Damageable>();

//         int newHealth = baseHealth + (currentWave * healthIncrease);

//         if (dmg != null)
//         {
//             dmg.MaxHealth = newHealth;
//         }

//         if (enemyMove != null)
//         {
//             float speed = baseSpeed + (currentWave * speedIncrease);

//             // higher health slightly slows enemy
//             speed -= newHealth * 0.08f;

//             enemyMove.walkSpeed = Mathf.Clamp(speed, 1.6f, 3.2f);
//         }
//     }
// }
// using System.Collections;
// using UnityEngine;

// public class WaveSpawner : MonoBehaviour
// {
//     [Header("Enemy")]
//     public GameObject enemyPrefab;

//     [Header("Spawn Area")]
//     public float minX = -8f;
//     public float maxX = 8f;
//     public float groundY = -2f;

//     [Header("Wave Settings")]
//     public int totalWaves = 6;
//     public float timeBetweenWaves = 4f;

//     [Header("Enemy Count")]
//     public int baseEnemies = 4;
//     public int waveIncrease = 2;

//     [Header("Difficulty")]
//     public float baseSpeed = 2f;
//     public float speedIncrease = 0.25f;

//     public int baseHealth = 2;
//     public int healthIncrease = 1;

//     int currentWave = 0;

//     void Start()
//     {
//         StartCoroutine(StartWaveSystem());
//     }

//     IEnumerator StartWaveSystem()
//     {
//         while (currentWave < totalWaves)
//         {
//             currentWave++;

//             // Random enemy count per wave
//             int minEnemies = baseEnemies + (currentWave * waveIncrease);
//             int maxEnemies = minEnemies + 3;

//             int enemiesThisWave = Random.Range(minEnemies, maxEnemies);

//             Debug.Log("Wave " + currentWave + " started with " + enemiesThisWave + " enemies");

//             for (int i = 0; i < enemiesThisWave; i++)
//             {
//                 SpawnEnemy();

//                 float spawnDelay = Mathf.Lerp(1.1f, 0.4f, (float)currentWave / totalWaves);
//                 yield return new WaitForSeconds(Random.Range(spawnDelay * 0.6f, spawnDelay));
//             }

//             yield return new WaitForSeconds(timeBetweenWaves);
//         }

//         Debug.Log("All waves finished!");
//     }

//     void SpawnEnemy()
//     {
//         float randomX = Random.Range(minX, maxX) + Random.Range(-1f, 1f);
//         Vector2 spawnPos = new Vector2(randomX, groundY);

//         GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//         Demogorgan enemyMove = enemy.GetComponent<Demogorgan>();
//         Damageable dmg = enemy.GetComponent<Damageable>();

//         int newHealth = baseHealth + (currentWave * healthIncrease);

//         if (dmg != null)
//         {
//             dmg.MaxHealth = newHealth;
//         }

//         if (enemyMove != null)
//         {
//             float speed = baseSpeed + (currentWave * speedIncrease);

//             // add slight randomness to speed
//             speed += Random.Range(-0.2f, 0.2f);

//             // more health slightly slows enemy
//             speed -= newHealth * 0.08f;

//             enemyMove.walkSpeed = Mathf.Clamp(speed, 1.7f, 3.3f);
//         }
//     }
// }using System.Collections;
using UnityEngine;
using System.Collections;

namespace Season2
{
    public class WaveSpawner : MonoBehaviour
    {
        [Header("Enemy")]
        public GameObject enemyPrefab;

        [Header("Player")]
        public Season2.Damageable player; // Season2 player only

        [Header("Spawn Area")]
        public float minX = -8f;
        public float maxX = 8f;
        public float groundY = -2f;

        [Header("Wave Settings")]
        public int totalWaves = 6;
        public float timeBetweenWaves = 4f;

        [Header("Enemy Count")]
        public int baseEnemies = 4;
        public int waveIncrease = 2;

        [Header("Difficulty")]
        public float baseSpeed = 2f;
        public float speedIncrease = 0.25f;

        public int baseHealth = 2;
        public int healthIncrease = 1;

        int currentWave = 0;
        bool playerDead = false;

        void Start()
        {
            StartCoroutine(StartWaveSystem());
        }

        IEnumerator StartWaveSystem()
        {
            while (currentWave < totalWaves && !playerDead)
            {
                if (player != null && player.IsAlive == false)
                {
                    playerDead = true;
                    Debug.Log("Player died. Stopping waves.");
                    yield break;
                }

                currentWave++;

                int minEnemies = baseEnemies + (currentWave * waveIncrease);
                int maxEnemies = minEnemies + 3;

                int enemiesThisWave = Random.Range(minEnemies, maxEnemies);

                Debug.Log("Wave " + currentWave + " started with " + enemiesThisWave + " enemies");

                for (int i = 0; i < enemiesThisWave; i++)
                {
                    if (player != null && player.IsAlive == false)
                    {
                        playerDead = true;
                        Debug.Log("Player died during wave.");
                        yield break;
                    }

                    SpawnEnemy();

                    float spawnDelay = Mathf.Lerp(1.1f, 0.4f, (float)currentWave / totalWaves);
                    yield return new WaitForSeconds(Random.Range(spawnDelay * 0.6f, spawnDelay));
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }

            if (!playerDead)
            {
                Debug.Log("All waves finished!");
            }
        }

        void SpawnEnemy()
        {
            float randomX = Random.Range(minX, maxX) + Random.Range(-1f, 1f);
            Vector2 spawnPos = new Vector2(randomX, groundY);

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            Season2.Demogorgan enemyMove = enemy.GetComponent<Season2.Demogorgan>();
            Season2.Damageable dmg = enemy.GetComponent<Season2.Damageable>();

            int newHealth = baseHealth + (currentWave * healthIncrease);

            if (dmg != null)
            {
                dmg.MaxHealth = newHealth;
            }

            if (enemyMove != null)
            {
                float speed = baseSpeed + (currentWave * speedIncrease);

                speed += Random.Range(-0.2f, 0.2f);
                speed -= newHealth * 0.08f;

                enemyMove.walkSpeed = Mathf.Clamp(speed, 1.7f, 3.3f);
            }
        }
    }
}