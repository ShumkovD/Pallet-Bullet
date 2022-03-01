using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{
    //�ϐ�
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    [Header("Gravity Properties")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravitationEffect;

    //�L�����v���p�e�B
    [Header("Character Properties")]
    [SerializeField] float playerThickness;

    //��������
    [SerializeField] GameObject walkingSound;
    //�v���C���[�̃R���|�l���g
    [SerializeField] CharacterController playerController;
    //����
    Gamepad gamepad;
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //����
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    bool isJumping;
    bool isGrounded;
    Vector3 gravity;

    // Update is called once per frame
    void Update()
    {
        //�ڑ�����Ă���Q�[���p�b�h��Ⴂ�܂�
        if (Gamepad.current != null)
            gamepad = Gamepad.current;
        //�n�ʂ̊m�F
        isGrounded = Physics.CheckSphere(transform.position, playerThickness, groundMask);
        //�W�����v����
        isJumping = gamepad.aButton.wasPressedThisFrame�@|| gamepad.aButton.isPressed;
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    
    //�W�����v
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    void FixedUpdate()
    {
        //���ł��Ȃ��ꍇ
        if (isGrounded&&isJumping)
        {
            gravity.y = Mathf.Sqrt(-0.5f * gravitationEffect * 3f) * Time.deltaTime;
        }
        
        //���ł���ꍇ
        if (!isGrounded)
        {
            //�����������ʂ𖳌�
            walkingSound.SetActive(false);
            //�d�͂̌v�Z
            gravity.y += gravitationEffect * Time.deltaTime * Time.deltaTime;
        }

        //�ړ�
        playerController.Move(gravity);
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
}
