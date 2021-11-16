using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Moving Properties")]
    public float        speed;
    public float        dashX;
    //地上の確認用
    CharacterController playerController;
    UI_Time uiTime;
    public GameObject walkingSound;
    Gamepad gamepad;

    // Start is called before the first frame update
    private void Start()
    { 
        uiTime = GameObject.Find("GameManager").GetComponent<UI_Time>();
        playerController = GetComponent<CharacterController>();
    }

    
    float inputHorizontal, inputVertical;
    // Update is called once per frame
    private void Update()
    {
        if(Gamepad.current!=null)
            gamepad = Gamepad.current;
        
        if (!uiTime.gameOver)
        {

            inputVertical = gamepad.leftStick.y.ReadValue();
            inputHorizontal = gamepad.leftStick.x.ReadValue();
        }
        else
        {
            inputVertical = 0f;
            inputHorizontal = 0f;
        }
    }


    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(inputVertical, 0f, inputHorizontal).normalized;
        //このベクトルの長さ＞＝0.１
        if (direction.magnitude >= 0.1)
        {
            walkingSound.SetActive(true);
            GetComponent<Animator>().SetBool("runFlag", true);
            Vector3 move = transform.right * inputHorizontal + transform.forward * inputVertical;
            playerController.Move(move * speed * Time.deltaTime);
        }
        else
        {
            walkingSound.SetActive(false);
            GetComponent<Animator>().SetBool("runFlag", false);
        }
    }
}
