using UnityEngine;
[CreateAssetMenu(fileName = "MovementKeys", menuName = "Movement Keys", order = 51)]
public class PlayerMovementKeys : ScriptableObject
{
    [SerializeField] private KeyCode up;
    public KeyCode Up => up;

    [SerializeField] private KeyCode down;
    public KeyCode Down => down;


    [SerializeField] private KeyCode right;
    public KeyCode Right => right;

    [SerializeField] private KeyCode left;
    public KeyCode Left => left;
}