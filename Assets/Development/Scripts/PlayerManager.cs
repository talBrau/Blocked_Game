using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private GameObject wallTile;
    [SerializeField] private GameObject tntTile;

    #endregion

    #region fields

    // Movement
    private Vector3 _direction = Vector3.zero;
    public Vector3 Direction => _direction;
    private Vector3 _lastDir = Vector3.up;
    public Vector3 LastDir => _lastDir;

    // components
    private Rigidbody2D _rb;

    // create & move tiles
    private bool _canBuy;
    private GameObject wallsObject;
    private GameObject groundObject;

    private Tilemap groundTileMap;
    public Tilemap GroundTileMap => groundTileMap;
    private Tilemap wallTileMap;
    public Tilemap WallTileMap => wallTileMap;
    
    private GameObject _nearTile;
    private GameObject _currentHoldTile;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        wallsObject = GameObject.Find("Walls");
        wallTileMap = wallsObject.GetComponent<Tilemap>();
        groundObject = GameObject.Find("Ground");
        groundTileMap = groundObject.GetComponent<Tilemap>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Base"))
            _canBuy = true;
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") && !_currentHoldTile && !_nearTile)
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            _nearTile = other.gameObject;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") && _nearTile)
        {
            _nearTile.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            _nearTile = null;
        }

        if (other.gameObject.CompareTag("Base"))
            _canBuy = false;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _direction * moveSpeed;
    }

    #endregion

    #region Methods

    public void Move(Vector2 input)
    {
        _direction = input;
        if (_direction != _lastDir && _direction != Vector3.zero)
            _lastDir = _direction;
    }
    
    public void BuyWallTile()
    {
        if (!_currentHoldTile && _canBuy)
        {
            var newtile = Instantiate(wallTile, transform.position, transform.rotation, wallsObject.transform);
            newtile.GetComponent<TileScript>().setMovingTile(gameObject);
            _currentHoldTile = newtile;
        }
        else if (_currentHoldTile)
        {
            _currentHoldTile.GetComponent<TileScript>().placeMovingTile();
            _currentHoldTile = null;
        }
    }
    
    public void BuyTntTile()
    {
        if (!_currentHoldTile && _canBuy)
        {
            var newtile = Instantiate(tntTile, transform.position, transform.rotation, wallsObject.transform);
            newtile.GetComponent<TileScript>().setMovingTile(gameObject);
            _currentHoldTile = newtile;
        }
        else if (_currentHoldTile)
        {
            _currentHoldTile.GetComponent<TileScript>().placeMovingTile();
            _currentHoldTile = null;
        }
    }


    public void MoveTile()
    {
        if (!(_nearTile || _currentHoldTile))
            return;

        if (!_currentHoldTile)
        {
            _currentHoldTile = _nearTile;
            _nearTile = null;
            _currentHoldTile.GetComponent<TileScript>().setMovingTile(gameObject);
        }
        else
        {
            if (!_currentHoldTile.GetComponent<TileScript>().CanPlace)
                return;
            _currentHoldTile.GetComponent<TileScript>().placeMovingTile();
            _currentHoldTile = null;
        }
    }
    

    #endregion
}