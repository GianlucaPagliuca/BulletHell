using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//get the enemies bullets to find where the players x value is and shoot a bullet going in that direction but make it keep going until the y is off screen
public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private GameObject cam;
    [SerializeField]
    [Range(1,50)]
    private float movementSpeed, randomWaveSpeed, randomWaveLength;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
        randomWaveSpeed = Mathf.Floor(Random.Range(1.0f, 8.0f));
        randomWaveLength = Mathf.Floor(Random.Range(1.0f, 5.0f));
        cam = GameObject.FindGameObjectWithTag("MainCamera");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {

            Destroy(collision.gameObject);
            cam.GetComponent<GameManager>().Score += 1 * Mathf.CeilToInt(movementSpeed);
            Debug.Log(cam.GetComponent<GameManager>().Score);

        }else if(collision.gameObject.tag == "Player"){
            player.GetComponent<PlayerController>().health -= 1;
            Destroy(this.gameObject);
            Debug.Log(player.GetComponent<PlayerController>().health);
        }
    }

    void Enemy1Movement()
    {
        Vector3 movement = new Vector3(-1, Mathf.Sin(Time.time * randomWaveSpeed) * randomWaveLength, 0);
        float enemyHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        movement *= Time.deltaTime * movementSpeed;
        transform.Translate(movement);

        if(transform.position.y >= screenBounds.y - enemyHeight)
        {
            transform.position = new Vector3(transform.position.x, screenBounds.y - enemyHeight, transform.position.z);
        }else if(transform.position.y <= (screenBounds.y * -1) + enemyHeight)
        {
            transform.position = new Vector3(transform.position.x, (screenBounds.y * -1) + enemyHeight, transform.position.z);
        }

        if (transform.position.x <= screenBounds.x * -1 - (gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2))
        {
            Destroy(this.gameObject);
        }
    }

    void Enemy2Movement()
    {
        Vector3 movement = new Vector3(Mathf.Cos(Time.time * randomWaveSpeed) * randomWaveLength, -1, 0);
        float enemyWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        movement *= Time.deltaTime;
        transform.Translate(movement);

        if(transform.position.x >= screenBounds.x - enemyWidth)
        {
            transform.position = new Vector3(screenBounds.x - enemyWidth, transform.position.y, transform.position.z);
        }else if(transform.position.x <= (screenBounds.x * -1) + enemyWidth)
        {
            transform.position = new Vector3((screenBounds.x * -1) + enemyWidth, transform.position.y, transform.position.z);
        }

        if (transform.position.y <= screenBounds.y * -1 - (gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2))
        {
            Destroy(this.gameObject);
        }
    }

    void Enemy3Movement()
    {
        Vector3 movement = new Vector3(+1, Mathf.Sin(Time.time * randomWaveSpeed) * randomWaveLength, 0);
        float enemyHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        movement *= Time.deltaTime * movementSpeed;
        transform.Translate(movement);

        if(transform.position.y >= screenBounds.y - enemyHeight)
        {
            transform.position = new Vector3(transform.position.x, screenBounds.y - enemyHeight, transform.position.z);
        }else if(transform.position.y <= (screenBounds.y * -1) + enemyHeight)
        {
            transform.position = new Vector3(transform.position.x, (screenBounds.y * -1) + enemyHeight, transform.position.z);
        }

        if(transform.position.x >= screenBounds.x + gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameObject.tag)
        {
            case "Enemy1":
                Enemy1Movement();
                break;
            case "Enemy2":
                Enemy2Movement();
                break;
            case "Enemy3":
                Enemy3Movement();
                break;
            default:
                Enemy1Movement();
                break;
        }
    }
}
