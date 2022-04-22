using System.Collections.Generic;
using UnityEngine;

public class PlayersButtons : MonoBehaviour
{
    private PlayersSpawnManager _spawnManager;
    private bool readyToExplode;
    private List<GameObject> readyEndGameList;
    private int readyCountDetonate;

    public List<GameObject> explodingTiles;
    void Start()
    {
        readyEndGameList = new List<GameObject>();
        _spawnManager = GetComponent<PlayersSpawnManager>();
    }
    
    public void IncreaseReadyDetonate()
    {
        readyCountDetonate++;
        if (readyCountDetonate >= (_spawnManager.playersSpawned/2))
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
    }
    
    public void addToList(GameObject explodingTile)
    {
        explodingTiles.Add(explodingTile);
    }
    
    public void IncreaseReadyEnd(GameObject player)
    {
        readyEndGameList.Add(player);
        if (readyEndGameList.Count == _spawnManager.playersSpawned)
        {
           GameManager.InvokeBale();
        }
    }
    
    public void DecreaseReadyEnd(GameObject player)
    {
        readyEndGameList.Remove(player);
    }

    public bool playerInReadyEnd(GameObject player)
    {
        return readyEndGameList.Contains(player);
    }


}
