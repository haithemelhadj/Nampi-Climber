using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if position on y axis is less than  camera position -5 destroy the platform
        if (transform.position.y < GameManager.MainCamera.transform.position.y - 5)
        {
            Destroy(gameObject);
        }


    }
}
