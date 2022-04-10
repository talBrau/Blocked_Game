using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTileScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Runner"))
            Destroy(gameObject);
    }
}
