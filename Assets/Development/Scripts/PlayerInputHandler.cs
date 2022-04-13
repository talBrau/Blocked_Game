using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    
    #region fields

    private GameObject _playersManager;
    private GameObject player;
    private PlayerManager _PlayerManager;

    #endregion

    void Start()
    {
        _playersManager = GameObject.Find("Players Manager");
        PlayersSpawnManager _playersSpawnManager = _playersManager.GetComponent<PlayersSpawnManager>();
        gameObject.name = "Player " + (5 - _playersSpawnManager.playersPrefabs.Count);
        transform.parent = _playersManager.transform;
        
        var ind = Random.Range(0, _playersSpawnManager.playersPrefabs.Count);
        player = Instantiate(_playersSpawnManager.playersPrefabs[ind],
                              _playersSpawnManager.playerInitialPositions[ind], transform.rotation, transform);
        
        _PlayerManager = player.GetComponent<PlayerManager>();
        
        _playersSpawnManager.playersPrefabs.Remove(_playersSpawnManager.playersPrefabs[ind]);
        _playersSpawnManager.playerInitialPositions.Remove(_playersSpawnManager.playerInitialPositions[ind]);
        
        var monsterManager = GameObject.Find("Monster Manager");
        monsterManager.GetComponent<MonsterManager>().players.Add(transform);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _PlayerManager.Move(context.ReadValue<Vector2>());
    }

    public void InstantiateTile(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _PlayerManager.InstantiateTile();
    }

    public void MoveTile(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _PlayerManager.MoveTile();
    }
}