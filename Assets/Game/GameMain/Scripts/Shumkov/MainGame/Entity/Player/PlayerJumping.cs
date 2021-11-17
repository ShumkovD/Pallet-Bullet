using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{
    [Header("Gravity Properties")]
    public LayerMask groundMask;
    public float gravitationEffect;

    //キャラプロパティ
    [Header("Character Properties")]
    public float playerThickness;

    //歩く音
    public GameObject walkingSound;
    //コンポーネント関係
    CharacterController playerController;
    Gamepad gamepad;

    public bool isJumping;
    public bool isGrounded;
    Vector3 gravity;
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
            gamepad = Gamepad.current;
        //地面の確認
        isGrounded = Physics.CheckSphere(transform.position, playerThickness, groundMask);
        //ジャンプ入力
        isJumping = gamepad.aButton.wasPressedThisFrame|| gamepad.aButton.isPressed;
    }
    
    void FixedUpdate()
    {
        //地面についているかどうか確認
        if (isGrounded&&isJumping)
        {
            gravity.y = Mathf.Sqrt(-0.5f * gravitationEffect * 3f) * Time.deltaTime;
        }
 
        if (!isGrounded)
        {
            walkingSound.SetActive(false);
            //重力の計算
            gravity.y += gravitationEffect * Time.deltaTime * Time.deltaTime;
        }
        playerController.Move(gravity);
    }
}
