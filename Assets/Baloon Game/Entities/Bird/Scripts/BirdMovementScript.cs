using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovementScript : MonoBehaviour
{
    private bool Down = false;
    
    private int random;
    public float speed = 1f;
    private Vector2 ScreenDimensions;
    private Vector2 destination;
    void Start()
    {
        ScreenDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        random = Random.Range(0, 2);
        if (random == 0)
        {
            random = -1;
            flip();
        }
    }

    void Update()
    {
        if(!Down)
        {
            destination = new Vector2(Camera.main.transform.position.x + (ScreenDimensions.x * random), transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, destination, speed*Time.deltaTime);
            if (transform.position.x == destination.x)
            {
                random*=-1;
                flip();
            }
        }
        /*
        if (!Down)
        {
            // continusly move left and right between screen borders           
            if (transform.position.x >= GameManager.ScreenDimensions.x - 0.1f)
            {
                random *= -1;
                flip();

            }
            else if (transform.position.x <= -GameManager.ScreenDimensions.x + 0.1f)
            {
                random *= -1;
                flip();
            }
            else
            {
                Debug.Log("another bird bug idk");
            }
            transform.Translate(random * speed * Time.deltaTime, 0f, 0f);
        }
        /**/
        else
        {
            // move down
            transform.position = new Vector3(transform.position.x, transform.position.y - 2f * Time.deltaTime, transform.position.z);
        }
    }

    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Down = true;
        }
    }
}
