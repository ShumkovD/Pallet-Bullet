using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{
    //変数
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    [Header("Gravity Properties")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravitationEffect;

    //キャラプロパティ
    [Header("Character Properties")]
    [SerializeField] float playerThickness;

    //音響効果
    [SerializeField] GameObject walkingSound;
    //プレイヤーのコンポネント
    [SerializeField] CharacterController playerController;
    //入力
    Gamepad gamepad;

    UI_Time uI_Time;
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //入力
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    bool isJumping;
    bool isGrounded;
    Vector3 gravity;

    private void Start()
    {
        uI_Time = GameObject.Find("GameManager").GetComponent<UI_Time>();
    }

    void Update()
    {
        //接続されているゲームパッドを貰います
        if (Gamepad.current != null)
            gamepad = Gamepad.current;

        if (uI_Time.gameOver)
            return;

        //地面の確認
        isGrounded = Physics.CheckSphere(transform.position, playerThickness, groundMask);
        //ジャンプ入力
        isJumping = gamepad.aButton.wasPressedThisFrame　|| gamepad.aButton.isPressed;
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    
    //ジャンプ
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    void FixedUpdate()
    {
        //飛んでいない場合
        if (isGrounded&&isJumping)
        {
            gravity.y = Mathf.Sqrt(-0.5f * gravitationEffect * 3f) * Time.deltaTime;
        }
        
        //飛んでいる場合
        if (!isGrounded)
        {
            //歩く音響効果を無効
            walkingSound.SetActive(false);
            //重力の計算
            gravity.y += gravitationEffect * Time.deltaTime * Time.deltaTime;
        }

        //移動
        playerController.Move(gravity);
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
}
