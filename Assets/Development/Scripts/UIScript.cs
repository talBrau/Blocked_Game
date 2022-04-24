using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;


public class UIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI Highscore;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Highscore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
        
    }

    private void Update()
    {
        score.text = math.round(GameManager.Score).ToString();
        if (GameManager.Score > PlayerPrefs.GetInt("highscore", 0))
        {
            Highscore.text = math.round(GameManager.Score).ToString();
        }
        else
        {
            Highscore.text =  PlayerPrefs.GetInt("highscore", 0).ToString();
        }
    }
    
}
