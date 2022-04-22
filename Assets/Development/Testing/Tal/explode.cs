using UnityEngine;

public class explode : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float radius;
    [SerializeField] private float explosionForce;

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
