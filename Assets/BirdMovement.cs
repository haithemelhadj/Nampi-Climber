using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    private bool Down = false;
    private int[] list = { -1, 1 };
    private int random;
    private float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        random = list[Random.Range(0, list.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if(!Down)
        {
            // continusly move left and right between screen borders
            //transform.position = new Vector3(list[Random.Range(0, list.Length)] * Mathf.PingPong(Time.time * 2, 4) - 2, transform.position.y, transform.position.z);
            //translate betwwen 2 x points
            if(transform.position.x >= Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - 0.4f)
            {
                random *= -1;

            }
            else if (transform.position.x <= -Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x+0.4f)
            {
                random *= -1;
            }

            transform.Translate(random * speed * Time.deltaTime, 0f, 0f);


        }
        else
        {
            //get random either 1 or -1
            
            
            // move down

            transform.position = new Vector3(transform.position.x , transform.position.y - 2f * Time.deltaTime, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Down = true;
        }
    }
}
/*
fix bird movement randomness
add death logic with gameobject

do score 
do game over
do restart
do menu
do pause
do sound
do music
do power ups
do animations



 */