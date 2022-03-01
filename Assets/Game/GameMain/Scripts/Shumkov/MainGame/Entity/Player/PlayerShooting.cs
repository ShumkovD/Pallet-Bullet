using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class PlayerShooting : MonoBehaviour
{
    //変数
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    //弾の種類
    [Header("Bullet Type")]
    [SerializeField] GameObject[] bullet;
    [SerializeField] GameObject superBullet;
    //弾が発生している場所
    [Header("Gun Position")]
    [SerializeField] GameObject barrel;
    //弾のプロパティ
    [Header("Bullet Properties")]
    public float attackDamage;
    [SerializeField] float attackSpeed;
    [SerializeField] Vector3 speed;
    //パワーアップのタイマー
    [Header("PowerUp duration")]
    [SerializeField] float powerUpTimer;
    float time;
    //フラッグ
    public bool poweredUp = false;
    bool allowFire = true;
    //敵を倒すボーナス
    [SerializeField] float bonusSetting;
    //撃っている弾の情報
    int index = 0;
    public GameObject shootingBullet;
    //音響効果
    [SerializeField] AudioSource audioShooting;
    //入力
    Gamepad gamepad;
    //その他
    UI_Time timeManager;
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //初期化
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    void Start()
    {
        timeManager = GameObject.Find("GameManager").GetComponent<UI_Time>();
        shootingBullet = bullet[0];
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //入力
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    void Update()
    {
        //入力
        if (Gamepad.current != null)
            gamepad = Gamepad.current;

        //ゲームオーバー
        if (timeManager.gameOver)
        {
            StopAllCoroutines();
            return;
        }

        //弾の切り替え（右）
        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            OnButtonDown_rightChange();
        }

        //弾の切り替え（左）
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
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    
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
        //レイを指して
        if (Physics.Raycast(ray, out hit))
        {
            //当たったオブジェクトを確認する
            targetPoint = hit.point;
            //敵の場合、補助あり
            if (hit.transform.gameObject.tag == "Enemy")
            {
                targetPoint = hit.point;
                target = hit.transform.gameObject;
            }
            //マップだったら、補助なし
            else　if (hit.transform.gameObject.tag == "Terrain")
            {
                targetPoint = hit.point;
            }
            //ターゲットがない
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
    //弾の切り替え
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
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

    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

}
