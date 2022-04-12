using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float playerHealth, spawnTimer, spawnCooldown;
    private int enemyCount, randomChoice, randomEnemyCount;
    public bool gameOver = false;
    private GameObject player;
    public GameObject[] enemies;
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
        randomChoice = Random.Range(1, 10);
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
            randomChoice = Random.Range(1, 10);
            Debug.Log(randomChoice);
        }
    }

    void SpawnEnemy1()
    {
        float enemyWidth = enemies[0].GetComponent<SpriteRenderer>().bounds.size.x / 2;

        Vector3 enemyPos = new Vector3(screenBounds.x + enemyWidth, screenBounds.y / 2);
        Quaternion enemyRot = enemies[0].transform.rotation;

        Instantiate<GameObject>(enemies[0], enemyPos, enemyRot);
        EnemyCountAndSpawn();
    }

    void SpawnEnemy2()
    {
        float enemyHeight = enemies[1].GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Vector3 enemyPos = new Vector3(Mathf.Floor(Random.Range((screenBounds.x / 2) * -1 , screenBounds.x / 2)), screenBounds.y + enemyHeight);
        Quaternion enemyRot = enemies[1].transform.rotation;

        Instantiate<GameObject>(enemies[1], enemyPos, enemyRot);
        EnemyCountAndSpawn();
    }

    void SpawnEnemy3()
    {
        float enemyWidth = enemies[2].GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Vector3 enemyPos = new Vector3((screenBounds.x * -1) - enemyWidth, screenBounds.y / 2);
        Quaternion enemyRot = enemies[2].transform.rotation;

        Instantiate<GameObject>(enemies[2], enemyPos, enemyRot);
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
                if (randomChoice % 2 == 0)
                    randomChoice = 2;
                if (randomChoice % 3 == 0)
                    randomChoice = 3;
                switch (randomChoice)
                {
                    case 1:
                        SpawnEnemy2();
                        return;

                    case 2:
                        SpawnEnemy1();
                        return;

                    case 3:
                        SpawnEnemy3();
                        return;

                    default:
                        SpawnEnemy2();
                        return;
                }
            }

            
        }

    }
}
