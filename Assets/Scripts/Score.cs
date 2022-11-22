using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject spawner;
    [SerializeField]private TextMeshProUGUI scoreText;
    
    
    void Update()
    {
        if(((spawner.transform.position.y) - 5)>=0)
        {
            scoreText.text = ((spawner.transform.position.y) - 5).ToString("");
        }        
    }
}
