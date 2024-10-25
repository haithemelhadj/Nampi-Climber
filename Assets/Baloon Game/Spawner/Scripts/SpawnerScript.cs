using System.Collections;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public PlayerInput playerScript;
    public GameObject[] platforms;
    //private Vector3 spawnerPos;
    //public GameObject spawner;

    //[SerializeField][Range(0f, 1f)] private float factorial = 1f;
    float Score = 0;                                    //score
    float weight;                                       //the value of possibility of getting each platform
    float randomChance;                                            //random number betwwen 0 and 100 
    int platformNumber = 0;                                          //future used platfrom index
    float stdPlatformChance = 100;                                    //max value of the possibility of getting a standard platform 
    int lastPlatformNumber = 1;

    //public GameObject[] platforms;
    private void Update()
    {
        Score = playerScript.Score;
        StartCoroutine(SpawnBubblesCorotine());
        if (transform.position.y < Camera.main.transform.position.y + 6f)
        {
            float randomXPosition = GetRandHorizPos();

            DecidePlatform();

            SpawnPlatform(randomXPosition);

            //move the spawner up            
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

        }
    }

    #region Platforms
    public void SpawnPlatform(float randomXPosition)
    {
        //spawn a random platform[platformNumber] from Platforms at random x position
        Instantiate(platforms[platformNumber], new Vector3(randomXPosition, transform.position.y, 0), Quaternion.identity);

    }

    public float GetRandHorizPos()
    {
        // get a random x position between screen borders
        return Random.Range(-GameManager.ScreenDimensions.x + 0.4f, GameManager.ScreenDimensions.x - 0.4f);

    }


    public float dificulty;
    public void SetDificulty()
    {
        //dificulty = Score * 0.1f;
        //get a random weighted platform index
        if (stdPlatformChance >= 50 && platforms.Length != 0)//decrece standard platform chance and increase other platforms chnaces with weight
        {
            weight = (Score) / platforms.Length;// * factorial) / 4;   
            stdPlatformChance = 100 - Score;// * factorial;
        }

    }

    public void DecidePlatform()
    {
        SetDificulty();
        randomChance = Random.Range(0, 100);
        // platformNumber is the index of the chosen platform
        if (randomChance >= 0 && randomChance < stdPlatformChance)
        {
            platformNumber = 0;
        }
        if (randomChance >= stdPlatformChance && randomChance < stdPlatformChance + weight)
        {
            platformNumber = 1;
        }
        if (randomChance >= stdPlatformChance + weight && randomChance < stdPlatformChance + weight * 2)
        {
            platformNumber = 2;
        }
        if (randomChance >= stdPlatformChance + weight * 2 && randomChance < stdPlatformChance + weight * 3)
        {
            platformNumber = 3;
        }
        if (randomChance >= stdPlatformChance + weight * 3 && randomChance < stdPlatformChance + weight * 4)
        {
            platformNumber = 4;
        }

        if (platformNumber == lastPlatformNumber)//can't repeat the same special platform twice
        {
            platformNumber++;
            if (platformNumber > 4)
            {
                platformNumber = 1;
            }
        }

        if (platformNumber != 0)
        {
            lastPlatformNumber = platformNumber;
        }
    }

    #endregion

    #region Bubbles
    public GameObject bubble;
    public IEnumerator SpawnBubblesCorotine()
    {
        SpawnBubbles();
        yield return new WaitForSeconds(Random.Range(0, 0.4f));
    }

    public void SpawnBubbles()
    {
        float randomXPosition = GetRandHorizPos();
        //choose if to spawn a coin with platform or not
        int SpawnCoin = Random.Range(0, 100);
        if (SpawnCoin == 1)
        {
            Instantiate(bubble, new Vector3(randomXPosition, transform.position.y + 0.5f, 0), Quaternion.identity);
        }
    }
    #endregion

}
