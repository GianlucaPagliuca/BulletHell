using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float movementSpeed = 2;
    private AudioSource audioSource;
    private AudioSource[] audioSources;
    public AudioClip powerUpPickUp;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    
    void Start()
    {
        screenBounds = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>().screenBounds;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            if (audio.name == "Audio Source")
            {
                audioSource = audio;
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            float playerHealth = collision.gameObject.GetComponent<PlayerController>().health;
            switch (this.gameObject.tag)
            {
                case "1Up":
                    if(playerHealth < 5 && playerHealth != 0)
                    {
                        collision.gameObject.GetComponent<PlayerController>().health += 1;
                        audioSource.clip = powerUpPickUp;
                        audioSource.Play();
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

        if (transform.position.y <= (screenBounds.y * -1) - this.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2)
        {
            Destroy(this.gameObject);
        }
    }
}
