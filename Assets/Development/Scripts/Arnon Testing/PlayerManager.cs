using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject tile;
    [SerializeField] private PlayerActionKeys _playerActionKeys;
    [SerializeField] private GameObject walls;
    public Tilemap groundTileMap;
    public Tilemap WallTileMap;

    #endregion

    #region fields

    private bool _initTileFlag;
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
        // var map = WallTileMap.WorldToCell(transform.position);
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     print(map);
        // }
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     var world = WallTileMap.CellToWorld(map);
        //     print(world);
        // }
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

        // else if (Input.GetKey(_playerActionKeys.MoveTile))
        //     _moveTileFlag = true;
        // else if (Input.GetKeyUp(_playerActionKeys.MoveTile))
        // {
        //     _moveTileFlag = false;
        //     if (_currentHoldTile)
        //     {
        //         _currentHoldTile.transform.parent = walls.transform;
        //         _currentHoldTile.layer = 7;
        //         _currentHoldTile = null;
        //     }
        // }
    }

    private void InstantiateTile()
    {
        _initTileFlag = !_initTileFlag;
        if (_initTileFlag)
        {
            // var dir = _playerMovementScript.Direction;
            // if (dir == Vector3.zero)
            //     dir = _playerMovementScript.LastDir;
            // Vector3Int gridPos = groundTileMap.WorldToCell(transform.position + (dir));
            // var tilePos = groundTileMap.CellToWorld(gridPos);
            var newtile = Instantiate(tile, transform.position, transform.rotation, walls.transform);
            newtile.GetComponent<TileScript>().setMovingTile(gameObject);
            _currentHoldTile = newtile;
        }
        else
        {
            _currentHoldTile.GetComponent<TileScript>().placeMovingTile();
            _currentHoldTile = null;
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