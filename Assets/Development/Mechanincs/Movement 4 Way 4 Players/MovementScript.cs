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
        _direction = new Vector2(xDir, yDir);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _direction * speed;
    }
    
    #endregion
}