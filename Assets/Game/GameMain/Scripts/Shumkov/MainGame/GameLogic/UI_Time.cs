using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Time : MonoBehaviour
{

    //時間スライダー　/　HPバー
    [Header("Time Slider")]
    public GameObject hpBar;
    Vector3 hpScale;
    [Header("Time")]
    public float maxTime = 180;
    public float currentTime = 0;
    //プレイヤー
    [Header("Player Attributes")]
    public GameObject player;
    public Transform playerCamera;


    //外部依存関係
    ResultDirector resultDirector;
    [Header("For use in \"Result Camera\"")]
    public bool gameOver = false;

    void Start()
    {
        resultDirector = GetComponent<ResultDirector>();
    }

    void Update()
    {
        if (!gameOver)
        {
            resultDirector.remainingTime = Mathf.Max(0, (int)(maxTime - currentTime));
            //制限時間の計算
            if (currentTime <= 180)
            {
                currentTime += Time.deltaTime;
                hpScale = hpBar.transform.localScale;
                hpScale.x = Mathf.Lerp(hpScale.x, (maxTime - currentTime) / maxTime, 0.1f);
                hpBar.transform.localScale = hpScale;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                if (!gameOver)
                {
                    resultDirector.remainingTime = 0;
                    currentTime = 180;
                    hpScale.x = 0;
                    hpBar.transform.localScale = hpScale;
                    gameOver = true;
                    playerCamera.parent = null;
                    Destroy(player);
                }

            }
        }
        else
        {
            //ゲームオバー
            Cursor.lockState = CursorLockMode.None;
            playerCamera.parent = null;
            playerCamera.GetComponent<PlayerAimCamera>().enabled = false;
            playerCamera.GetComponent<ResultCamera>().enabled = true;
        }
    }

}
