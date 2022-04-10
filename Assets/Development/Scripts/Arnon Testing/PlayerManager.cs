using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject tile;
    [SerializeField] private PlayerActionKeys _playerActionKeys;
    [SerializeField] private GameObject walls;
    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private Tilemap WallTileMap;

    #endregion

    #region fields

    private bool _moveTileFlag;
    private GameObject _currentHoldTile;
    public GameObject CurrentHoldTile => _currentHoldTile;
    private PlayerMovementScript _playerMovementScript;
    
    #endregion

    #region MonoBehaviour

    private void Start()
    {
        _playerMovementScript = GetComponent<PlayerMovementScript>();
    }

    private void Update()
    {
        checkInput();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") && _moveTileFlag && !_currentHoldTile)
        {
            _currentHoldTile = other.gameObject;
            other.gameObject.transform.parent = gameObject.transform;
            _currentHoldTile.layer = 8;
        }
    }

    #endregion

    #region Methods

    private void checkInput()
    {
        if (Input.GetKeyDown(_playerActionKeys.InstantiateTile))
            InstantiateTile();
        else if (Input.GetKey(_playerActionKeys.MoveTile))
            _moveTileFlag = true;
        else if (Input.GetKeyUp(_playerActionKeys.MoveTile))
        {
            _moveTileFlag = false;
            if (_currentHoldTile)
            {
                _currentHoldTile.transform.parent = walls.transform;
                _currentHoldTile.layer = 7;
                _currentHoldTile = null;
            }
        }
    }

    private void InstantiateTile()
    {
        var dir = _playerMovementScript.Direction;
        if (dir == Vector3.zero)
            dir = _playerMovementScript.LastDir;
        Vector3Int gridPos = groundTileMap.WorldToCell(transform.position + (dir * 1.3f));
        if (canMove(gridPos))
        {
            var tilePos = groundTileMap.CellToWorld(gridPos);
            Instantiate(tile, tilePos, transform.rotation, walls.transform);
        }
    }
    
    private bool canMove(Vector3Int gridPos)
    {
        if (!groundTileMap.HasTile(gridPos) || WallTileMap.HasTile(gridPos)|| 
            gridPos == groundTileMap.CellToWorld(groundTileMap.WorldToCell(transform.position)))
            return false;
        return true;
    }

    #endregion
}