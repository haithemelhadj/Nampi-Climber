using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlatform : MonoBehaviour
{
    void Update()
    {
        //if position on y axis is less than  camera position -5 destroy the platform
        if (transform.position.y < GameManager.MainCamera.transform.position.y - (GameManager.ScreenDimensions.y))
        {
            Destroy(gameObject);
        }
    }
}
