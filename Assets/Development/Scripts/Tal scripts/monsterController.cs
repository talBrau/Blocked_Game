using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class monsterController : MonoBehaviour
{
    [SerializeField] private float lookRadius = 10f;
    [SerializeField] private List<Transform> players;
    [SerializeField] private float maxTimeOnWall = 10;
    [SerializeField] private float monsterSpeed;
    [SerializeField] private float TimeUntillDead = 10;
    private bool isTouchingWall = false;
    private float TimeOnWall = 0;
    private GameObject curTile = null;
    private Transform target;
    private float TimeAlive;

    void Start()
    {
        InvokeRepeating(nameof(LookForTarget), 0f, 3f);
    }

    void Update()
    {
        updateLifeTime();

        if (target != null && !isTouchingWall)
        {
            print(target.name);
            transform.position = Vector2.MoveTowards(transform.position, target.position,
                Time.deltaTime * monsterSpeed);
        }

        if (isTouchingWall)
        {
            TimeOnWall += Time.deltaTime;
            if (TimeOnWall > maxTimeOnWall || curTile == null)
            {
                Destroy(curTile);
                curTile = null;
                isTouchingWall = false;
                TimeOnWall = 0;
            }
        }
    }

    private void updateLifeTime()
    {
        if (TimeAlive > TimeUntillDead)
        {
            Destroy(gameObject);
        }

        TimeAlive += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void LookForTarget()
    {
        if (players.Count == 0) //todo: fix?
        {
            return;
        }

        float[] distances = new float[players.Count];
        for (int i = 0; i < players.Count; i++)
        {
            distances[i] = Vector3.Distance(players[i].position, transform.position);
        }

        int minInd = Array.IndexOf(distances, distances.Min());
        target = players[minInd];
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isTouchingWall && col.gameObject.name == "Wall Tile")
        {
            isTouchingWall = true;
            TimeOnWall = 0;
            curTile = col.gameObject;
        }

        if (col.gameObject.CompareTag("Runner"))
        {
            players.Remove(col.gameObject.transform);
            Destroy(col.gameObject);
            print("killed player");
            if (players.Count == 0) //TODO: NOT DETECTING 
            {
                print("endGame");
                SceneManager.LoadScene("TalScene");
            }

            Destroy(gameObject);
        }
    }
}