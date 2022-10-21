using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    private bool Down = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Down)
        {
            // continusly move left and right between screen borders
            transform.position = new Vector3(Mathf.PingPong(Time.time * 2, 4) - 2, transform.position.y, transform.position.z);
        }
        else
        {
            // move down
            transform.position = new Vector3(transform.position.x, transform.position.y - 2f * Time.deltaTime, transform.position.z);
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