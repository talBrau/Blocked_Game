using UnityEngine;

public class BaseManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private int baseHealth = 5;

    #endregion

    #region MonoBehaviour

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            if (!GameManager.GameOverFlag)
                baseHealth -= 1;
            Destroy(other.gameObject);
            if (baseHealth == 0)
                GameManager.InvokeGameOver();
        }
    }

    #endregion
    
    #region Methods
    
    #endregion
}
