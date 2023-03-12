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
    [SerializeField][Range(0f,20f)] private float ofset ;   //camera hight /2  
    [SerializeField][Range(0f, 2f)] public float initTimer; //initial cd for death
    private float timer;                                    //timer for death
    
    public static Camera MainCamera;                        //the main camera 
    public static Vector3 ScreenDimensions;                 // screen dimensions variable

    public static GameObject Player;                               //player game object ref
    public GameObject Buttons;                              //in game menu buttons
    public GameObject PauseButton;
    public GameObject Tips;                                 //text tips in the start of the game
    public GameObject PlatformSpawner;                      //the spawner object refrence and also the refrence to the score
    

    private void Awake()
    {
        GameStarted = false;

        Time.timeScale = 0;        
        timer = initTimer;
        Gameover = false;
        MainCamera = Camera.main;
        ScreenDimensions = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Player = GameObject.Find("Player");

        Volume = BackGroundTheme.GetComponent<AudioSource>().volume;
        //Debug.Log(Volume);

        Tips.SetActive(true);
        PauseButton.SetActive(false);
    }
    
    
    public int HighScore;                                   //the high score 
    public TextMeshProUGUI H_scoreText;                     //the high score in text

    public static int TotalCoins;                           //the number of coins 
    public TextMeshProUGUI TotalCoinsText;                  //the number of coins in text
    private void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);     // update highscore on start
        H_scoreText.text = HighScore.ToString()+"m";
        TotalCoins = PlayerPrefs.GetInt("TotalCoins", 0);   //Update coin count on start
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
            PauseButton.SetActive(true);
        }
        if(GameStarted)
        {
            
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            KeepUpWithCamY();
            if (Player.transform.position.y < GameOverVal)
            {
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
        
    // go to main menu button 
    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //save coins and high score data
    public void SaveData()
    {
        if (PlatformSpawner.transform.position.y-5 > HighScore)
        {
            HighScore = (int)PlatformSpawner.transform.position.y-5;
            PlayerPrefs.SetInt("HighScore", HighScore);
            H_scoreText.text = HighScore.ToString();
        }
        PlayerPrefs.SetInt("TotalCoins", TotalCoins);
        TotalCoinsText.text = TotalCoins.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Buttons.SetActive(true);
    }

    public void ResumeGame()
    {
        if(!Gameover)
        {
            Time.timeScale = 1;
            Buttons.SetActive(false);
        }
        
    }


    //sound settings
    public AudioSource BackGroundTheme;
    public Button SoundButton;
    float Volume;
    public Sprite SoundOff;
    public Sprite SoundOn;
    public void ToggleSound()
    {
        if(BackGroundTheme.mute == false)
        {
            BackGroundTheme.mute = true;
            SoundButton.image.sprite = SoundOff;
        }
        else if (BackGroundTheme.mute == true)
        {
            BackGroundTheme.mute = false;
            SoundButton.image.sprite = SoundOn;
        }
    }
}


/*
 * bird animation on death
 * nampi jump animation
 * 
 * create a leader board
 * add sound effects
 * 
 */