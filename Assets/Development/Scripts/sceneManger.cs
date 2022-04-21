using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManger : MonoBehaviour
{
    private bool onBoarding;
    [SerializeField] private GameObject monsterManager;
    [SerializeField] private PlayersSpawnManager playersSpawnManager;
    private int _readyCounter;

    public void increaseReadyCounter()
    {
        _readyCounter += 1;
        if (_readyCounter == playersSpawnManager.playersSpawned)
        {
            onBoarding = false;
            monsterManager.SetActive(true);
        }

    }
    
    private void Awake()
    {
        onBoarding = true;
        monsterManager.SetActive(false);
        
    }
    
}
