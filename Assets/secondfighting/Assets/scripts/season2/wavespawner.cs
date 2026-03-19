// using System.Collections;
// using UnityEngine;
// using TMPro;

// namespace Season2
// {
//     public class WaveSpawner : MonoBehaviour
//     {
//         [Header("Enemy")]
//         public GameObject enemyPrefab;

//         [Header("Player")]
//         public Season2.Damageable player;

//         [Header("Game Manager")]
//         public GameManager gameManager;

//         [Header("Wave UI")]
//         public WaveUIController waveAnnouncement; // center fading text
//         public TextMeshProUGUI waveText;          // top permanent text

//         [Header("Spawn Area")]
//         public float minX = -8f;
//         public float maxX = 8f;
//         public float groundY = -2f;

//         [Header("Wave Settings")]
//         public int totalWaves = 6;
//         public float timeBetweenWaves = 4f;

//         [Header("Enemy Count")]
//         public int baseEnemies = 4;
//         public int waveIncrease = 2;

//         [Header("Difficulty")]
//         public float baseSpeed = 2f;
//         public float speedIncrease = 0.25f;

//         public int baseHealth = 2;
//         public int healthIncrease = 1;

//         int currentWave = 0;
//         bool playerDead = false;

//         void Start()
//         {
//             StartCoroutine(StartWaveSystem());
//         }

//         IEnumerator StartWaveSystem()
//         {
//             while (currentWave < totalWaves && !playerDead)
//             {
//                 if (player != null && player.IsAlive == false)
//                 {
//                     playerDead = true;
//                     Debug.Log("Player died. Stopping waves.");
//                     yield break;
//                 }

//                 currentWave++;

//                 // Update the top wave text
//                 if (waveText != null)
//                 {
//                     waveText.text = "Wave " + currentWave;
//                 }

//                 // Show center announcement
//                 if (waveAnnouncement != null)
//                 {
//                     waveAnnouncement.ShowWave(currentWave);
//                 }

//                 // Small delay before enemies spawn
//                 yield return new WaitForSeconds(1.5f);

//                 int minEnemies = baseEnemies + (currentWave * waveIncrease);
//                 int maxEnemies = minEnemies + 3;

//                 int enemiesThisWave = Random.Range(minEnemies, maxEnemies);

//                 Debug.Log("Wave " + currentWave + " started with " + enemiesThisWave + " enemies");

//                 for (int i = 0; i < enemiesThisWave; i++)
//                 {
//                     if (player != null && player.IsAlive == false)
//                     {
//                         playerDead = true;
//                         Debug.Log("Player died during wave.");
//                         yield break;
//                     }

//                     SpawnEnemy();

//                     float spawnDelay = Mathf.Lerp(1.1f, 0.4f, (float)currentWave / totalWaves);
//                     yield return new WaitForSeconds(Random.Range(spawnDelay * 0.6f, spawnDelay));
//                 }

//                 yield return new WaitForSeconds(timeBetweenWaves);
//             }

//             // ALL WAVES FINISHED
//             if (!playerDead)
//             {
//                 Debug.Log("All waves finished! Player wins!");

//                 if (gameManager != null)
//                 {
//                     gameManager.victory();
//                 }
//             }
//         }

//         void SpawnEnemy()
//         {
//             float randomX = Random.Range(minX, maxX) + Random.Range(-1f, 1f);
//             Vector2 spawnPos = new Vector2(randomX, groundY);

//             GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

//             Season2.Demogorgan enemyMove = enemy.GetComponent<Season2.Demogorgan>();
//             Season2.Damageable dmg = enemy.GetComponent<Season2.Damageable>();

//             int newHealth = baseHealth + (currentWave * healthIncrease);

//             if (dmg != null)
//             {
//                 dmg.MaxHealth = newHealth;
//             }

//             if (enemyMove != null)
//             {
//                 float speed = baseSpeed + (currentWave * speedIncrease);

//                 speed += Random.Range(-0.2f, 0.2f);
//                 speed -= newHealth * 0.08f;

//                 enemyMove.walkSpeed = Mathf.Clamp(speed, 1.7f, 3.3f);
//             }
//         }
//     }
// }
using System.Collections;
using UnityEngine;
using TMPro;

namespace Season2
{
    public class WaveSpawner : MonoBehaviour
    {
        [Header("Enemy")]
        public GameObject enemyPrefab;

        [Header("Player")]
        public Season2.Damageable player;

        [Header("Game Manager")]
        public GameManager gameManager;

        [Header("Wave UI")]
        public WaveUIController waveAnnouncement;
        public TextMeshProUGUI waveText;

        [Header("Spawn Points")]
        public Transform leftSpawn;
        public Transform rightSpawn;

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

                if (waveText != null)
                {
                    waveText.text = "Wave " + currentWave;
                }

                if (waveAnnouncement != null)
                {
                    waveAnnouncement.ShowWave(currentWave);
                }

                yield return new WaitForSeconds(1.5f);

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
                Debug.Log("All waves finished! Player wins!");

                if (gameManager != null)
                {
                    gameManager.victory();
                }
            }
        }

        void SpawnEnemy()
        {
            // Choose left or right spawn randomly
            Transform spawnPoint = Random.value < 0.5f ? leftSpawn : rightSpawn;

            // Small random offset so enemies don't overlap
            Vector3 spawnPos = spawnPoint.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);

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