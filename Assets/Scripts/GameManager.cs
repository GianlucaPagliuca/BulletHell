using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float playerHealth, spawnTimer, spawnCooldown;
    private int enemyCount, randomChoice, randomEnemyCount;
    public bool gameOver = false;
    private GameObject player;
    public GameObject enemy, enemy2, enemy3;
    private bool enemySpawnCooldown;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
        RandomSpawnTimer();
        enemyCount = 0;
        enemySpawnCooldown = false;
        spawnCooldown = 10.0f;
        randomEnemyCount = Random.Range(5, 10);
        randomChoice = Random.Range(2, 4);
    }

    void EnemyCountAndSpawn()
    {
        enemyCount++;
        RandomSpawnTimer();

        if (enemyCount >= randomEnemyCount)
        {
            enemyCount = 0;
            randomEnemyCount = Random.Range(5, 10);
            enemySpawnCooldown = true;
            randomChoice = Random.Range(1, 3);
        }
    }

    void SpawnEnemy1()
    {
        float enemyWidth = enemy.GetComponent<SpriteRenderer>().bounds.size.x / 2;

        Vector3 enemyPos = new Vector3(screenBounds.x + enemyWidth, screenBounds.y / 2);
        Quaternion enemyRot = enemy.transform.rotation;

        Instantiate<GameObject>(enemy, enemyPos, enemyRot);
        EnemyCountAndSpawn();
    }

    void SpawnEnemy2()
    {
        float enemyHeight = enemy2.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Vector3 enemyPos = new Vector3(Mathf.Floor(Random.Range((screenBounds.x / 2) * -1 , screenBounds.x / 2)), screenBounds.y + enemyHeight);
        Quaternion enemyRot = enemy2.transform.rotation;

        Instantiate<GameObject>(enemy2, enemyPos, enemyRot);
        EnemyCountAndSpawn();
    }

    void SpawnEnemy3()
    {
        float enemyWidth = enemy3.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Vector3 enemyPos = new Vector3((screenBounds.x * -1) - enemyWidth, screenBounds.y / 2);
        Quaternion enemyRot = enemy.transform.rotation;

        Instantiate<GameObject>(enemy3, enemyPos, enemyRot);
        EnemyCountAndSpawn();
    }

    void RandomSpawnTimer()
    {
        spawnTimer = Mathf.Floor(Random.Range(1.0f, 5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<PlayerController>().health;
        if(playerHealth <= 0)
        {
            gameOver = true;
        }

        if (!gameOver)
        {
            if (enemySpawnCooldown)
            {
                spawnCooldown -= 1 * Time.deltaTime;

                if (spawnCooldown <= 0)
                {
                    enemySpawnCooldown = false;
                    spawnCooldown = 10.0f;
                }
            }

            spawnTimer -= 1 * Time.deltaTime;
            if(spawnTimer <= 0 && !enemySpawnCooldown)
            {
                switch (randomChoice)
                {
                    case 1:
                        SpawnEnemy1();
                        return;

                    case 2:
                        SpawnEnemy2();
                        return;

                    case 3:
                        SpawnEnemy3();
                        return;

                    default:
                        SpawnEnemy1();
                        return;
                }
            }

            
        }

    }
}
