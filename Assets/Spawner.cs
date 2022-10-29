using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Platforms;
    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        //if position is less than camera position + 5 spawn a platform 
        if (transform.position.y < Camera.main.transform.position.y + 5f)
        {
            // get a random x position between screen borders
            float RandomX = Random.Range(-Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 0.4f, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - 0.4f);

            //spawn a random platform from Platforms at random x position
            Instantiate(Platforms[Random.Range(0, Platforms.Length)], new Vector3(RandomX, transform.position.y, 0), Quaternion.identity);
            
            //move the spawner up
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        }
    }
}
