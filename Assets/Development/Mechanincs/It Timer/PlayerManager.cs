using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private SceneManager _sceneManager;
    [SerializeField] private Grid _grid;

    #endregion

    #region Fields

    public enum State
    {
        It,
        Runner
    }

    private State _state;
    Tilemap tilemap;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            var map = other.collider.GetComponent<Tilemap>();
            var grid = map.layoutGrid;

            // Find the coordinates of the tile we hit.
            var contact = other.GetContact(0);
            Vector3 contactPoint = contact.point;
            print(contactPoint);
            Vector3Int cell = grid.WorldToCell(contactPoint);
            // Extract the tile asset at that location.
            var tile = map.GetTile(cell);
            if(tile == null)
                print("none");
            else 
                map.SetTile(cell,null);
        }
    }

    private void NewRoundDelay()
    {
        _sceneManager.invokeRoundEnd();
    }
    #endregion
}