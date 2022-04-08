using System;
using UnityEngine;

public class SwichingTags : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("It") && other.gameObject.CompareTag("Runner"))
        {
            // change other game object to "It" state
            other.gameObject.tag = "It";
            other.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        }
    }
}
