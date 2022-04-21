using UnityEngine;

public class sceneManger : MonoBehaviour
{
    private bool onBoarding;
    [SerializeField] private GameObject monsterManager;
    [SerializeField] private PlayersSpawnManager playersSpawnManager;
    private int _readyCounter;

    public void increaseReadyCounter()
    {
        _readyCounter += 1;
        if (_readyCounter == playersSpawnManager.playersSpawned)
        {
            onBoarding = false;
            monsterManager.GetComponent<MonsterManager>().stopOnBoarding();
        }
    }
    
    private void Awake()
    {
        onBoarding = true;
    }
    
}
