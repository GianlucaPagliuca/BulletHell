using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//get the enemies bullets to find where the players x value is and shoot a bullet going in that direction but make it keep going until the y is off screen
public class EnemyController : MonoBehaviour
{
    private GameObject enemy, player;
    private Vector3 destination;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
        destination = player.transform.position;
        destination.y = (screenBounds.y * -1) - (enemy.GetComponent<SpriteRenderer>().bounds.size.y / 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }else if(collision.gameObject.tag == "Player"){
            player.GetComponent<PlayerController>().health -= 1;
            Destroy(this.gameObject);
            Debug.Log(player.GetComponent<PlayerController>().health);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.transform.position != destination)
        {
            Vector3 enemyMovement = new Vector3(0, 0);

            if (enemy.transform.position.x != destination.x)
            {
                enemyMovement.x = enemy.transform.position.x > destination.x ? -1 : 1;
            }

            if(enemy.transform.position.y != destination.y)
            {
                enemyMovement.y = enemy.transform.position.y > destination.y ? -1 : 1;
            }

            if(enemy.transform.position.y < destination.y)
            {
                Destroy(enemy);
            }

            enemyMovement *= Time.deltaTime;

            transform.Translate(enemyMovement);
        }
    }
}
