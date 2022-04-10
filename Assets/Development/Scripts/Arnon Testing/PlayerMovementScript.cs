using UnityEngine;
using UnityEngine.Tilemaps;
using UnityScreen = UnityEngine.Screen;

public class PlayerMovementScript : MonoBehaviour
{
    #region Inspector
    
    [SerializeField] private PlayerMovementKeys playerKeys;
    [SerializeField] private float speed = 6;
    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private Tilemap WallTileMap;
    [SerializeField] private GameObject TargetPosition;

    #endregion

    #region Fields
    
    private Vector3 _direction = Vector2.zero;
    public Vector3 Direction => _direction;

    private Vector3 _lastDir;
    public Vector3 LastDir => _lastDir;
    
    private Rigidbody2D _rb;
    private PlayerManager _playerManager;

    #endregion

    #region MonoBehaviour
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lastDir = Vector3.up;
        _playerManager = GetComponent<PlayerManager>();
    }

    void Update()
    {
        // transform.position = Vector3.MoveTowards(transform.position, TargetPosition.transform.position, 
        //                                         speed * Time.deltaTime);
        // if (Vector3.Distance(transform.position,TargetPosition.transform.position) <= 0.5f) 
        CheckInput();

    }
    
    #endregion
    
    #region Methods
    private void CheckInput()
    {
        float xDir = 0;
        float yDir = 0;
        if (Input.GetKey(playerKeys.Up))
            yDir = 1;
        else if (Input.GetKey(playerKeys.Down))
            yDir = -1;
        if (Input.GetKey(playerKeys.Left))
            xDir = -1;
        else if (Input.GetKey(playerKeys.Right))
            xDir = 1;
        _direction = new Vector3(xDir, yDir,0);
        Vector3Int gridPos = groundTileMap.WorldToCell(transform.position + _direction);
        if (!canMove(gridPos))
        {
            _direction = Vector3.zero;
            // TargetPosition.transform.position = groundTileMap.CellToWorld(gridPos);;
        }

        if (_playerManager.CurrentHoldTile)
        {
            LayerMask mask = LayerMask.GetMask("Wall");
            if (Physics2D.CircleCastAll(_playerManager.CurrentHoldTile.transform.position,
                0.01f, _direction, 0.01f, mask).Length != 0)
            {
                print("hit");
                _direction = Vector3.zero;
            }
                
        }
        
        if (_direction != _lastDir && _direction != Vector3.zero)
            _lastDir = _direction;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = _direction * speed;
    }

    private bool canMove(Vector3Int gridPos)
    {
        if (!groundTileMap.HasTile(gridPos) || WallTileMap.HasTile(gridPos))
            return false;
        return true;
    }
    #endregion
}