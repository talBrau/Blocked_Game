using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersSpawnManager", menuName = "Players Spawn Manager", order = 51)]
public class PlayersSpawnManager : ScriptableObject
{
    public List<GameObject> playersPrefabs;
    public List<Vector3> playerInitialPositions;
}
