using System.Collections.Generic;
using UnityEngine;

public class PlayersButtons : MonoBehaviour
{
    private PlayersSpawnManager _spawnManager;
    private List<GameObject> _readyEndGameList;
    private int _readyCountDetonate;

    public List<GameObject> explodingTiles;
    void Start()
    {
        _readyEndGameList = new List<GameObject>();
        _spawnManager = GetComponent<PlayersSpawnManager>();
    }
    
    public void IncreaseReadyDetonate()
    {
        _readyCountDetonate++;
        if (_readyCountDetonate >= (_spawnManager.playersSpawned/2))
        {
            foreach (var tile in explodingTiles)
            {
                tile.GetComponent<ExplodeScript>().explodeTile();
            }
            explodingTiles = new List<GameObject>();
        }
    }

    public void DecreaseReadyDetonate()
    {
        _readyCountDetonate--;
    }
    
    public void addToList(GameObject explodingTile)
    {
        explodingTiles.Add(explodingTile);
    }
    
    public void IncreaseReadyEnd(GameObject player)
    {
        _readyEndGameList.Add(player);
        if (_readyEndGameList.Count == _spawnManager.playersSpawned)
        {
           GameManager.InvokeBale();
        }
    }
    
    public void DecreaseReadyEnd(GameObject player)
    {
        _readyEndGameList.Remove(player);
    }

    public bool playerInReadyEnd(GameObject player)
    {
        return _readyEndGameList.Contains(player);
    }


}
