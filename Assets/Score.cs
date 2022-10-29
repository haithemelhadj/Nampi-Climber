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
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ((spawner.transform.position.y)+3).ToString("");
    }
}
