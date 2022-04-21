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
            foreach (var player in _spawnManager.playersPrefabs)
            {
                if (!player.GetComponent<PlayerManager>().isHoldingDetonateTrigger)
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
