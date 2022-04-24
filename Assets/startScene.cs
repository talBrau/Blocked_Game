using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
    [SerializeField] private GameObject startAnim;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject insruction0;
    [SerializeField] private GameObject insruction1;

    private bool _startFlag;
    private bool _startFlagSkip;

    public void StopAnim(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        startAnim.GetComponent<Animator>().SetTrigger("SetDark");
        startButton.GetComponent<Animator>().SetTrigger("SetDark");
        Invoke("disableObjects",1f);

    }
    private void disableObjects()
    {
        startAnim.SetActive(false);
        startButton.SetActive(false);
        insruction0.SetActive(true);
        _startFlagSkip = true;
    }
    
    private void disableObjects2()
    {
        insruction0.SetActive(false);
        _startFlag = true;
        insruction1.SetActive(true);
    }
    
    public void changeSlide(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        insruction0.GetComponent<Animator>().SetTrigger("SetDark");
        Invoke("disableObjects2",1f);
    }
    
    public void loadGame(InputAction.CallbackContext context)
    {
        if (!_startFlag)
            return;
        if (!context.performed)
        {
            return;
        }
        SceneManager.LoadScene("Prototype");
    }
    
    public void loadGameSkip(InputAction.CallbackContext context)
    {
        if (!_startFlagSkip)
            return;
        if (!context.performed)
        {
            return;
        }
        SceneManager.LoadScene("Prototype");
    }
}
