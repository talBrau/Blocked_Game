using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{

    #region Inspector

    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject monsterPrefab;
    public List<Transform> players;

    #endregion

    #region Fields

    private float _minRadius;
    private float _maxRadius;

    #endregion

    #region MonoBehaviour

    void Start()
    {
        //get center of screen in world
        Vector3 centerRight = Camera.main.ScreenToWorldPoint
            (new Vector3(Screen.width, Screen.height / 2f, 0));
        
        _minRadius = centerRight.x;
        _maxRadius = 1f * _minRadius;
        StartCoroutine(SpawnMonster(spawnRate));
    }

    #endregion
    
    #region Methods

    private IEnumerator SpawnMonster(float sec)
    {
        while (true)
        {
            float distance = Random.Range(_minRadius, _maxRadius); //get radius
            float angle = Random.Range(-Mathf.PI, Mathf.PI); // get angle
            Vector2 spawnPos = transform.position; // middle of screen
            spawnPos += new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance; // set spawn location
            Instantiate(monsterPrefab, spawnPos, quaternion.identity, transform);
            yield return new WaitForSeconds(sec);
        }
    }

    public int GetPlayersCount => players.Count;
    public Transform GetPlayerI(int i) => players[i];

    public void RemovePlayer(Transform player) => players.Remove(player);

    #endregion
}