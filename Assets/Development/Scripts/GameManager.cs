using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private int wallTilePrice;
    [SerializeField] private int explodingTilePrice;
    [SerializeField] private GameObject monsterManager;
    [SerializeField] private PlayersSpawnManager playersSpawnManager;
     private GameObject gameOverUI;
     private GameObject gameWonUI;
     private GameObject gameWonNewHighScoreUI;
    [SerializeField] private TextMeshProUGUI gameWonScore;
    [SerializeField] private TextMeshProUGUI gameWonHighScore;
    [SerializeField] private TextMeshProUGUI PrevHighScore;
    [SerializeField] private GameObject newHighScore;

    #endregion

    #region Fields

    public static float Score;
    public static int WallTilePrice;
    public static int ExplodingTilePrice;
    public static bool GameOverFlag;
    public static bool onBoarding = true;
    private int _readyCounter;

    #endregion

    #region Events
    public static event Action GameOver;
    public static event Action Bale;
    public static event Action ResetGame;
    
    
    #endregion
    
    #region MonoBehaviour

    private void OnEnable()
    {
        GameOver += showGameOverScreen;
        ResetGame += loadScene;
        Bale += showGameWonScreen;
    }

    private void OnDestroy()
    {
        GameOver -= showGameOverScreen;
        ResetGame -= loadScene;
        Bale += showGameWonScreen;
    }

    private void Awake()
    {
        WallTilePrice = wallTilePrice;
        ExplodingTilePrice = explodingTilePrice;
    }

    private void Start()
    {
        gameOverUI = GameObject.Find("GameOver Screen");
        gameWonUI = GameObject.Find("GameWon Screen");
        gameWonNewHighScoreUI = GameObject.Find("Game Won High score");
        GameOverFlag = false;
        if(!gameOverUI)
            print("deadGuiOnstart");
        gameOverUI.SetActive(false);
        gameWonUI.SetActive(false);
        gameWonNewHighScoreUI.SetActive(false);
        onBoarding = true;
        Score = 0;
        _readyCounter = 0;
    }

    private void Update()
    {
        if (!onBoarding && !GameOverFlag)
        {
            Score += Time.deltaTime;
        }
    }

    #endregion

    #region Methods

    private void loadScene()
    {
        GameObject.Find("Canvas").SetActive(false);
        SceneManager.LoadScene("StartScene");
    }
    
    private void showGameOverScreen()
    {
        gameOverUI.SetActive(true);
        GameOverFlag = true;
    }

    private void showGameWonScreen()
    {
        gameWonScore.text = Math.Round(Score).ToString();
        var prevHighScore = PlayerPrefs.GetInt("highscore", 0);
        if (Score >=prevHighScore)
        {
            PlayerPrefs.SetInt("highscore", (int) math.round(Score));
            PrevHighScore.text = prevHighScore.ToString();
            gameWonHighScore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
            gameWonNewHighScoreUI.SetActive(true);
        }
        else
        {
            gameWonHighScore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
            if (!gameWonUI)
            {
                print("deadGui");
            }
            gameWonUI.SetActive(true);
        }
        GameOverFlag = true;
    }
    public void increaseReadyCounter()
    {
        _readyCounter += 1;
        if (_readyCounter == playersSpawnManager.playersSpawned)
        {
            onBoarding = false;
            monsterManager.GetComponent<MonsterManager>().stopOnBoarding();
        }
    }

    public static void InvokeGameOver()
    {
        GameOver?.Invoke();
    }

    public static void InvokeResetGame()
    {
        ResetGame?.Invoke();
    }
    
    public static void InvokeBale()
    {
        Bale?.Invoke();
    }
    

    #endregion

}