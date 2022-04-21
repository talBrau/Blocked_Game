using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject Myplayer;
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == Myplayer.name)
        {
            Myplayer.GetComponent<PlayerManager>().IsStandingOnButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name ==  Myplayer.name)
        {
            Myplayer.GetComponent<PlayerManager>().IsStandingOnButton = false;
        }    
    }
}
