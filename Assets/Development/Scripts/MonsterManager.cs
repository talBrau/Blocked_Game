using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{
    #region Inspector

    [FormerlySerializedAs("spawnRate")] [SerializeField]
    private float minWaitTime;

    [SerializeField] private float maxWaitTime;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private float timeTillSpawnChange = 10;
    public GameObject baseObject;
    public List<Transform> players;

    #endregion

    #region Fields

    private float _minRadius;
    private float _maxRadius;
    private float curWaveTime;
    private bool _onBoarding = true;

    #endregion

    #region MonoBehaviour

    void Start()
    {
        //get center of screen in world
        Vector3 centerRight = Camera.main.ScreenToWorldPoint
            (new Vector3(Screen.width, Screen.height / 2f, 0));

        _minRadius = centerRight.x;
        _maxRadius = 1f * _minRadius;
    }

    private void Update()
    {
        if (_onBoarding)
            return;

        curWaveTime += Time.deltaTime;
        if (curWaveTime >= timeTillSpawnChange)
        {
            minWaitTime -= 1;
            maxWaitTime -= 1;
            curWaveTime = 0;
        }
    }

    #endregion

    #region Methods

    private IEnumerator SpawnMonster()
    {
        while (true)
        {
            float distance = Random.Range(_minRadius, _maxRadius); //get radius
            float angle = Random.Range(-Mathf.PI, Mathf.PI); // get angle
            Vector2 spawnPos = transform.position; // middle of screen
            spawnPos += new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance; // set spawn location
            Instantiate(monsterPrefab, spawnPos, quaternion.identity, transform);

            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

    public int GetPlayersCount => players.Count;
    public Transform GetPlayerI(int i) => players[i];

    public void RemovePlayer(Transform player) => players.Remove(player);

    public void stopOnBoarding()
    {
        _onBoarding = false;
        StartCoroutine(SpawnMonster());
    }

    #endregion
}