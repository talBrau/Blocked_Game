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

    private bool _initTileFlag = true;
    private GameObject _nearTile;
    private GameObject _currentHoldTile;

    #endregion

    #region MonoBehaviour

    private void Update()
    {
        checkInput();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") && !_currentHoldTile)
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
    }

    #endregion

    #region Methods

    private void checkInput()
    {
        if (Input.GetKeyDown(_playerActionKeys.InstantiateTile) && _initTileFlag)
            InstantiateTile();
        else if (Input.GetKeyDown(_playerActionKeys.MoveTile) && (_nearTile || _currentHoldTile))
            MoveTile();
    }

    private void InstantiateTile()
    {
        if (!_currentHoldTile)
        {
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

    private void MoveTile()
    {
        if (!_currentHoldTile)
        {
            _initTileFlag = false;
            _currentHoldTile = _nearTile;
            _nearTile = null;
            _currentHoldTile.GetComponent<TileScript>().setMovingTile(gameObject);
        }
        else
        {
            _currentHoldTile.GetComponent<TileScript>().placeMovingTile();
            _currentHoldTile = null;
            _initTileFlag = true;
        }
    }

    #endregion
}