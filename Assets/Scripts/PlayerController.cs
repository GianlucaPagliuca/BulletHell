using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 20.0f)]
    public float movementSpeed = 5;
    [SerializeField]
    [Range(0.0f, 15.0f)]
    private float shootSpeed = 5;
    [SerializeField]
    public float health = 5.0f;
    private float bulletTimer = 0.0f;
    private bool bulletReady = true;
    private Vector2 screenBounds;
    private GameObject player, cam;
    private bool particleSpawned = false;
    
    public AudioClip shootSound;
    public AudioSource audioSource;
    public GameObject bullet;
    public GameObject hitParticle;


    private float playerWidth, playerHeight;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        playerWidth = player.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        playerHeight = player.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
        Mathf.Clamp(health, 0, 5);
    }

    void SetNewPos(Vector3 newPos)
    {
        transform.position = newPos;
    }

    void CheckForOutOfBounds()
    {
        Vector3 viewPos = transform.position;

        if (viewPos.x >= screenBounds.x - playerWidth)
        {
            SetNewPos(new Vector3(screenBounds.x - playerWidth, viewPos.y, viewPos.z));
        }else if(viewPos.x <= (screenBounds.x * -1) + playerWidth)
        {
            SetNewPos(new Vector3((screenBounds.x * -1) + playerWidth, viewPos.y, viewPos.z));
        }

        if(viewPos.y >= screenBounds.y - playerHeight)
        {
            SetNewPos(new Vector3(viewPos.x, screenBounds.y - playerHeight, viewPos.z));
        }else if(viewPos.y <= (screenBounds.y * -1) + playerHeight)
        {
            SetNewPos(new Vector3(viewPos.x, (screenBounds.y * -1) + playerHeight, viewPos.z));
        }

        if (viewPos.x >= screenBounds.x - playerWidth && viewPos.y >= screenBounds.y - playerHeight)
        {
            SetNewPos(new Vector3(screenBounds.x - playerWidth, screenBounds.y - playerHeight));
        } else if (viewPos.y <= (screenBounds.y * -1) + playerHeight && viewPos.x <= (screenBounds.x * -1) + playerWidth)
        {
            SetNewPos(new Vector3((screenBounds.x * -1) + playerWidth, (screenBounds.y * -1) + playerHeight));
        } else if (viewPos.y >= screenBounds.y - playerHeight && viewPos.x <= (screenBounds.x * -1) + playerWidth)
        {
            SetNewPos(new Vector3((screenBounds.x * -1) + playerWidth, screenBounds.y - playerHeight));
        }else if(viewPos.y <= (screenBounds.y * -1) + playerHeight && viewPos.x >= screenBounds.x - playerWidth)
        {
            SetNewPos(new Vector3(screenBounds.x - playerWidth, (screenBounds.y * -1) + playerHeight));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!cam.GetComponent<GameManager>().gameOver)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(movementSpeed * inputX, movementSpeed * inputY);

            movement *= Time.deltaTime;

            transform.Translate(movement);

            CheckForOutOfBounds();
        }

    }

    private void FixedUpdate()
    {
        Vector3 particlePos = new Vector3(player.transform.position.x, player.transform.position.y);
        Quaternion particleRotation = player.transform.rotation;
        if (!cam.GetComponent<GameManager>().gameOver)
        {
            float jump = Input.GetAxis("Jump");

            Vector3 bulletPos = new Vector3(player.transform.position.x, player.transform.position.y + 1f);
            Quaternion bulletRotation = player.transform.rotation;

            if (jump == 1 && bulletReady)
            {
                Instantiate<GameObject>(bullet, bulletPos, bulletRotation);
                audioSource.clip = shootSound;
                audioSource.Play();
                bulletReady = false;
            }

            if (!bulletReady)
            {
                bulletTimer += shootSpeed * Time.deltaTime;

                if (bulletTimer >= 1.0)
                {
                    bulletReady = true;
                    bulletTimer = 0.0f;
                }
            }
        }
       
       
           



        
    }

}
