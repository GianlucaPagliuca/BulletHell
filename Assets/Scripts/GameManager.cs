using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float playerHealth, spawnTimer;
    public bool gameOver = false;
    private GameObject player;
    public GameObject enemy;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
        RandomSpawnTimer();
    }

    void SpawnEnemy()
    {
        float randomX = Mathf.Floor(Random.Range(screenBounds.x * -1, screenBounds.x));
        float enemyHeight = enemy.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        Vector3 enemyPos = new Vector3(randomX, screenBounds.y + enemyHeight);
        Quaternion enemyRot = enemy.transform.rotation;

        Instantiate<GameObject>(enemy, enemyPos, enemyRot);
        RandomSpawnTimer();
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
            spawnTimer -= 1 * Time.deltaTime;
            if(spawnTimer <= 0)
            {
                SpawnEnemy();
            }
        }
    }
}
