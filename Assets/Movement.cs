using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public Rigidbody2D rb;  //player rigidbody 
    [SerializeField][Range(0f, 1000f)] private float JumpForce; 
    [SerializeField] private float MoveSpeed;
    //[SerializeField] private bool Grounded = false;
    [SerializeField] private bool GoingDown = false; // bool of the player is going down
    private float LastY; // last y position of the player
    private Vector3 ScreenDimensions;
    private float DirectX;// direction of the player on x axes
    //private bool Gameover = false;

    private void Awake()
    {
        
    }

    void Start()
    {
        LastY = transform.position.y;
        ScreenDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));        
    }

    //move right and left
    private void Move()
    {
        //get input
        DirectX = Input.GetAxis("Horizontal");

        //move
        rb.velocity = new Vector2(DirectX * MoveSpeed, rb.velocity.y);
    }

    

    void Update()
    {
        Move();
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
        

        //movement on pc
        DirectX = Input.acceleration.x * MoveSpeed * Time.deltaTime;
        transform.Translate(DirectX, 0f, 0f);

        //if not going down set object to istrigger
        if (!GoingDown)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }


        //move camera with player on y axis
        if (transform.position.y > Camera.main.transform.position.y )
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y , Camera.main.transform.position.z);
        }

        //move camer if player goes under -4 on y axis
        if (transform.position.y < Camera.main.transform.position.y - 4f)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y + 4f, Camera.main.transform.position.z);
            
        }

        
        //screen edge teleprort from left to right       
        if (transform.position.x > Camera.main.transform.position.x + (ScreenDimensions.x ))
        {
            transform.position = new Vector3(Camera.main.transform.position.x - (ScreenDimensions.x ) + 0.2f, transform.position.y, transform.position.z);
        }                        
        //screen edge teleport from right to left
        else if (transform.position.x < Camera.main.transform.position.x - (ScreenDimensions.x ))
        {
            transform.position = new Vector3(Camera.main.transform.position.x + (ScreenDimensions.x ) - 0.2f , transform.position.y, transform.position.z);
        }

        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //if colliding with platform normal jump
        if (other.gameObject.tag=="Ground" && GoingDown)
        {
            rb.AddForce(transform.up * JumpForce);
        }
        //if colliding with Bird normal jump
        if (other.gameObject.tag == "Bird" && GoingDown)
        {
            rb.AddForce(transform.up * JumpForce);
        }
        //if colliding with mushroom super jump
        if (other.gameObject.tag == "Mushroom" && GoingDown)
        {
            rb.AddForce(transform.up * JumpForce* 2);
        }
        //if colliding with sharp object die
        if (other.gameObject.tag == "Sharp" && GoingDown)
        {
            //Debug.Log("Dead");
            //Gameover= true;
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //Grounded = false;            
        }
       
    }


}
