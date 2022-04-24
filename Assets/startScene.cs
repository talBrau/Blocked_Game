using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGame(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        SceneManager.LoadScene("Prototype");
    }
}
