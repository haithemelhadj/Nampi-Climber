using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public Rigidbody2D rb;                                                      //player rigidbody 
    [SerializeField][Range(0f, 1000f)] private float JumpForce; 
    [SerializeField] private float MoveSpeed;                                   // player movement speed
    //[SerializeField] private bool Grounded = false;
    [SerializeField] private bool GoingDown = false;                            // bool of the player is going down
    private float LastY;                                                        // last y position of the player
    //private Vector3 ScreenDimensions;                                           // screen dimensions variable
    private float DirectX;                                                      // direction of the player on x axes
    //public Camera Camera;
    public Collider2D Collider;
    public Animator animator;

    public GameObject Player;

    void Start()
    {
        LastY = transform.position.y;
        GameManager.ScreenDimensions = GameManager.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        //TpPos = new Vector3(GameManager.MainCamera.transform.position.x + (GameManager.ScreenDimensions.x) - 0.2f, transform.position.y, transform.position.z);
    }

    //move right and left
    private void Move()
    {
        //get input
        DirectX = Input.GetAxis("Horizontal");

        //move
        rb.velocity = new Vector2(DirectX * MoveSpeed, rb.velocity.y);

        //movement with tilting the phone
        DirectX = Input.acceleration.x * MoveSpeed * Time.deltaTime;
        
        transform.Translate(DirectX, 0f, 0f);




        //move Player with finger
        if (Input.touchCount > 0 || Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = (GameManager.MainCamera.ScreenToWorldPoint(touch.position));


            if (touchPosition.x > transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(touchPosition.x, transform.position.y), MoveSpeed * Time.deltaTime);
            }
            else if (touchPosition.x < transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(touchPosition.x, transform.position.y), MoveSpeed * Time.deltaTime);
            }
        }
    }

    

    void Update()
    {
        //check if player is going down
        if (transform.position.y < LastY)
        {
            GoingDown = true;
        }
        if (transform.position.y > LastY)
        {
            GoingDown = false;
        }
        LastY = transform.position.y;

        if (GameManager.Gameover == false)
        {
            Move();
            //flip();
        }

        



        //if not going down set object to istrigger
        // when going up collision is still detected 
        //trigger is to prevent animation start when going up
        if (!GoingDown)
        {
            Collider.isTrigger = true;
        }
        else
        {
            Collider.isTrigger = false;
        }
        

        //move camera with player on y axis
        if (transform.position.y > GameManager.MainCamera.transform.position.y )
        {
            GameManager.MainCamera.transform.position = new Vector3(GameManager.MainCamera.transform.position.x, transform.position.y , GameManager.MainCamera.transform.position.z);
        }

        //move camer if player goes under -4 on y axis
        if (transform.position.y < GameManager.MainCamera.transform.position.y - 4f)
        {
            GameManager.MainCamera.transform.position = new Vector3(GameManager.MainCamera.transform.position.x, transform.position.y + 4f, GameManager.MainCamera.transform.position.z);
            //GameManager.MainCamera.transform.Translate(Vector2.down*rb.velocity.y);
        }

        
        //screen edge teleprort from left to right       
        if (transform.position.x > GameManager.MainCamera.transform.position.x + (GameManager.ScreenDimensions.x ))
        {
            transform.position = new Vector3(GameManager.MainCamera.transform.position.x - (GameManager.ScreenDimensions.x ) + 0.2f, transform.position.y, transform.position.z);
        }                        
        //screen edge teleport from right to left
        else if (transform.position.x < GameManager.MainCamera.transform.position.x - (GameManager.ScreenDimensions.x ))
        {
            transform.position = new Vector3(GameManager.MainCamera.transform.position.x + (GameManager.ScreenDimensions.x ) - 0.2f , transform.position.y, transform.position.z);
            
        }

        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(GoingDown)
        {
            //if colliding with platform normal jump        
            if (other.gameObject.CompareTag("Ground"))
            {
                rb.AddForce(transform.up * JumpForce);
            }
            //if colliding with Bird normal jump
            if (other.gameObject.CompareTag("Bird"))
            {
                rb.AddForce(transform.up * JumpForce);
            }
            //if colliding with mushroom super jump
            if (other.gameObject.CompareTag("Mushroom"))
            {
                rb.AddForce(transform.up * JumpForce * 2);
            }
            //if colliding with sharp object gameover
            if (other.gameObject.CompareTag("Sharp"))
            {
                GameManager.Gameover = true;
                animator.SetTrigger("isDead");
                
            }
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            //coint count +1
            //GameManager.TotalCoins++;
            //GameManager.TotalCoinsText.text = GameManager.TotalCoins.ToString();
            

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
