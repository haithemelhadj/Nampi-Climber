using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public float GameOverVal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeepUpWithCamY();
        //if()
    }


    void KeepUpWithCamY()
    {
        // if the camera is higher than the gameover value, set the gameover value to the camera's y position
        if (Camera.main.transform.position.y - 5f > GameOverVal)
        {
            GameOverVal = Camera.main.transform.position.y-5f;
        }
    }
}

