using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //�ϐ�
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    [Header("Moving Properties")]
    public float        speed;
    public float        dashX;
    //�v���C���[�̃R���|�[�l���g
    [SerializeField] CharacterController playerController;
    [SerializeField] GameObject walkingSound;
    //���̑�
    UI_Time uiTime;
    Gamepad gamepad;
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //�R���|�l���g�̏�����
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    private void Start()
    { 
        uiTime = GameObject.Find("GameManager").GetComponent<UI_Time>();
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //����
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    float inputHorizontal, inputVertical;

    private void Update()
    {
        //�ڑ�����Ă���Q�[���p�b�h��Ⴂ�܂�
        if(Gamepad.current!=null)
            gamepad = Gamepad.current;

        //�Q�[���I�[�o�[�̏ꍇ�A���͂����Ȃ�
        if (uiTime.gameOver)
        {
            inputVertical = 0f;
            inputHorizontal = 0f;
            return;
        }
        //����
        inputVertical = gamepad.leftStick.y.ReadValue();
        inputHorizontal = gamepad.leftStick.x.ReadValue();
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //�ړ�
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    private void FixedUpdate()
    {
        //�ړ��̃x�N�g��
        Vector3 direction = new Vector3(inputVertical, 0f, inputHorizontal).normalized;
        //���̃x�N�g���̓[���ɋ߂��l�łȂ����
        if (direction.magnitude >= 0.1)
        {
            //�������ʂ�L��
            walkingSound.SetActive(true);
            //�A�j���[�V������ݒ�
            GetComponent<Animator>().SetBool("runFlag", true);
            //�ړ��̕������v�Z����
            Vector3 move = transform.right * inputHorizontal + transform.forward * inputVertical;
            //�ړ�������
            playerController.Move(move * speed * Time.deltaTime);
            //�v���O�����I��
            return;
        }

        //�������ʂ�L��
        walkingSound.SetActive(false);
        //�A�j���[�V������ݒ�
        GetComponent<Animator>().SetBool("runFlag", false); 
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
}
