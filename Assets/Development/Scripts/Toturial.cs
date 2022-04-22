using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toturial : MonoBehaviour
{
    [SerializeField] private GameObject leftKey;
    [SerializeField] private GameObject rightKey;
    [SerializeField] private GameObject upKey;
    [SerializeField] private GameObject downKey;
    [SerializeField] private GameObject lbKey;
    [SerializeField] private GameObject rbKey;
    [SerializeField] private GameObject startKey;

    
    
    public enum Keys
    {
        LeftKey,
        RightKey,
        UpKey,
        DownKey,
        LbKey,
        RbKey,
        StartKey
    }

    public void ShowKey(Keys key)
    {
        if (key == Keys.LeftKey)
            leftKey.SetActive(true);
        if (key == Keys.RightKey)
            rightKey.SetActive(true);
        if (key == Keys.UpKey)
            upKey.SetActive(true);
        if (key == Keys.DownKey)
            downKey.SetActive(true);
        if (key == Keys.LbKey)
            lbKey.SetActive(true);
        if (key == Keys.RbKey)
            rbKey.SetActive(true);
        if (key == Keys.StartKey)
            startKey.SetActive(true);
    }
    public void HideKey(Keys key)
    {
        if (key == Keys.LeftKey)
            leftKey.SetActive(false);
        if (key == Keys.RightKey)
            rightKey.SetActive(false);
        if (key == Keys.UpKey)
            upKey.SetActive(false);
        if (key == Keys.DownKey)
            downKey.SetActive(false);
        if (key == Keys.LbKey)
            lbKey.SetActive(false);
        if (key == Keys.RbKey)
            rbKey.SetActive(false);
        if (key == Keys.StartKey)
            startKey.SetActive(false);
    }
    
}
