using System.Collections.Generic;
using UnityEngine;

public class PlayersDetonate : MonoBehaviour
{
    private PlayersSpawnManager _spawnManager;
    private bool readyToExplode;
    private bool readyEndGame;
    private int readyCountDetonate;
    private int readyCountEnd;
    
    [SerializeField] private List<GameObject> explodingTiles;
    void Start()
    {
        _spawnManager = GetComponent<PlayersSpawnManager>();
    }
    
    public void IncreaseReadyDetonate()
    {
        readyCountDetonate++;
        print(readyCountDetonate);
        if (readyCountDetonate == _spawnManager.playersSpawned)
        {
            foreach (var tile in explodingTiles)
            {
                tile.GetComponent<explode>().explodeTile();
            }
            explodingTiles = new List<GameObject>();
        }
    }

    public void DecreaseReadyDetonate()
    {
        readyCountDetonate--;
        print(readyCountDetonate);
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

    public void addToList(GameObject explodingTile)
    {
        explodingTiles.Add(explodingTile);
    }
}
