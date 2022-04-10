using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItTimerSceneManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private List<GameObject> playersList;

    #endregion

    #region Fields

    private List<GameObject> _canSelectedAsIt;
    private List<GameObject> _currentRoundsIts;

    public List<GameObject> CurrentRoundIts
    {
        get => _currentRoundsIts;
    }

    private int _roundsCounter;
    private int _numberOfPlayers;
    public int NumberOfPlayers
    {
        get => _numberOfPlayers;
    }
    private Color _itColor;

    #endregion

    #region Events

    public event Action RoundStart;
    public event Action RoundEnd;
    public event Action EndGame;

    #endregion

    #region MonoBehaviour

    private void OnEnable()
    {
        RoundStart += ResetLists;
        RoundStart += RandomizeNewIt;
        RoundEnd += StartNewRound;
        EndGame += CheckWinner;
    }

    private void OnDestroy()
    {
        RoundStart -= ResetLists;
        RoundStart -= RandomizeNewIt;
        RoundEnd -= StartNewRound;
        EndGame -= CheckWinner;
    }

    private void Start()
    {
        _canSelectedAsIt = new List<GameObject>(playersList);
        _roundsCounter = 0;
        _numberOfPlayers = playersList.Count;
        _itColor = Color.red;
        StartNewRound();
    }

    #endregion

    #region Methods

    private void ResetLists()
    {
        _currentRoundsIts = new List<GameObject>();
        foreach (var player in playersList)
        {
            ChangeToRunner(player);
        }
    }

    private void RandomizeNewIt()
    {
        int randomInt = Random.Range(0, _canSelectedAsIt.Count);
        ChangeToIt(_canSelectedAsIt[randomInt]);
        _canSelectedAsIt.Remove(_canSelectedAsIt[randomInt]);
    }

    public void ChangeToIt(GameObject player)
    {
        player.GetComponent<SpriteRenderer>().color = _itColor;
        player.gameObject.tag = "It";
        player.GetComponent<ItTimerPlayerManager>().PlayerState = ItTimerPlayerManager.State.It;
        _currentRoundsIts.Add(player);
    }

    public void ChangeToRunner(GameObject player)
    {
        player.GetComponent<SpriteRenderer>().color = player.GetComponent<ItTimerPlayerManager>().InitialColor;
        player.gameObject.tag = "Runner";
        player.GetComponent<ItTimerPlayerManager>().PlayerState = ItTimerPlayerManager.State.Runner;
    }

    #endregion

    private void StartNewRound()
    {
        _roundsCounter += 1;
        if (_roundsCounter <= _numberOfPlayers)
            RoundStart.Invoke();
        else
            EndGame.Invoke();
    }

    private void CheckWinner()
    {
        float curBest = Int32.MaxValue;
        GameObject curWinner = null;
        foreach (var player in playersList)
        {
            float playerCounter = player.GetComponent<ItTimerPlayerManager>().ItCounter;
            print(player.gameObject.name + " 'It' time is: " + playerCounter);
            if (playerCounter < curBest)
            {
                curWinner = player;
                curBest = playerCounter;
            }
        }
        print("Winner is: " + curWinner.gameObject.name);
    }

    public void invokeRoundEnd()
    {
        RoundEnd.Invoke();
    }
}