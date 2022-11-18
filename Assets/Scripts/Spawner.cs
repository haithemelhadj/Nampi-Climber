using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Platforms;

    [SerializeField] private GameObject spawner;

    [SerializeField][Range(0f, 1f)] private float factorial;
    float Score = 0;//score
    float weight;//the value of possibility of getting each platform
    float x;   //random number betwwen 0 and 100 
    int n = 0;//future used platfrom index
    float std = 100;//max value of the possibility of getting a standard platform 
    int save=1;
    public GameObject Coin;
    
    void Update()
    {

        Score = spawner.transform.position.y + 3f;
        
        //if position is less than camera position + 5 spawn a platform 
        if (transform.position.y < Camera.main.transform.position.y + 5f)
        {
            // get a random x position between screen borders
            float RandomX = Random.Range(-Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 0.4f, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - 0.4f);
           
            //get a random weighted platform index
            if (std >= 50)
            {
                weight = (Score * factorial) / 4;
                std = 100 - Score * factorial;
            }
                x = Random.Range(0, 100);

            //Debug.Log("std="+std);//49
            //Debug.Log("wei="+weight);//12.75

            if (x >= 0 && x < std)
            {
                n = 0;
            }
            if (x >= std && x < std + weight)
            {
                n = 1;
            }
            if (x >= std + weight && x < std + weight * 2)
            {
                n = 2;
            }
            if (x >= std + weight * 2 && x < std + weight * 3)
            {
                n = 3;
            }
            if (x >= std + weight * 3 && x < std + weight * 4)
            {
                n = 4;
            }
            
            if (n == save)
            {
                n ++;
                if (n > 4)
                {
                    n = 1;
                }
            }

            if (n ==1 || n == 2 || n == 3 || n ==4 )
            {
                save = n;
            }

            int SpawnCoin = Random.Range(0, 10);
            
            // n is the index of the chosen platform
            //Debug.Log(weight);
            //Debug.Log(std);

            //spawn a random platform[n] from Platforms at random x position
            Instantiate(Platforms[n], new Vector3(RandomX, transform.position.y, 0), Quaternion.identity);
            if (SpawnCoin == 1)
            {
                Instantiate(Coin, new Vector3(RandomX, transform.position.y + 0.5f, 0), Quaternion.identity);
            }
            //Instantiate(Platforms[Random.Range(0, Platforms.Length)], new Vector3(RandomX, transform.position.y, 0), Quaternion.identity);

            //move the spawner up
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        }
    }
}
