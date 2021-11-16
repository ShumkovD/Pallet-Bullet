using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

public class mainUI : MonoBehaviour
{
    //public GameObject panelStart;//スタート画面
    public GameObject panelOption;//オプション画面
    public Button startButton;

    float delta = 0f;

    //デモシーンへの移行
    float demoTimer;

    //option画面切り替えの猶予時間
    bool isOption = false;
    float changeTime = 0;

    Gamepad gamepad;

    public GameObject fadeOut;

    public static float volValue;
    string path;

    [Header("Debug")]
    public bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        demoTimer = 0;
            
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
            gamepad = Gamepad.current;
        Debug.Log(demoTimer);
        Option();


        //デモシーンへの遷移
        if (!isOption)//入力がない場合タイマーをカウント
        {
            if (Input.anyKey)
            {
                demoTimer = 0f;
            }
            else
            {
                demoTimer += Time.deltaTime;


                if (demoTimer > 10.0f)//10秒でシーン移行
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
        else
        {
            demoTimer = 0f;
        }

        if(gamepad.bButton.wasPressedThisFrame)
        {
            OnButtonDown_Start();
        }

        if(gamepad.startButton.isPressed)
        {
            delta += Time.deltaTime;

            if(delta > 3.0f)
            {
                delta = 0;
                ApplicationQuit();
            }
        }


    }
    //startボタンでゲームシーンへ遷移
    public void OnButtonDown_Start()
    {
        OnButtonDown_start();
    }
    public void OnButtonDown_start()
    {
        fadeOut.SetActive(true);
    }
    //optionを閉じる
    public void OnButtonDown_close()
    {
        panelOption.SetActive(false);
    }

    //オプション画面を管理する関数
    public void Option()
    {
        // Escキーでオプション画面
        if (gamepad.bButton.wasPressedThisFrame && !isOption&& debug)
        {
            panelOption.SetActive(true);
            demoTimer = 0;
            isOption = true;
        }

        if (isOption)
        {
            changeTime += Time.deltaTime;

            if (changeTime > 0.05 && gamepad.bButton.wasPressedThisFrame)
            {
                panelOption.SetActive(false);
                changeTime = 0;
                demoTimer = 0;
                isOption = false;
            }
        }
    }


    void ApplicationQuit()
    {
        Application.Quit();
    }
}
