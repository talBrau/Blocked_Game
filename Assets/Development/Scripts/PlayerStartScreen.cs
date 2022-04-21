using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerStartScreen : MonoBehaviour
{
    #region Inspector

    [SerializeField] private float moveSpeed = 4;

    #endregion

    #region fields

    // Movement
    private Vector3 _direction = Vector3.zero;
    public Vector3 Direction => _direction;
    private Vector3 _lastDir = Vector3.up;
    public Vector3 LastDir => _lastDir;

    // components
    private Rigidbody2D _rb;
    
    #endregion

    #region MonoBehaviour

    private void Start()
    {
       
        _rb = GetComponent<Rigidbody2D>();
    }
    

    private void FixedUpdate()
    {
        _rb.velocity = _direction * moveSpeed;
    }

    #endregion

    #region Methods

    public void Move(Vector2 input)
    {
        _direction = input;
        if (_direction != _lastDir && _direction != Vector3.zero)
            _lastDir = _direction;
    }
    
    #endregion
}