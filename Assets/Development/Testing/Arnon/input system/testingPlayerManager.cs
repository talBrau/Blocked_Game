using UnityEngine;
using UnityEngine.Tilemaps;

public class testingPlayerManager : MonoBehaviour
{
    #region Inspector
    
    [SerializeField] private GameObject tile;


    #endregion

    #region fields
    
    private GameObject wallsObject;
    private GameObject groundObject;
    
    private Tilemap groundTileMap;
    public Tilemap GroundTileMap => groundTileMap;
    private Tilemap wallTileMap;
    public Tilemap WallTileMap => wallTileMap;
    
    private bool _initTileFlag = true;
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

    public void InstantiateTile()
    {
        if (!_initTileFlag)
            return;

        if (!_currentHoldTile)
        {
            var newtile = Instantiate(tile, transform.position, transform.rotation, wallsObject.transform);
            newtile.GetComponent<testingTileScript>().setMovingTile(gameObject);
            _currentHoldTile = newtile;
        }
        else
        {
            _currentHoldTile.GetComponent<testingTileScript>().placeMovingTile();
            _currentHoldTile = null;
        }
    }
 
    public void MoveTile()
    {
        if (!(_nearTile || _currentHoldTile))
            return;
        
        if (!_currentHoldTile)
        {
            _initTileFlag = false;
            _currentHoldTile = _nearTile;
            _nearTile = null;
            _currentHoldTile.GetComponent<testingTileScript>().setMovingTile(gameObject);
        }
        else
        {
            _currentHoldTile.GetComponent<testingTileScript>().placeMovingTile();
            _currentHoldTile = null;
            _initTileFlag = true;
        }
    }

    #endregion
}