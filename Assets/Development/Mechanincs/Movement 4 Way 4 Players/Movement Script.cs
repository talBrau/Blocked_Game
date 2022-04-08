using UnityEngine;
using UnityScreen = UnityEngine.Screen;

public class BandHeadMovement : MonoBehaviour
{
    #region Inspector
    
    [SerializeField] private PlayerMovementKeys playerKeys;
    [SerializeField] private float speed = 6;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    
    #endregion

    #region Fields
    
    private float _maxX ;
    private float _minX ;
    private float _maxY ;
    private float _minY ;
    private Vector2 _direction = Vector2.zero;
    private Rigidbody2D _rb;

    #endregion

    #region MonoBehaviour
    
    private void Start()
    {
        Vector3 upRight = Camera.main.ScreenToWorldPoint(new
            Vector3(UnityScreen.width,UnityScreen.height,0));
        _maxX = upRight.x;
        _maxY = upRight.y;
        Vector3 downLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _minX = downLeft.x;
        _minY = downLeft.y;
        _rb = GetComponent<Rigidbody2D>();
    }

    // void Update()
    // {
    //     if (bandManager.CanMove)
    //     {
    //         CheckInput();
    //         CheckBorders(gameObject);
    //     }
    //     else
    //     {
    //         _direction = Vector2.zero;
    //     }
    // }
    // #endregion
    //
    // #region Methods
    //
    // private void CheckInput()
    // {
    //     if (Input.GetKeyDown(playerKeys.Up) && _rb.velocity != Vector2.down * speed)
    //         _direction = Vector2.up;
    //     else if (Input.GetKeyDown(playerKeys.Down)  && _rb.velocity != Vector2.up * speed)
    //         _direction = Vector2.down;
    //     else if (Input.GetKeyDown(playerKeys.Left) && _rb.velocity != Vector2.right * speed)
    //     {
    //         _direction = Vector2.left;
    //         bandManager.left = true;
    //     }
    //     else if (Input.GetKeyDown(playerKeys.Right) && _rb.velocity != Vector2.left * speed)
    //     {
    //         _direction = Vector2.right;
    //         bandManager.left = false;
    //     }
    //     if (_direction != Vector2.zero)
    //         GetComponent<BandHeadManager>().StartMove();
    // }
    
    public void CheckBorders(GameObject gameObj)
    {
        var tran = gameObj.transform;
        Vector3 pos = tran.position;
        if (pos.x < _minX)
            pos.x = _maxX;
        else if (pos.x > _maxX)
            pos.x = _minX;
        else if (pos.y < _minY)
            pos.y = _maxY;
        else if (pos.y > _maxY)
            pos.y = _minY;
        gameObj.transform.position = pos;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = _direction * speed;
        // spriteRenderer.flipX = !bandManager.left;
    }


    #endregion
}