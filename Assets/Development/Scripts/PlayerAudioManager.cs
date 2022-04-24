using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private AudioSource buyTile;
    [SerializeField] private AudioSource moveTile;
    [SerializeField] private AudioSource reviveFriend;
    [SerializeField] private AudioSource GotHit;

    #endregion

    public void playBuyTile()
    {
        buyTile.Play(0);
    }


    public void playMoveTile()
    {
        moveTile.Play(0);
    }

    public void playReviveFriend()
    {
        reviveFriend.Play(0);
    }
    
    public void playGotHit()
    {
        GotHit.Play(0);
    }
}
