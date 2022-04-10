using UnityEngine;
[CreateAssetMenu(fileName = "ActionKeys", menuName = "Action Keys", order = 51)]
public class PlayerActionKeys : ScriptableObject
{
    [SerializeField] private KeyCode instantiateTile ;
    public KeyCode InstantiateTile => instantiateTile;

    [SerializeField] private KeyCode moveTile;
    public KeyCode MoveTile => moveTile;
    
}