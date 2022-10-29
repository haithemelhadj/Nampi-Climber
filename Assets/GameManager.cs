using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject Player;
    public float GameOverVal = -6f;
    public static bool Gameover = false;
    [SerializeField] [Range(0f,20f)] private float ofset = 0f;


    [SerializeField][Range(0f, 2f)] public float initTimer;
    private float timer;

    private void Awake()
    {
        Time.timeScale = 1;
        timer = initTimer;
        Gameover = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeepUpWithCamY();
        if(Player.transform.position.y  < GameOverVal )
        {
            Debug.Log("Game Over");
            Gameover = true;
        }

        if(Gameover)
        {
            if (timer > 0)
            {
                timer -= Time.fixedDeltaTime;
            }
            else
            {
                timer = initTimer;
                Buttons.SetActive(true);
                Time.timeScale -= 0.2f;
            }
        }
        
        


    }


    void KeepUpWithCamY()
    {
        // if the camera is higher than the gameover value, set the gameover value to the camera's y position
        if (Camera.main.transform.position.y - 5f >= GameOverVal)
        {
            GameOverVal = Camera.main.transform.position.y-ofset;
        }
    }


    //reset level function
    public void ResetLevel()
    {
        //reset level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}

