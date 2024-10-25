using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
  
    public void LoadSceneWithName(string name)
    {
        SceneManager.LoadScene(sceneName:name);
    }


    //go next scene
    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    //quit game 
    public void QuitGame()
    {
        Application.Quit();
    }
}
