using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //変数
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    [Header("Moving Properties")]
    public float        speed;
    public float        dashX;
    //プレイヤーのコンポーネント
    [SerializeField] CharacterController playerController;
    [SerializeField] GameObject walkingSound;
    //その他
    UI_Time uiTime;
    Gamepad gamepad;
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //コンポネントの初期化
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    private void Start()
    { 
        uiTime = GameObject.Find("GameManager").GetComponent<UI_Time>();
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //入力
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    float inputHorizontal, inputVertical;

    private void Update()
    {
        //接続されているゲームパッドを貰います
        if(Gamepad.current!=null)
            gamepad = Gamepad.current;

        //ゲームオーバーの場合、入力をしない
        if (uiTime.gameOver)
        {
            inputVertical = 0f;
            inputHorizontal = 0f;
            return;
        }
        //入力
        inputVertical = gamepad.leftStick.y.ReadValue();
        inputHorizontal = gamepad.leftStick.x.ReadValue();
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //移動
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    private void FixedUpdate()
    {
        //移動のベクトル
        Vector3 direction = new Vector3(inputVertical, 0f, inputHorizontal).normalized;
        //このベクトルはゼロに近い値でなければ
        if (direction.magnitude >= 0.1)
        {
            //音響効果を有効
            walkingSound.SetActive(true);
            //アニメーションを設定
            GetComponent<Animator>().SetBool("runFlag", true);
            //移動の方向を計算する
            Vector3 move = transform.right * inputHorizontal + transform.forward * inputVertical;
            //移動させる
            playerController.Move(move * speed * Time.deltaTime);
            //プログラム終了
            return;
        }

        //音響効果を有効
        walkingSound.SetActive(false);
        //アニメーションを設定
        GetComponent<Animator>().SetBool("runFlag", false); 
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
}
