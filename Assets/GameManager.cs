using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool Gameover = false;
    public GameObject Buttons;
    public GameObject Player;
    public float GameOverVal = 6f;
    [SerializeField] [Range(0f,20f)] private float ofset ;  // camera hight /2  
    [SerializeField][Range(0f, 2f)] public float initTimer; // initial cd for death
    private float timer; // timer for death
    private bool GameStarted; 

    private void Awake()
    {
        GameStarted = false;

        Time.timeScale = 0;//=0
        
        timer = initTimer;
        Gameover = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        //start game after player touch screen
        if (!GameStarted && Input.touchCount > 0)
        {
            GameStarted = true;
            Time.timeScale = 1;
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
            GameOverVal = Camera.main.transform.position.y-5f;
        }
    }


    //reset level function
    public void ResetLevel()
    {
        //reset level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ShowOptionsMenu()
    {
        //show options menu
        Buttons.SetActive(false);
        //OptionsMenu.SetActive(true);
    }
    

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    


    //slow down time
    /*
    if (Time.timeScale >= 0.2f)
            {
                Time.timeScale -= 0.2f;
            }
            else
            {
                Time.timeScale = 0;
            } 
    */
}


