using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    #region Inspector

    [SerializeField] private PlayersSpawnManager _playersSpawnManager;

    #endregion

    #region fields

    private GameObject _playersManager;
    private GameObject player;
    private testingPlayerManager _testingPlayerManager;

    #endregion

    void Start()
    {
        _playersManager = GameObject.Find("Players Manager");
        gameObject.name = "Player " + (5 - _playersSpawnManager.playersPrefabs.Count);
        transform.parent = _playersManager.transform;
        
        var ind = Random.Range(0, _playersSpawnManager.playersPrefabs.Count);
        player = Instantiate(_playersSpawnManager.playersPrefabs[ind],
                              _playersSpawnManager.playerInitialPositions[ind], transform.rotation, transform);
        
        _testingPlayerManager = player.GetComponent<testingPlayerManager>();
        
        _playersSpawnManager.playersPrefabs.Remove(_playersSpawnManager.playersPrefabs[ind]);
        _playersSpawnManager.playerInitialPositions.Remove(_playersSpawnManager.playerInitialPositions[ind]);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _testingPlayerManager.Move(context.ReadValue<Vector2>());
    }

    public void InstantiateTile(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _testingPlayerManager.InstantiateTile();
    }

    public void MoveTile(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _testingPlayerManager.MoveTile();
    }
}