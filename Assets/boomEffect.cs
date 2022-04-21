using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomEffect : MonoBehaviour
{
    [SerializeField] private float liveTime = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
