using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    #region fields

    private GameObject _playersManager;
    private GameObject player;
    private PlayerController _playerController;
    private testingPlayerManager _testingPlayerManager;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _playersManager = GameObject.Find("Players Manager");
        PlayersSpawnManager playersSpawnManager = _playersManager.GetComponent<PlayersSpawnManager>();
        gameObject.name = "Player " + (5 - playersSpawnManager.playersPrefabs.Count);
        transform.parent = _playersManager.transform;
        var ind = Random.Range(0, playersSpawnManager.playersPrefabs.Count);
        player = Instantiate(playersSpawnManager.playersPrefabs[ind],
            playersSpawnManager.playerInitialPositions[ind].transform.position,
            transform.rotation, transform);
        _playerController = player.GetComponent<PlayerController>();
        _testingPlayerManager = player.GetComponent<testingPlayerManager>();
        playersSpawnManager.playersPrefabs.Remove(playersSpawnManager.playersPrefabs[ind]);
        playersSpawnManager.playerInitialPositions.Remove(playersSpawnManager.playerInitialPositions[ind]);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _playerController.Move(context.ReadValue<Vector2>());
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