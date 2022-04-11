using UnityEngine;
using UnityScreen = UnityEngine.Screen;

public class PlayerMovementScript : MonoBehaviour
{
    #region Inspector

    [SerializeField] private PlayerMovementKeys playerKeys;
    [SerializeField] private int speed = 4;

    #endregion

    #region Fields

    private Vector3Int _direction = Vector3Int.zero;
    public Vector3Int Direction => _direction;

    private Vector3Int _lastDir;
    public Vector3Int LastDir => _lastDir;

    private Rigidbody2D _rb;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lastDir = Vector3Int.up;
    }

    void Update()
    {
        CheckInput();
    }

    #endregion

    #region Methods

    private void CheckInput()
    {
        int xDir = 0;
        int yDir = 0;
        if (Input.GetKey(playerKeys.Up))
            yDir = 1;
        else if (Input.GetKey(playerKeys.Down))
            yDir = -1;
        if (Input.GetKey(playerKeys.Left))
            xDir = -1;
        else if (Input.GetKey(playerKeys.Right))
            xDir = 1;
        _direction = new Vector3Int(xDir, yDir, 0);
        if (_direction != _lastDir && _direction != Vector3.zero)
            _lastDir = _direction;
    }

    private void FixedUpdate()
    {
        var velX = _direction.x * speed;
        var velY = _direction.y * speed;
        _rb.velocity = new Vector2(velX, velY);
    }

    #endregion
}