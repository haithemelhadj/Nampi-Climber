using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove_coin : MonoBehaviour
{
    public Animator anim;
    public Collider2D collider;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("collision");
            anim.SetTrigger("picked");
            GameManager.TotalCoins++;
            Debug.Log("added coin");
            collider.enabled = false;
            Debug.Log("collider off");
        }
    }
}
