using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float radius;
    [SerializeField] private float explosionForce = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) //TODO: input system all players press key
        {
            explodeTile();
        }
    }

    public void explodeTile()
    {
        Instantiate(explosionEffect,transform.position,transform.rotation);
        Collider2D[] surrounding = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var col in surrounding)
        {
            if (col.gameObject.CompareTag("Monster"))
            {
                Destroy(col.gameObject);
            }

            if (col.CompareTag("Player"))
            {
                print("playerBoom");
                var rb = col.gameObject.GetComponent<Rigidbody2D>();
                Vector2 dir = rb.position - new Vector2(transform.position.x,transform.position.y);
                rb.AddForce(dir*explosionForce);
            }
        }
        print("boom");
        Destroy(gameObject);
    }
}
