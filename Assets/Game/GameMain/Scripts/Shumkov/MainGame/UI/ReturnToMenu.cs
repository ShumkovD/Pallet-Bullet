using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public GameObject fadeOut;
    Gamepad gamepad;
    private void Update()
    {
        if (Gamepad.current != null)
            gamepad = Gamepad.current;

        if (gamepad.bButton.wasPressedThisFrame)
            ReturnToMainMenu();
    }
    public void ReturnToMainMenu()
    {
        fadeOut.SetActive(true);
    }
}
