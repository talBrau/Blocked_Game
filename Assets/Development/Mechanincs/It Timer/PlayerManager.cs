using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private SceneManager _sceneManager;

    #endregion

    #region Fields

    public enum State
    {
        It,
        Runner
    }

    private State _state;

    public State PlayerState
    {
        get => _state;
        set => _state = value;
    }

    private float _itCounter;

    public float ItCounter
    {
        get => _itCounter;
        set => _itCounter = value;
    }

    private Color _initialColor;

    public Color InitialColor
    {
        get => _initialColor;
    }

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _initialColor = GetComponent<SpriteRenderer>().color;
        _state = State.Runner;
    }

    private void Update()
    {
        if (_state == State.It)
        {
            _itCounter += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("It") && other.gameObject.CompareTag("Runner"))
        {
            // change other game object to "It" state
            _sceneManager.ChangeToIt(other.gameObject);
            if (_sceneManager.CurrentRoundIts.Count == _sceneManager.NumberOfPlayers)
                Invoke(nameof(NewRoundDelay),0.5f);
        }
    }
    private void NewRoundDelay()
    {
        _sceneManager.invokeRoundEnd();
    }
    #endregion
}