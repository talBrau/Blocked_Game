using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerManager>().IsStandingOnButton = true;
            GetComponent<SpriteRenderer>().sprite = _sprites[1];
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sprite = _sprites[0];
            other.gameObject.GetComponent<PlayerManager>().IsStandingOnButton = false;
        }    
    }
}
