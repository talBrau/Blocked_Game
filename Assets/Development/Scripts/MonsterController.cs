using System;
using System.Linq;
using Development.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;


public class MonsterController : MonoBehaviour
{
    #region Inspector

    [SerializeField] private float monsterSpeed;
    [SerializeField] private float timeUntilDead = 10;

    #endregion

    #region Fields

    private IsometricCharecterRenderer _isometricRenderer;
    private PlayersButtons _playersButtons;
    public MonsterManager monsterManager;
    private bool _isTouchingWall;
    private GameObject _curTile;
    private Transform _target;

    private float _timeAlive;

    #endregion

    #region MonoBehaviour

    void Start()
    {
        _isometricRenderer = GetComponentInChildren<IsometricCharecterRenderer>();
        _isometricRenderer.isMonster = true;
        _playersButtons = GameObject.Find("Players Manager").GetComponent<PlayersButtons>();
        monsterManager = FindObjectOfType<MonsterManager>();
        int targetPlayerOrBase = Random.Range(0, 2);
        if (targetPlayerOrBase == 0)
            InvokeRepeating(nameof(LookForTarget), 0f, 3f);
        else
            _target = monsterManager.baseObject.transform;
    }

    void Update()
    {
        UpdateLifeTime();
        if (_target != null && !_isTouchingWall)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _target.position, Time.deltaTime * monsterSpeed);
            var direction = Vector2.ClampMagnitude((_target.position - transform.position), 1);
            _isometricRenderer.SetDirection(direction);
        }

        if (_isTouchingWall)
        {
            UpdateTileTouch();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        if (!_isTouchingWall && (obj.CompareTag("Wall") || obj.CompareTag("Exploding Tile")))
        {
            //change color of tile to indicate its being eaten
            obj.GetComponent<SpriteRenderer>().color = Color.gray;
            _isTouchingWall = true;
            _curTile = obj;
        }

        if (obj.CompareTag("Player"))
        {
            if (!obj.GetComponent<PlayerManager>().IsAlive)
                return;
            obj.GetComponent<PlayerManager>().playerDead();
            if (monsterManager.GetPlayersCount == 0)
            {
                GameManager.InvokeGameOver();
            }

            Destroy(gameObject);
        }
    }

    #endregion

    #region Methods

    private void UpdateTileTouch()
    {
        var value = Time.deltaTime;
        if (!_curTile)
        {
            _curTile = null;
            _isTouchingWall = false;
            return;
        }

        _curTile.GetComponent<TileScript>().UpdateTileHealth(value);
        if (_curTile.GetComponent<TileScript>().TileHealth <= 0)
        {
            if (_playersButtons.explodingTiles.Contains(_curTile))
                _playersButtons.explodingTiles.Remove(_curTile);
            Destroy(_curTile);
            _curTile = null;
            _isTouchingWall = false;
        }
    }

    private void UpdateLifeTime()
    {
        if (_timeAlive > timeUntilDead)
        {
            Destroy(gameObject);
        }

        _timeAlive += Time.deltaTime;
    }


    private void LookForTarget()
    {
        int playerCount = monsterManager.GetPlayersCount;
        if (playerCount == 0)
        {
            return;
        }

        float[] distances = new float[playerCount];
        for (int i = 0; i < playerCount; i++)
        {
            distances[i] = Vector3.Distance(monsterManager.GetPlayerI(i).transform.position, transform.position);
        }

        int minInd = Array.IndexOf(distances, distances.Min());
        _target = monsterManager.GetPlayerI(minInd).transform;
        _target = _target.GetChild(0);
    }

    #endregion
}