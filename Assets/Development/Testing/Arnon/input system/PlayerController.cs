using UnityEngine;


public class PlayerController : MonoBehaviour
{
    #region Inpsector

    [SerializeField] private float speed = 4;

    #endregion

    #region Fields

    // Input
    private PlayerControls playerControls;

    // Movement
    private Vector3 _direction = Vector3.zero;
    public Vector3 Direction => _direction;
    private Vector3 _lastDir = Vector3.up;
    public Vector3 LastDir => _lastDir;

    // components
    private Rigidbody2D _rb;
    
    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    public void Move(Vector2 input)
    {
        _direction = input;
        if (_direction != _lastDir && _direction != Vector3.zero)
            _lastDir = _direction;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = _direction * speed;
    }
}
