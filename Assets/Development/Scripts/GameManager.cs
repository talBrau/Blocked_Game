using System;
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

    #endregion

    #region Fields

    public static float Score;
    public static int WallTilePrice;
    public static int ExplodingTilePrice;
    public static bool GameOverFlag;
    private bool onBoarding = true;
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
    }

    private void OnDestroy()
    {
        GameOver -= showGameOverScreen;
        ResetGame -= loadScene;
    }

    private void Update()
    {
        if (!onBoarding)
        {
            Score += Time.deltaTime;
        }
    }

    private void Awake()
    {
        WallTilePrice = wallTilePrice;
        ExplodingTilePrice = explodingTilePrice;
    }

    #endregion

    #region Methods

    private void loadScene()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void showGameOverScreen()
    {
        gameOverUI.SetActive(true);
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