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
    [SerializeField] private bool GoingDown = false;
    private Vector3 ScreenDimensions;
    private float DirectX;
    
    // Start is called before the first frame update
    void Start()
    {
        LastY = transform.position.y;
        ScreenDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));        
    }

    // Update is called once per frame
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

        // movement
        DirectX = Input.acceleration.x * MoveSpeed * Time.deltaTime;
        transform.Translate(DirectX, 0f, 0f);



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
            transform.position = new Vector3(Camera.main.transform.position.x - (ScreenDimensions.x ), transform.position.y, transform.position.z);
        }                
        
        //screen edge teleport from right to left
        else if (transform.position.x < Camera.main.transform.position.x - (ScreenDimensions.x ))
        {
            transform.position = new Vector3(Camera.main.transform.position.x + (ScreenDimensions.x ) , transform.position.y, transform.position.z);
        }

        


        //if grounded jump
        if (Grounded == true)// && GoingDown == true)        
        {            
            Grounded = false;
            rb.AddForce(transform.up * JumpForce  );            
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Ground")
        {
            Grounded = true;
            Debug.Log("Grounded");
        }
    }

        
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Grounded = false;            
        }
    }


}
