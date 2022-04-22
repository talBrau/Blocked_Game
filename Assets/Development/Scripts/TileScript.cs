using UnityEngine;
using UnityEngine.Tilemaps;

public class TileScript : MonoBehaviour
{
    #region Inspector

    [Range(0, 1)]
    [SerializeField] private float onMoveTransparency = 0.5f;

    #endregion
    
    
    #region fields 

    private GameObject _player;
    private Tilemap _wallsTilemap;
    private Tilemap _groundTilemap;
    private PlayerManager _playerScript;
    private bool _moving;
    private bool _canPlace;
    public bool CanPlace => _canPlace;

    #endregion

    public void setMovingTile(GameObject player)
    {
        _moving = true;
        _player = player;
        _playerScript = _player.GetComponent<PlayerManager>();
        _wallsTilemap = _playerScript.WallTileMap;
        _groundTilemap = _playerScript.GroundTileMap;
        GetComponent<EdgeCollider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().color = new Color(1,1,1, onMoveTransparency);
    }

    public void placeMovingTile()
    {
        _moving = false;
        
        GetComponent<EdgeCollider2D>().isTrigger = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") && _moving && !_canPlace)
        {
            _canPlace = true;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, onMoveTransparency);
        }
    }

    private void Update()
    {
        if (!_moving)
            return;

        var playerDir = _playerScript.Direction;
        if (playerDir == Vector3.zero)
            playerDir = _playerScript.LastDir;
        // Vector3 check = playerDir;
        var temp = _player.transform.position + playerDir;
        Vector3Int gridPos = _groundTilemap.WorldToCell(temp);
        var tilePos = _groundTilemap.CellToWorld(gridPos);
        transform.position = tilePos;

        /// TODO : FIX!!!
        if (_wallsTilemap.HasTile(_wallsTilemap.WorldToCell(transform.position))
            || !_groundTilemap.HasTile(_groundTilemap.WorldToCell(transform.position - Vector3.up)))
        {
            _canPlace = false;
            GetComponent<SpriteRenderer>().color = new Color(1,0,0,onMoveTransparency);
        }
        else
        {
            if (!_canPlace)
            {
                _canPlace = true;
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, onMoveTransparency);
            }
        }

    }
}