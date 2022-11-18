using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove_coin : MonoBehaviour
{
    public Animator anim;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetTrigger("picked");
            GameManager.TotalCoins++;
        }
    }
}
