using UnityEngine;
using UnityEngine.Tilemaps;

public class testingTileScript : MonoBehaviour
{
    #region fields

    private GameObject _player;
    private Tilemap _wallsTilemap;
    private Tilemap _groundTilemap;
    private testingPlayerManager _playerScript;
    private bool _moving;

    #endregion

    public void setMovingTile(GameObject player)
    {
        _moving = true;
        _player = player;
        _playerScript = _player.GetComponent<testingPlayerManager>();
        _wallsTilemap = _playerScript.WallTileMap;
        _groundTilemap = _playerScript.GroundTileMap;
        GetComponent<EdgeCollider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void placeMovingTile()
    {
        _moving = false;
        
        GetComponent<EdgeCollider2D>().isTrigger = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void Update()
    {
        if (!_moving)
            return;

        var playerDir = _playerScript.Direction;
        if (playerDir == Vector3.zero)
            playerDir = _playerScript.LastDir;
        Vector3 check = playerDir;
        var temp = _player.transform.position + playerDir + check * 0.7f;
        Vector3Int gridPos = _groundTilemap.WorldToCell(temp);
        var tilePos = _groundTilemap.CellToWorld(gridPos);
        transform.position = tilePos;

        /// TODO : FIX!!!
        if (_wallsTilemap.HasTile(_groundTilemap.WorldToCell(transform.position)))
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            print("yup");
        }
    }
}