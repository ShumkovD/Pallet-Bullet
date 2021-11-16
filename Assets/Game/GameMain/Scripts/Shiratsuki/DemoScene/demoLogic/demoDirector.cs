using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class demoDirector : MonoBehaviour
{
    Gamepad gamepad;
    float delta = 0;
    float quitTime = 3.0f;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Press any button";
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current != null)
        {
            gamepad = Gamepad.current;
        }

        if(Input.anyKey)
        {
            ReturnToTitle();
        }

        if(gamepad.startButton.isPressed)
        {
            IsQuit();
        }
    }

    void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }

    void IsQuit()
    {
        delta += Time.deltaTime;
        if(delta > quitTime)
        {
            QuitExe();
        }
    }

    void QuitExe()
    {
        Application.Quit();
    }
}
