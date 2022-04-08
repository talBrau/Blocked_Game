using UnityEngine;
using UnityScreen = UnityEngine.Screen;

public class MovementScript : MonoBehaviour
{
    #region Inspector
    
    [SerializeField] private PlayerMovementKeys playerKeys;
    [SerializeField] private float speed = 6;

    #endregion

    #region Fields
    
    private Vector2 _direction = Vector2.zero;
    private Rigidbody2D _rb;

    #endregion

    #region MonoBehaviour
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
    }
    
    #endregion
    
    #region Methods
    private void CheckInput()
    {
        if (Input.GetKeyDown(playerKeys.Up))
            _direction = Vector2.up;
        else if (Input.GetKeyDown(playerKeys.Down))
            _direction = Vector2.down;
        else if (Input.GetKeyDown(playerKeys.Left))
            _direction = Vector2.left;
        else if (Input.GetKeyDown(playerKeys.Right))
            _direction = Vector2.right;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _direction * speed;
    }
    
    #endregion
}