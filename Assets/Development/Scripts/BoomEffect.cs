using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    [SerializeField] private float liveTime = 3;

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
