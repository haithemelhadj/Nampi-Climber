using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool Gameover = false;
    private bool GameStarted;                               //boolean to let the player start the game by pressing
    
    public float GameOverVal = 6f;                          //a float set to decide when the player loses
    [SerializeField] [Range(0f,20f)] private float ofset ;  //camera hight /2  
    [SerializeField][Range(0f, 2f)] public float initTimer; //initial cd for death
    private float timer;                                    //timer for death
    
    public Camera MainCamera;                               //the main camera 
    
    public GameObject Player;                               //player game object ref
    public GameObject Buttons;                              //in game menu buttons
    public GameObject Tips;                                 //text tips in the start of the game
    public GameObject Spawner;                              //the spawner object refrence and also the refrence to the score
    
    public int HighScore;                                   //the high score 
    public TextMeshProUGUI H_scoreText;                     //the high score in text

    public static int TotalCoins;                           //the number of coins 
    public TextMeshProUGUI TotalCoinsText;                  //the number of coins in text


    private void Awake()
    {
        GameStarted = false;

        Time.timeScale = 0;
        
        timer = initTimer;
        Gameover = false;
    }
    
    private void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        H_scoreText.text = HighScore.ToString();
        TotalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        TotalCoinsText.text = TotalCoins.ToString();
    }
    

    void Update()
    {
        SaveData();
        //start game after player touch screen
        if (!GameStarted && Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            GameStarted = true;
            Time.timeScale = 1;
            Tips.SetActive(false);
        }
        if(GameStarted)
        {
            
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            KeepUpWithCamY();
            if (Player.transform.position.y < GameOverVal)
            {
                //Debug.Log("Game Over");
                Gameover = true;
            }

            if (Gameover)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    timer = initTimer;
                    Time.timeScale = 0;
                    Buttons.SetActive(true);

                }
            }
        }        
    }


    void KeepUpWithCamY()
    {
        // if the camera is higher than the gameover value, set the gameover value to the camera's y position
        if (Camera.main.transform.position.y - 5f >= GameOverVal)
        {
            GameOverVal = MainCamera.transform.position.y-5f;
        }
    }


    //reset level function
    public void ResetLevel()
    {
        //reset level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    //show options menu button
    public void ShowOptionsMenu()
    {
        //show options menu
        Buttons.SetActive(false);
        //OptionsMenu.SetActive(true);
    }
    
    // go to main meni button 
    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //save coins and high score data
    public void SaveData()
    {
        if (Spawner.transform.position.y-5 > HighScore)
        {
            HighScore = (int)Spawner.transform.position.y-5;
            PlayerPrefs.SetInt("HighScore", HighScore);
            H_scoreText.text = HighScore.ToString();
        }
        PlayerPrefs.SetInt("TotalCoins", TotalCoins);
        TotalCoinsText.text = TotalCoins.ToString();



    }


}


