using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersDetonate : MonoBehaviour
{
    private PlayersSpawnManager _spawnManager;
    private bool readyToExplode = false;
    [SerializeField] private List<GameObject> explodingTile;
    void Start()
    {
        _spawnManager = GetComponent<PlayersSpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (explodingTile.Count > 0)
        {
            for (int i = 0; i < _spawnManager.playersSpawned; i++)
            {
                {
                    if (!_spawnManager.playersPrefabs[i].GetComponent<PlayerManager>().isHoldingDetonateTrigger)
                    {
                        readyToExplode = false;
                    }
                }
                if (readyToExplode)
                {
                    foreach (var tile in explodingTile)
                    {
                        tile.GetComponent<explode>().explodeTile();
                    }
                }
            }
            
        }

        
        
    }
}
