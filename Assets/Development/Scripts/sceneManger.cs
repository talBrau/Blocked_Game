using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManger : MonoBehaviour
{
    private bool onBoarding;
    [SerializeField] private GameObject monsterManager;

    private void Awake()
    {
        onBoarding = true;
        monsterManager.SetActive(false);    }
    
    void Update()
    {
        if (onBoarding && Input.GetKey(KeyCode.Return))
        {
            monsterManager.SetActive(true);
            onBoarding = false;
        }
    }
}
