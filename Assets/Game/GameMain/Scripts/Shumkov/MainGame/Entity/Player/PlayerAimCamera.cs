using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimCamera : MonoBehaviour
{
    
    [Header("Camera Position")]
    public GameObject    player;
    
    float rotateY = 0f;
    [Header("Camera Sensitivity")]
    public float         sensitivityUsedX = 100f;
    public float         sensitivityUsedY = 50f;
    UI_Time uI_Time;

    Gamepad gamepad;
    private void Start()
    {
        uI_Time = GameObject.Find("GameManager").GetComponent<UI_Time>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    private void LateUpdate()
    {
        if (Gamepad.current != null)
            gamepad = Gamepad.current;
        //マインキャラがいるか確認
        if (player != null)
        {
            float mouseX = gamepad.rightStick.x.ReadValue() * sensitivityUsedX * Time.deltaTime;
            float mouseY = gamepad.rightStick.y.ReadValue() * Time.deltaTime * sensitivityUsedY;

            //Y入力を制限するために：
            rotateY -= mouseY;
            rotateY = Mathf.Clamp(rotateY, -20f, 20f);

            //カメラの回転（ｘ軸）                       ↓X軸
            transform.localRotation = Quaternion.Euler(rotateY, 0, 0);
            //プレイヤーの回転（ｘ軸）　
            player.transform.Rotate(Vector3.up, mouseX);
        }
    }


}
