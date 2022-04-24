using System.Collections;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float radius;
    [SerializeField] private float explosionForce;
    [SerializeField] private AudioSource boomSfx;

    public void explodeTile()
    {
        Instantiate(explosionEffect,transform.position,transform.rotation);
        Collider2D[] surrounding = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var col in surrounding)
        {
            if (col.gameObject.CompareTag("Monster"))
            {
                GameManager.Score += GameManager.scoreWhenKillingMonster;
                Destroy(col.gameObject);
            }

            if (col.CompareTag("Player"))
            {
                var rb = col.gameObject.GetComponent<Rigidbody2D>();
                Vector2 dir = rb.position - new Vector2(transform.position.x,transform.position.y);
                rb.AddForce(dir*explosionForce);
            }
        }

        boomSfx.Play();
        GetComponent<SpriteRenderer>().sprite = null;
        Invoke("waitDestroy",0.5f);
    }

    private void waitDestroy()
    {
        Destroy(gameObject);
    }
}
