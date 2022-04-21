using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private int wallTilePrice;
    [SerializeField] private int explodingTilePrice;
    [SerializeField] private GameObject monsterManager;
    [SerializeField] private PlayersSpawnManager playersSpawnManager;

    #endregion

    #region Fields

    public static float Score;
    public static int WallTilePrice;
    public static int ExplodingTilePrice;
    private bool onBoarding = true;
    private int _readyCounter;

    #endregion

    #region MonoBehaviour

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
    public void increaseReadyCounter()
    {
        _readyCounter += 1;
        if (_readyCounter == playersSpawnManager.playersSpawned)
        {
            onBoarding = false;
            monsterManager.GetComponent<MonsterManager>().stopOnBoarding();
        }
    }
    
}