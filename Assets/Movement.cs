using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField][Range(0f, 1000f)] private float JumpForce;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private bool Grounded = false;
    public Rigidbody2D rb;
    private float LastY;
    private bool GoingDown = false;

    private float DirectX;
    
    // Start is called before the first frame update
    void Start()
    {
        LastY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        //check if player is going down
        if (transform.position.y < LastY)
        {
            GoingDown = true;
        }
        else
        {
            GoingDown = false;
        }
        LastY = transform.position.y;


        // movement
        DirectX = Input.acceleration.x * MoveSpeed * Time.deltaTime;
        transform.Translate(DirectX, 0f, 0f);



        //move camera with player on y axis
        if (transform.position.y > Camera.main.transform.position.y)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        }

        //move camer if player goes under -4 on y axis
        if (transform.position.y < Camera.main.transform.position.y - 3f)
        {
            //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.Translate(0f, rb.velocity.y * Time.deltaTime, 0f);
        }





        //screen edge teleprort
        if (gameObject.transform.position.x >= Camera.main.transform.position.x + (Camera.main.scaledPixelWidth / 2)) 
        {
            transform.position = new Vector3(Camera.main.transform.position.x - (Camera.main.scaledPixelWidth / 2), transform.position.y, transform.position.z);
        }
        if (gameObject.transform.position.x <= Camera.main.transform.position.x - (Camera.main.scaledPixelWidth / 2))
        {
            transform.position = new Vector3(Camera.main.transform.position.x + (Camera.main.scaledPixelWidth / 2), transform.position.y, transform.position.z);
        }

        //if grounded jump
        if (Grounded == true && GoingDown == true)        
        {            
            Grounded = false;
            rb.AddForce(transform.up * JumpForce  );
            //rb.velocity = new Vector2(rb.velocity.x, JumpForce);

            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        rb.velocity = new Vector2(0f, 0f);
        if (other.gameObject.tag=="Ground")
        {
            Grounded = true;
            //Debug.Log("Grounded");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Grounded = false;
            //Debug.Log("exit");
        }
    }


}
