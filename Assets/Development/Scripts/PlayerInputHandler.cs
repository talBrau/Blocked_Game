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
        _playersSpawnManager.playersSpawned += 1;
        
        var ind = Random.Range(0, _playersSpawnManager.playersPrefabs.Count);
        player = Instantiate(_playersSpawnManager.playersPrefabs[ind],
                              _playersSpawnManager.playerInitialPositions[ind], transform.rotation, transform);
        
        _PlayerManager = player.GetComponent<PlayerManager>();
        
        _playersSpawnManager.playersPrefabs.Remove(_playersSpawnManager.playersPrefabs[ind]);
        _playersSpawnManager.playerInitialPositions.Remove(_playersSpawnManager.playerInitialPositions[ind]);
        
        var monsterManager = GameObject.Find("Monster Manager");
        monsterManager.GetComponent<MonsterManager>().AddPlayer(gameObject);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!_PlayerManager)
            return;
        if (context.performed)
            _PlayerManager.Move(context.ReadValue<Vector2>());
        else
            _PlayerManager.Move(Vector2.zero);
    }

    public void BuyWallTile(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _PlayerManager.BuyWallTile();
    }
    
    public void BuyTntTile(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _PlayerManager.BuyTntTile();
    }

    public void MoveTile(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _PlayerManager.MoveTile();
    }

    public void ReviveFriend(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _PlayerManager.ReviveFriend();
    }
    
    public void DetonateTnt(InputAction.CallbackContext context)
    {
        if (context.performed)
            _PlayerManager.DetonateTnt(true);
        else if (context.canceled)
            _PlayerManager.DetonateTnt(false);
    }

    public void SetReady(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _PlayerManager.SetReady();
    }

    public void SetReadyEndGame(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _PlayerManager.SetReadyEndGame();
    }
    public void ResetGame(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (GameManager.GameOverFlag)
            GameManager.InvokeResetGame();
    }
}