using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    [SerializeField]
    [Range(1,50)]
    public float bulletSpeed;
    private GameObject enemy;
    private Vector3 screenBounds;
    private float bulletWidth, enemyMovementSpeed;
    private GameObject player;
    bool particleSpawned = false;



    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        bulletWidth = this.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        player = GameObject.FindGameObjectWithTag("Player");


        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            this.GetComponent<SpriteRenderer>().color = enemy.GetComponent<SpriteRenderer>().color;
        }else if (GameObject.FindGameObjectWithTag("Enemy2") != null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy2");
            this.GetComponent<SpriteRenderer>().color = enemy.GetComponent<SpriteRenderer>().color;
        }
        else if (GameObject.FindGameObjectWithTag("Enemy3") != null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy3");
            this.GetComponent<SpriteRenderer>().color = enemy.GetComponent<SpriteRenderer>().color;
        }
        enemyMovementSpeed = enemy.GetComponent<EnemyController>().movementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().health -= 1;
            Destroy(this.gameObject);
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().health -= 1;
            KillParticles();
            Destroy(this.gameObject);

            
            



        }

    }
    void KillParticles()
    {
        Vector3 particlePos = new Vector3(player.transform.position.x, player.transform.position.y);
        Quaternion particleRotation = player.transform.rotation;
        
        
             Instantiate<GameObject>(player.GetComponent<PlayerController>().hitParticle, particlePos, particleRotation);
            particleSpawned = true;
        

        

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0, -1);
        movement *= Time.deltaTime * bulletSpeed * enemyMovementSpeed;
        transform.Translate(movement);

        if (transform.position.y <= (screenBounds.y * -1) - bulletWidth)
        {
            Destroy(this.gameObject);
        }

    }
}
