using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private GameObject bullet;
    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float bulletSpeed;
    private GameObject player, enemy;
    // Start is called before the first frame update
    void Start()
    {
        bullet = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy2" || collision.gameObject.tag == "Enemy3")
        {
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        float baseSpeed = player.GetComponent<PlayerController>().movementSpeed;
        Vector3 bulletMovement = new Vector3(0, baseSpeed * bulletSpeed);

        bulletMovement *= Time.deltaTime;

        transform.Translate(bulletMovement);

        if(this.transform.position.y > 5.0f)
        {
            Destroy(bullet);
        }
    }
}
