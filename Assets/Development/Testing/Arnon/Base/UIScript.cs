using TMPro;
using Unity.Mathematics;
using UnityEngine;


public class UIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    private void Update()
    {
        score.text = math.round(GameManager.Score).ToString();
    }
}
