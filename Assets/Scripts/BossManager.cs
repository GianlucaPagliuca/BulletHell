using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private bool inPosition = false;
    private float movementSpeed = 1.0f;
    private GameObject cam;
    private Vector2 screenBounds;
    public int health = 15;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        this.GetComponent<SpriteRenderer>().color = Color.magenta;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPosition)
        {
            Vector3 movement = new Vector3(0, -1, 0);
            movement *= Time.deltaTime * movementSpeed;
            transform.Translate(movement);

            if(transform.position.y <= screenBounds.y / 2)
            {
                inPosition = true;
                Vector3 newMovement = new Vector3(Mathf.Cos(Time.time * 2) * 2, Mathf.Sin(Time.time * 5) * 5, 0);
                movement *= Time.deltaTime;
                transform.Translate(movement);
            }
        }


        if(health <= 0)
        {
            //Have to set the boss to invisible instead of dying so that the game can end.
            this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
    }
}
