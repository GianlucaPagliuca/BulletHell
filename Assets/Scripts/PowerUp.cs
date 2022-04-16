using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float movementSpeed = 2;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float playerHealth = collision.gameObject.GetComponent<PlayerController>().health;
            switch (this.gameObject.tag)
            {
                case "1Up":
                    if(playerHealth < 5)
                    {
                        collision.gameObject.GetComponent<PlayerController>().health += 1;
                        Destroy(this.gameObject);
                    }
                    break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0, 1, 0);
        movement *= Time.deltaTime * movementSpeed;
        transform.Translate(movement);
    }
}
