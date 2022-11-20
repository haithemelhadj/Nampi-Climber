using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUsePlt : MonoBehaviour
{
    
    public BoxCollider2D col;



    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            //set to istrigger true
            col.isTrigger = true;
        }
    }
}
