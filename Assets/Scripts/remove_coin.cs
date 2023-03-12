using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove_coin : MonoBehaviour
{
    public Animator anim;
    public Collider2D CoinCollider;
    public AudioClip coincollect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetTrigger("picked");
            GameManager.TotalCoins++;
            CoinCollider.enabled = false;
            //play coin collect sound
            AudioSource.PlayClipAtPoint(coincollect, transform.position);


        }
    }
}
