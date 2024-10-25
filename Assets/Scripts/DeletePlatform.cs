using UnityEngine;

public class DeletePlatform : MonoBehaviour
{
    public float distance;
    void Update()
    {
        //if position on y axis is less than  camera position -5 destroy the platform
        if (transform.position.y < GameManager.MainCamera.transform.position.y - (GameManager.ScreenDimensions.y + distance))
        {
            Destroy(gameObject);
        }
    }
}
