using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float playerHealth, spawnTimer, spawnCooldown, newAlphaValue, randomPowerupSpawn;
    private int enemyCount, randomChoice, randomEnemyCount;
    public bool gameOver = false;
    private GameObject player;
    public GameObject[] enemies, gameOverButtons, pauseButtons, powerUps;
    private bool enemySpawnCooldown, activateGameOverButtons, button1Set, button2Set;
    private Vector2 screenBounds;
    public Canvas gameOverCanvas;
    public GameObject pauseMenuText, pauseMenuPanel;
  
    public GameObject gameOverText, scoreText, healthText;
    public Image gameOverPanel;
    public int Score = 0;
    public bool mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
        player.transform.position = new Vector3(0, (screenBounds.y * -1) / 2, 1);
        RandomSpawnTimer();
        enemyCount = 0;
        enemySpawnCooldown = false;
        spawnCooldown = 10.0f;
        randomEnemyCount = Random.Range(5, 10);
        randomChoice = Random.Range(1, 10);
        gameOverText.SetActive(false);
        pauseMenuText.transform.position = new Vector3(0, screenBounds.y / 2, -2);
        newAlphaValue = 0.0f;
        gameOverPanel.color = new Color(0, 0, 0, 0);
        activateGameOverButtons = false;
        button1Set = false;
        button2Set = false;
        randomPowerupSpawn = Random.Range(1, 20);
        scoreText.transform.position = new Vector3(screenBounds.x * -1 + 2, screenBounds.y * -1 + 1, -2);
        healthText.transform.position = new Vector3(screenBounds.x - 2, screenBounds.y * -1 + 1, -2);
        foreach (GameObject button in gameOverButtons){
            button.SetActive(false);

        }
        pauseButtons[0].transform.position = new Vector3(0, 0, -2);
        pauseButtons[1].transform.position = new Vector3(0, screenBounds.y * -1 / 2, -2);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    void SetGameOverScreen()
    {
        float alphaValue = gameOverText.GetComponent<TextMeshProUGUI>().color.a;

        if (!activateGameOverButtons)
        {
            gameOverText.SetActive(true);
            gameOverText.GetComponent<TextMeshProUGUI>().color = new Color(191, 0, 0, alphaValue >= 1 ? 1 : newAlphaValue);
            gameOverPanel.color = new Color(0, 0, 0, alphaValue >= 1 ? 1 : newAlphaValue);
            newAlphaValue += Time.deltaTime * 0.3f ;

            activateGameOverButtons = alphaValue >= 1 ? true : false;
        }
        else
        {
            Vector3 movement = new Vector3(0, 1, 0);
            movement *= Time.deltaTime * 1;
            gameOverText.transform.Translate(movement);

            if(gameOverText.transform.position.y >= screenBounds.y / 2)
            {
                gameOverText.transform.position = new Vector3(gameOverText.transform.position.x, screenBounds.y / 2, gameOverText.transform.position.z);

                if (!button1Set)
                {
                    gameOverButtons[0].transform.position = new Vector3(0, 0, 0);
                    gameOverButtons[0].SetActive(true);
                    button1Set = true;
                }

                if(button1Set && !button2Set)
                {
                    gameOverButtons[1].transform.position = new Vector3(0, (screenBounds.y * -1) / 2, 0);
                    gameOverButtons[1].SetActive(true);
                    button2Set = true;
                }
            }
        }
    }

    void SpawnPowerup()
    {
        int randomPowerUp = Random.Range(powerUps.GetLowerBound(0), powerUps.GetUpperBound(0));
        Vector2 powerUpSize = powerUps[randomPowerUp].GetComponent<SpriteRenderer>().bounds.size;
        float randomX = Random.Range(screenBounds.x * -1 + powerUpSize.x, screenBounds.x - powerUpSize.x);
        Vector3 powerUpPosition = new Vector3(randomX, screenBounds.y + 2, 0);
        Quaternion powerUpRot = new Quaternion(0, 0, 90, 0);
        
        Instantiate<GameObject>(powerUps[randomPowerUp], powerUpPosition, powerUpRot);
    }

    public void ResumeGame()
    {

        pauseMenuPanel.SetActive(false);
        mainMenu = false;
        Time.timeScale = 1;
        gameOverPanel.color = new Color(0, 0, 0, 0);

    }



    void SetMainMenu(bool menuActive)
    {

        Color panelColor = new Color(0, 0, 0, 1);


        pauseMenuPanel.SetActive(menuActive);
        if (menuActive )
        {

            gameOverPanel.color = panelColor;


        }
        else
        {
            panelColor.a = 0;
            gameOverPanel.color = panelColor;
        }





    }
    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<PlayerController>().health;
        healthText.GetComponent<Text>().text = "Health: " + Mathf.CeilToInt(playerHealth).ToString();
        if(playerHealth <= 0)
        {
            gameOver = true;

            SetGameOverScreen();
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
            if (spawnTimer <= 0 && !enemySpawnCooldown)
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
            randomPowerupSpawn -= 1 * Time.deltaTime;
            if (randomPowerupSpawn <= 0)
            {
                SpawnPowerup();
                randomPowerupSpawn = Random.Range(1, 20);

            }

            bool menu = Input.GetKeyDown("escape");

            if (Input.GetKeyDown("escape"))
            {
              
                if (mainMenu) 
                    {
                    mainMenu = false;                    
                    Time.timeScale = 1;
                    
                    
                }
                else
                {
                    mainMenu = true;
                    Time.timeScale = 0;
                    
                    
                }
                SetMainMenu(mainMenu);
                
            }
         
        }

    }

}
