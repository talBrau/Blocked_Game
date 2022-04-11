using UnityEngine;
using UnityEngine.Tilemaps;

public class TileScript : MonoBehaviour
{
    #region fields

    private bool _moving;

    public bool Moving
    {
        set => _moving = value;
    }
    
    private GameObject _player;
    private Tilemap _wallsTilemap;
    private Tilemap _groundTilemap;
    private PlayerMovementScript _playerMovementScript;
    private PlayerManager _playerScript;
    
    #endregion

    public void setMovingTile(GameObject player)
    {
        _moving = true;
        _player = player;
        _playerScript = _player.GetComponent<PlayerManager>();
        _playerMovementScript = _player.GetComponent<PlayerMovementScript>();
        _wallsTilemap = _playerScript.WallTileMap;
        _groundTilemap = _playerScript.groundTileMap;
        GetComponent<EdgeCollider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void placeMovingTile()
    {
        // if (_groundTilemap.WorldToCell(_player.transform.position) == transform.position)
        // {
        //     var playerDir = _playerMovementScript.Direction;
        //     if (playerDir == Vector3Int.zero)
        //         playerDir = _playerMovementScript.LastDir;
        //     var tilePos = _groundTilemap.WorldToCell(transform.position + (playerDir));
        //     transform.position = _groundTilemap.CellToWorld(tilePos);
        // }
        _moving = false;
        
        GetComponent<EdgeCollider2D>().isTrigger = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void Update()
    {
        if (!_moving)
            return;

        var playerDir = _playerMovementScript.Direction;
        if (playerDir == Vector3.zero)
            playerDir = _playerMovementScript.LastDir;
        Vector3 check = playerDir;
        var temp = _player.transform.position + playerDir + check * 0.7f;
        Vector3Int gridPos = _groundTilemap.WorldToCell(temp); 
        var tilePos = _groundTilemap.CellToWorld(gridPos);
        transform.position = tilePos;

        if (_wallsTilemap.HasTile(_groundTilemap.WorldToCell(transform.position)))
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            print("yup");
        }

    }
}
