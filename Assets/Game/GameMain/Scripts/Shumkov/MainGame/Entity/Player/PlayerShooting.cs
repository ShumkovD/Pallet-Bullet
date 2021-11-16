using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class PlayerShooting : MonoBehaviour
{
    //弾の種類
    public GameObject[] bullet;
    public GameObject superBullet;
    //弾が発生している場所
    public GameObject barrel;
    //弾のプロパティ
    public float attackDamage;
    public float attackSpeed;
    public Vector3 speed;

    public float powerUpTimer;
    float time;
 
    //フラッグ
    public bool poweredUp;
    bool allowFire = true;

    //敵を殺すボーナス
    public float bonusSetting;


    
    //撃っている弾の情報
    int index = 0;
    public GameObject shootingBullet;
    //ゲームパッド
    Gamepad gamepad;

    //外部依存関係
    UI_Time timeManager;
    public AudioSource audioShooting;
    void Start()
    {
        timeManager = GameObject.Find("GameManager").GetComponent<UI_Time>();
        audioShooting = GetComponent<AudioSource>();
        shootingBullet = bullet[0];
    }
    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
            gamepad = Gamepad.current;

        if (!timeManager.gameOver)
        {
            //弾の切り替え
            if (gamepad.rightTrigger.wasPressedThisFrame)
            {
                OnButtonDown_rightChange();
            }
            if (gamepad.leftTrigger.wasPressedThisFrame)
            {
                OnBUttonDown_leftChange();
            }

            //撃つ
            bool canShoot = gamepad.rightShoulder.wasPressedThisFrame;
            Shooting(canShoot);

            //パワーアップの管理
            if (poweredUp)
            {
                shootingBullet = superBullet;
                time += Time.deltaTime;
                if (time >= powerUpTimer)
                {
                    shootingBullet = bullet[0];
                    poweredUp = false;
                    time = 0;
                    attackDamage *= 0.5f;
                }
            }
        }
        else StopAllCoroutines();
    }

    //撃つ
    public void Shooting(bool canShoot)
    {
        if (allowFire && canShoot)
        {
            audioShooting.Play();
            GetComponent<Animator>().Play("Gunplay", 0);
            StartCoroutine(ShootingMethod());
        }
    }
    //撃ち方
    IEnumerator ShootingMethod()
    {
        allowFire = false;
        //レイの発生
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        //目的についたか確認のため
        GameObject target = null;
        Vector3 targetPoint;
        
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
            if (hit.transform.gameObject.tag == "Enemy")
            {
                targetPoint = hit.point;
                target = hit.transform.gameObject;
            }
            else
                if (hit.transform.gameObject.tag == "Terrain")
            {
                targetPoint = hit.point;
            } 
        else target = null;
        }
        else
            targetPoint = ray.GetPoint(1000);
        //弾の発生
        var shootBullet = Instantiate(shootingBullet, barrel.transform.position, barrel.transform.rotation);
        speed = (targetPoint - barrel.transform.position).normalized * 70f;
        //弾のプロパティの設定
        AttackDamage bulletProperty = shootBullet.GetComponent<AttackDamage>();
        bulletProperty.damage = attackDamage;
        bulletProperty.moveVector = targetPoint;
        bulletProperty.target = target;
        shootBullet.GetComponent<Rigidbody>().velocity = speed;
        //撃つアニメーションのフラッグ
        GetComponent<Animator>().SetBool("shootingFlag", false);
        //リロード時間
        yield return new WaitForSeconds(attackSpeed);
        allowFire = true;
    }

    public void OnBUttonDown_leftChange()
    {
        index--;
        if (index == -1)
            index = 2;
        shootingBullet = bullet[index];
    }
    public void OnButtonDown_rightChange()
    {
        index++;
        index %= bullet.Length;
        shootingBullet = bullet[index];
    }
}
