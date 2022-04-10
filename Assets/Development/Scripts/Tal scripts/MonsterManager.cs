using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private float timeToSpwan;
    [SerializeField] private GameObject monsterPrefab;

    private float minRadius;
    private float maxRadius;
    //TODO: PUT PLAYERS LIST HERE

    // Start is called before the first frame update
    void Start()
    {
        Vector3 centerRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 0));
        minRadius = centerRight.x;
        maxRadius = 1f * minRadius;
        StartCoroutine(spawnMonster(3));
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private IEnumerator spawnMonster(float sec)
    {
        while (true)
        {
            float distance= Random.Range( minRadius, maxRadius );
            float angle= Random.Range( -Mathf.PI, Mathf.PI );
            Vector2 spawnPos = transform.position;
            spawnPos += new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            Instantiate(monsterPrefab, spawnPos, quaternion.identity,transform);
            print("spawn");
            yield return new WaitForSeconds(sec);

        }
        
    }
}
