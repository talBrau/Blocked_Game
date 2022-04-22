using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toturial : MonoBehaviour
{
    [SerializeField] private GameObject leftKey;
    [SerializeField] private GameObject rightKey;
    [SerializeField] private GameObject upKey;
    [SerializeField] private GameObject lbRbKey;
    [SerializeField] private GameObject moveKey;

    public enum Keys
    {
        LeftKey,
        RightKey,
        UpKey,
        LbRbKey,
        MoveKey
    }

    public void ShowKey(Keys key)
    {
        if (key == Keys.LeftKey)
            leftKey.SetActive(true);
        if (key == Keys.RightKey)
            rightKey.SetActive(true);
        if (key == Keys.UpKey)
            upKey.SetActive(true);
        if (key == Keys.LbRbKey)
            lbRbKey.SetActive(true);
        if (key==Keys.MoveKey)
            moveKey.SetActive(true);
    }
    public void HideKey(Keys key)
    {
        if (key == Keys.LeftKey)
            leftKey.SetActive(false);
        if (key == Keys.RightKey)
            rightKey.SetActive(false);
        if (key == Keys.UpKey)
            upKey.SetActive(false);
        if (key == Keys.LbRbKey)
            lbRbKey.SetActive(false);

        if (key==Keys.MoveKey)
            moveKey.SetActive(false);
    }
    
}
