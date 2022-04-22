using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class monsterController : MonoBehaviour
{
    #region Inspector

    [SerializeField] private float maxTimeOnWall = 10;
    [SerializeField] private float monsterSpeed;
    [SerializeField] private float timeUntilDead = 10;

    #endregion

    #region Fields

    public MonsterManager monsterManager;
    private bool _isTouchingWall = false;
    private float _timeOnWall = 0;
    private GameObject _curTile = null;
    private Transform _target;

    private float _timeAlive;
    // private Color _defaultTileColor;

    #endregion


    #region MonoBehaviour

    void Start()
    {
        monsterManager = FindObjectOfType<MonsterManager>();
        int targetPlayerOrBase = Random.Range(0, 2);
        if (targetPlayerOrBase == 0)
            InvokeRepeating(nameof(LookForTarget), 0f, 3f);
        else
            _target = monsterManager.baseObject.transform;
        print(targetPlayerOrBase);
    }

    void Update()
    {
        UpdateLifeTime();
        if (_target != null && !_isTouchingWall)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _target.position, Time.deltaTime * monsterSpeed);
        }

        if (_isTouchingWall)
        {
            UpdateTileTouch();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        if (!_isTouchingWall && obj.CompareTag("Wall"))
        {
            //change color of tile to indicate its being eaten
            obj.GetComponent<SpriteRenderer>().color = Color.gray;
            print("gray?");
            _isTouchingWall = true;
            _timeOnWall = 0;
            _curTile = obj;
        }

        if (obj.CompareTag("Player"))
        {
            if (!obj.GetComponent<PlayerManager>().IsAlive)
                return;
            obj.GetComponent<PlayerManager>().playerDead();
            if (monsterManager.GetPlayersCount == 0) //endgame
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            Destroy(gameObject);
        }

    }

    #endregion

    #region Methods

    private void UpdateTileTouch()
    {
        _timeOnWall += Time.deltaTime;
        if (_timeOnWall > maxTimeOnWall || _curTile == null)
        {
            Destroy(_curTile);
            _curTile = null;
            _isTouchingWall = false;
            _timeOnWall = 0;
        }
    }

    private void UpdateLifeTime()
    {
        if (_timeAlive > timeUntilDead)
        {
            if (_curTile != null)
            {
                _curTile.GetComponent<SpriteRenderer>().color = Color.white;
            }

            Destroy(gameObject);
        }

        _timeAlive += Time.deltaTime;
    }


    private void LookForTarget()
    {
        int playerCount = monsterManager.GetPlayersCount;
        if (playerCount == 0)
        {
            return; // TODO: INVOKE ENDGAME WHEN MERGED
        }

        float[] distances = new float[playerCount];
        for (int i = 0; i < playerCount; i++)
        {
            distances[i] = Vector3.Distance(monsterManager.GetPlayerI(i).position, transform.position);
        }

        int minInd = Array.IndexOf(distances, distances.Min());
        _target = monsterManager.GetPlayerI(minInd);
        _target = _target.GetChild(0);
    }

    #endregion
}