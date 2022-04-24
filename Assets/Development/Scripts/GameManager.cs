using System;
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
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWonUI;
    [SerializeField] private TextMeshProUGUI gameWonScore;
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
        GameOverFlag = false;
        gameOverUI.SetActive(false);
        gameWonUI.SetActive(false);
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
        SceneManager.LoadScene("Prototype");
    }
    private void showGameOverScreen()
    {
        gameOverUI.SetActive(true);
        GameOverFlag = true;
    }

    private void showGameWonScreen()
    {
        gameWonUI.SetActive(true);
        gameWonScore.text = Math.Round(Score).ToString();
        if (Score >= PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", (int) math.round(Score));
            newHighScore.SetActive(true);
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