using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Platforms;
    private Vector3 SpawnerPos;
    [SerializeField] public GameObject spawner;

    [SerializeField][Range(0f, 1f)] private float factorial = 1f;
    float Score = 0;                                    //score
    float weight;                                       //the value of possibility of getting each platform
    float randomChance;                                            //random number betwwen 0 and 100 
    int platformNumber = 0;                                          //future used platfrom index
    float stdPlatformChance = 100;                                    //max value of the possibility of getting a standard platform 
    int lastPlatformNumber = 1;
    public GameObject Coin;

    private void Start()
    {
        SpawnerPos = transform.position;
    }
    void Update()
    {
        spawning();
        SpawnClouds();
    }

    bool cloudSpawned = false;
    public GameObject[] Clouds;
    int spawnCd = 0;
    int spawnCounter = 0;
    public void SpawnClouds()
    {
            
        float cloudSpawnRandomizer = Random.Range(0f, 100f);
        if (cloudSpawnRandomizer < 10 && !cloudSpawned) 
        {
            float randomXPosition = Random.Range(-GameManager.ScreenDimensions.x + 0.4f, GameManager.ScreenDimensions.x - 0.4f);
            Instantiate(Clouds[Random.Range(0, Clouds.Length)], new Vector3(randomXPosition, transform.position.y + Random.Range(0.2f,2f), 0), Quaternion.identity);
            cloudSpawned = true;
        }
    }


    private void spawning()
    {
        Score = spawner.transform.position.y + 3f;

        //if position is less than camera position + 5 spawn a platform 
        if (transform.position.y < GameManager.MainCamera.transform.position.y + 5f)
        {
            // get a random x position between screen borders
            float randomXPosition = Random.Range(-GameManager.ScreenDimensions.x + 0.4f, GameManager.ScreenDimensions.x - 0.4f);

            //get a random weighted platform index
            if (stdPlatformChance >= 50)//decrece standard platform chance and increase other platforms chnaces with weight
            {
                weight = (Score) / 4;// * factorial) / 4;   //4 is the number of available platforms
                stdPlatformChance = 100 - Score;// * factorial;
            }
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

            //spawn a random platform[platformNumber] from Platforms at random x position
            Instantiate(Platforms[platformNumber], new Vector3(randomXPosition, transform.position.y, 0), Quaternion.identity);

            //choose if to spawn a coin with platform or not
            int SpawnCoin = Random.Range(0, 10);
            if (SpawnCoin == 1)
            {
                Instantiate(Coin, new Vector3(randomXPosition, transform.position.y + 0.5f, 0), Quaternion.identity);
            }

            //move the spawner up            
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            spawnCd++;
        }
        if(spawnCd>=spawnCounter)
        {
            cloudSpawned = false;
            spawnCd = 0;
            spawnCounter = Random.Range(2, 5);
        }
    }
}
