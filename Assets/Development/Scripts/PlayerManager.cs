using System;
using System.Numerics;
using Development.Scripts;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private GameObject wallTile;
    [SerializeField] private GameObject tntTile;
    [SerializeField] private IsometricCharecterRenderer _isoRenderer;
    [SerializeField] private Sprite deadSprite;
    public GameObject PlayerRenderer;

    #endregion

    #region fields

    // Movement
    private Vector3 _direction = Vector3.zero;
    public Vector3 Direction => _direction;
    private Vector3 _lastDir = Vector3.up;
    public Vector3 LastDir => _lastDir;

    // components
    private Rigidbody2D _rb;

    // create & move tiles
    private bool _canBuy;
    private GameObject wallsObject;
    private GameObject groundObject;
    private GameObject sceneManager;
    private Toturial _toturial;
    private Tilemap groundTileMap;
    public Tilemap GroundTileMap => groundTileMap;
    private Tilemap wallTileMap;
    public Tilemap WallTileMap => wallTileMap;

    private GameObject _nearTile;

    private GameObject _currentHoldTile;

    //endGame

    private bool isStandingOnButton;

    public bool IsStandingOnButton
    {
        get => isStandingOnButton;
        set => isStandingOnButton = value;
    }

    private bool _boughtTNT;

    private GameObject _nearFriend;
    private bool isAlive = true;

    public bool IsAlive
    {
        get => isAlive;
        set => isAlive = value;
    }

    private MonsterManager _monsterManager;
    private PlayersButtons _playersButtons;
    private bool _buttonPressed;
    private PlayerAudioManager _playerAudioManager;
    public Sprite InitialSprite;
    private Toturial.Keys _readyButton = Toturial.Keys.empty;

    #endregion

    #region MonoBehaviour

    private void OnEnable()
    {
        GameManager.Bale += hidePlayer;
        GameManager.GameOver += hidePlayer;
    }

    private void OnDestroy()
    {
        GameManager.Bale -= hidePlayer;
        GameManager.GameOver -= hidePlayer;
    }

    private void Awake()
    {
        _playersButtons = GameObject.Find("Players Manager").GetComponent<PlayersButtons>();
        sceneManager = GameObject.Find("GameManager");
        wallsObject = GameObject.Find("Walls");
        wallTileMap = wallsObject.GetComponent<Tilemap>();
        groundObject = GameObject.Find("Ground");
        groundTileMap = groundObject.GetComponent<Tilemap>();
        _rb = GetComponent<Rigidbody2D>();
        _monsterManager = GameObject.Find("Monster Manager").GetComponent<MonsterManager>();
        _isoRenderer = GetComponentInChildren<IsometricCharecterRenderer>();
        _toturial = GetComponent<Toturial>();
        _toturial.ShowKey(Toturial.Keys.MoveKey);
        _playerAudioManager = GetComponent<PlayerAudioManager>();
        InitialSprite = PlayerRenderer.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            _canBuy = true;
            _toturial.ShowKey(Toturial.Keys.LbRbKey);
            if (_readyButton != Toturial.Keys.empty)
                _toturial.HideKey(_readyButton);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Exploding Tile")) &&
            (!_currentHoldTile && !_nearTile))
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            _nearTile = other.gameObject;
            _toturial.ShowKey(Toturial.Keys.LeftKey);
            if (_readyButton != Toturial.Keys.empty)
                _toturial.HideKey(_readyButton);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Exploding Tile")) && _nearTile)
        {
            _nearTile.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            _nearTile = null;
            _toturial.HideKey(Toturial.Keys.LeftKey);
            if (_readyButton != Toturial.Keys.empty)
                _toturial.ShowKey(_readyButton);
        }

        if (other.gameObject.CompareTag("Base"))
        {
            _canBuy = false;
            _toturial.HideKey(Toturial.Keys.LbRbKey);
            if (_readyButton != Toturial.Keys.empty)
                _toturial.ShowKey(_readyButton);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Button"))
        {
            _toturial.ShowKey(Toturial.Keys.UpKey);
            if (_readyButton != Toturial.Keys.empty)
                _toturial.HideKey(_readyButton);
        }

        //can revive player
        if (col.gameObject.CompareTag("Player") && isAlive)
        {
            _toturial.ShowKey(Toturial.Keys.RightKey);
            if (_readyButton != Toturial.Keys.empty)
                _toturial.HideKey(_readyButton);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isAlive)
        {
            _nearFriend = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isAlive)
        {
            _nearFriend = null;
            _toturial.HideKey(Toturial.Keys.RightKey);
        }

        if (other.gameObject.CompareTag("Button"))
        {
            _toturial.HideKey(Toturial.Keys.UpKey);
            if (_readyButton != Toturial.Keys.empty)
                _toturial.ShowKey(_readyButton);
            if (_playersButtons.playerInReadyEnd(gameObject))
            {
                _playersButtons.DecreaseReadyEnd(gameObject);
                _toturial.HideKey(Toturial.Keys.ReadyBail);
                _readyButton = Toturial.Keys.empty;
            }
        }
    }
    private void Update()
    {
        if (!GameManager.onBoarding)
        {
            _toturial.HideKey(Toturial.Keys.ReadyGame);  
            _readyButton = Toturial.Keys.empty;
        }

    }

    private void FixedUpdate()
    {
        _rb.velocity = _direction * moveSpeed;
        var inputVector = Vector2.ClampMagnitude(_direction, 1);
        _isoRenderer.isHolding = _currentHoldTile != null;
        _isoRenderer.SetDirection(inputVector);
    }

    #endregion

    #region Methods

    public void Move(Vector2 input)
    {
        _toturial.HideKey(Toturial.Keys.MoveKey);
        if (!isAlive)
        {
            _direction = Vector3.zero;
            return;
        }

        _direction = input;
        if (_direction != _lastDir && _direction != Vector3.zero)
            _lastDir = _direction;
    }

    public void BuyWallTile()
    {
        if (!_currentHoldTile && _canBuy)
        {
            if (GameManager.Score < GameManager.WallTilePrice)
            {
                return;
            }

            GameManager.Score -= GameManager.WallTilePrice;
            var newtile = Instantiate(wallTile, transform.position, transform.rotation, wallsObject.transform);
            newtile.GetComponent<TileScript>().setMovingTile(gameObject);
            _playerAudioManager.playBuyTile();
            _currentHoldTile = newtile;
        }
        else if (_currentHoldTile)
        {
            if (_currentHoldTile.gameObject.CompareTag("Exploding Tile") && _boughtTNT)
            {
                _boughtTNT = false;
                _playersButtons.addToList(_currentHoldTile);
            }

            _currentHoldTile.GetComponent<TileScript>().placeMovingTile();
            _playerAudioManager.playMoveTile();
            _currentHoldTile = null;
        }
    }

    public void BuyTntTile()
    {
        if (!_currentHoldTile && _canBuy)
        {
            if (GameManager.Score < GameManager.ExplodingTilePrice)
            {
                // TODO
                return;
            }

            GameManager.Score -= GameManager.ExplodingTilePrice;
            var newtile = Instantiate(tntTile, transform.position, transform.rotation, wallsObject.transform);
            newtile.GetComponent<TileScript>().setMovingTile(gameObject);
            _playerAudioManager.playBuyTile();
            _currentHoldTile = newtile;
            _boughtTNT = true;
        }
        else if (_currentHoldTile)
        {
            _currentHoldTile.GetComponent<TileScript>().placeMovingTile();
            if (_currentHoldTile.gameObject.CompareTag("Exploding Tile") && _boughtTNT)
            {
                _boughtTNT = false;
                _playersButtons.addToList(_currentHoldTile);
            }
            _playerAudioManager.playMoveTile();
            _currentHoldTile = null;
        }
    }


    public void MoveTile()
    {
        if (!(_nearTile || _currentHoldTile))
            return;

        if (!_currentHoldTile)
        {
            _currentHoldTile = _nearTile;
            _nearTile = null;
            _currentHoldTile.GetComponent<TileScript>().setMovingTile(gameObject);
            _toturial.ShowKey(Toturial.Keys.LeftKey);
            _toturial.leftKey.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.4f);
            _playerAudioManager.playMoveTile();
        }
        else
        {
            if (!_currentHoldTile.GetComponent<TileScript>().CanPlace)
                return;
            if (_currentHoldTile.gameObject.CompareTag("Exploding Tile") && _boughtTNT)
            {
                _boughtTNT = false;
                _playersButtons.addToList(_currentHoldTile);
            }

            _toturial.HideKey(Toturial.Keys.LeftKey);
            _rb.AddForce(-_lastDir*2000);
            _toturial.leftKey.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1f);
            _currentHoldTile.GetComponent<TileScript>().placeMovingTile();
            _playerAudioManager.playMoveTile();
            _currentHoldTile = null;
        }
    }

    public void SetReady()
    {
        if (_toturial.ReadyGame.activeSelf)
            return;
        _toturial.ShowKey(Toturial.Keys.ReadyGame);
        _readyButton = Toturial.Keys.ReadyGame;
        sceneManager.GetComponent<GameManager>().increaseReadyCounter();
    }

    public void DetonateTnt(bool startPress)
    {
        if (isStandingOnButton)
        {
            if (startPress)
            {
                _playersButtons.IncreaseReadyDetonate();
                _buttonPressed = true;
                return;
            }
        }

        if (_buttonPressed)
        {
            _playersButtons.DecreaseReadyDetonate();
            _buttonPressed = false;
        }
    }

    public void SetReadyEndGame()
    {
        if (isStandingOnButton)
        {
            _playersButtons.IncreaseReadyEnd(gameObject);
            _toturial.ShowKey(Toturial.Keys.ReadyBail);
            _readyButton = Toturial.Keys.ReadyBail;
            _buttonPressed = true;
        }
    }

    public void ReviveFriend()
    {
        if (_nearFriend && !(_nearFriend.GetComponent<PlayerManager>().isAlive))
        {
            _monsterManager.AddPlayer(_nearFriend.transform.parent.gameObject);
            var friendScript = _nearFriend.gameObject.GetComponent<PlayerManager>();
            friendScript.IsAlive = true;
            friendScript.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
            friendScript.gameObject.GetComponent<Toturial>().HideKey(Toturial.Keys.AskHelp);
            friendScript.PlayerRenderer.GetComponent<SpriteRenderer>().sprite = friendScript.InitialSprite;
            friendScript.PlayerRenderer.GetComponent<Animator>().enabled = true;
            _playerAudioManager.playReviveFriend();
            _nearFriend = null;
            _toturial.HideKey(Toturial.Keys.RightKey);
        }
    }

    public void playerDead()
    {
        _monsterManager.RemovePlayer(transform.parent.gameObject);
        isAlive = false;
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        PlayerRenderer.GetComponent<Animator>().enabled = false;
        PlayerRenderer.GetComponent<SpriteRenderer>().sprite = deadSprite;
        _toturial.ShowKey(Toturial.Keys.AskHelp);
        if (_currentHoldTile)
        {
            Destroy(_currentHoldTile);
            _currentHoldTile = null;
        }
    }

    private void hidePlayer()
    {
        gameObject.SetActive(false);
    }

    #endregion
}