using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersDetonate : MonoBehaviour
{
    private PlayersSpawnManager _spawnManager;
    private bool readyToExplode = false;
    private bool readyEndGame = false;
    private int readyCountDetonate = 0;
    private int readyCountEnd = 0;
    
    [SerializeField] private List<GameObject> explodingTile;
    void Start()
    {
        _spawnManager = GetComponent<PlayersSpawnManager>();
    }
    
    public void IncreaseReadyDetonate()
    {
        readyCountDetonate++;
        if (readyCountDetonate == _spawnManager.playersSpawned)
        {
            foreach (var tile in explodingTile)
            {
                tile.GetComponent<explode>().explodeTile();
            }
        }

        readyCountDetonate = 0;
    }
    public void IncreaseReadyEnd()
    {
        readyCountEnd++;
        if (readyCountEnd == _spawnManager.playersSpawned)
        {
           print("END GAME");
        }
        readyCountEnd = 0;
    }
}
