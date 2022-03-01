using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class PlayerShooting : MonoBehaviour
{
    //�ϐ�
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    //�e�̎��
    [Header("Bullet Type")]
    [SerializeField] GameObject[] bullet;
    [SerializeField] GameObject superBullet;
    //�e���������Ă���ꏊ
    [Header("Gun Position")]
    [SerializeField] GameObject barrel;
    //�e�̃v���p�e�B
    [Header("Bullet Properties")]
    public float attackDamage;
    [SerializeField] float attackSpeed;
    [SerializeField] Vector3 speed;
    //�p���[�A�b�v�̃^�C�}�[
    [Header("PowerUp duration")]
    [SerializeField] float powerUpTimer;
    float time;
    //�t���b�O
    public bool poweredUp = false;
    bool allowFire = true;
    //�G��|���{�[�i�X
    [SerializeField] float bonusSetting;
    //�����Ă���e�̏��
    int index = 0;
    public GameObject shootingBullet;
    //��������
    [SerializeField] AudioSource audioShooting;
    //����
    Gamepad gamepad;
    //���̑�
    UI_Time timeManager;
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //������
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    void Start()
    {
        timeManager = GameObject.Find("GameManager").GetComponent<UI_Time>();
        shootingBullet = bullet[0];
    }
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

    //����
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    void Update()
    {
        //����
        if (Gamepad.current != null)
            gamepad = Gamepad.current;

        //�Q�[���I�[�o�[
        if (timeManager.gameOver)
        {
            StopAllCoroutines();
            return;
        }

        //�e�̐؂�ւ��i�E�j
        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            OnButtonDown_rightChange();
        }

        //�e�̐؂�ւ��i���j
        if (gamepad.leftTrigger.wasPressedThisFrame)
        {
            OnBUttonDown_leftChange();
        }

        //����
        bool canShoot = gamepad.rightShoulder.wasPressedThisFrame;
        Shooting(canShoot);

        //�p���[�A�b�v�̊Ǘ�
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
    
    //����
    public void Shooting(bool canShoot)
    {
        if (allowFire && canShoot)
        {
            audioShooting.Play();
            GetComponent<Animator>().Play("Gunplay", 0);
            StartCoroutine(ShootingMethod());
        }
    }
    //������
    IEnumerator ShootingMethod()
    {
        allowFire = false;
        //���C�̔���
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        //�ړI�ɂ������m�F�̂���
        GameObject target = null;
        Vector3 targetPoint;
        //���C���w����
        if (Physics.Raycast(ray, out hit))
        {
            //���������I�u�W�F�N�g���m�F����
            targetPoint = hit.point;
            //�G�̏ꍇ�A�⏕����
            if (hit.transform.gameObject.tag == "Enemy")
            {
                targetPoint = hit.point;
                target = hit.transform.gameObject;
            }
            //�}�b�v��������A�⏕�Ȃ�
            else�@if (hit.transform.gameObject.tag == "Terrain")
            {
                targetPoint = hit.point;
            }
            //�^�[�Q�b�g���Ȃ�
            else target = null;
        }
        else
            targetPoint = ray.GetPoint(1000);
        //�e�̔���
        var shootBullet = Instantiate(shootingBullet, barrel.transform.position, barrel.transform.rotation);
        speed = (targetPoint - barrel.transform.position).normalized * 70f;
        //�e�̃v���p�e�B�̐ݒ�
        AttackDamage bulletProperty = shootBullet.GetComponent<AttackDamage>();
        bulletProperty.damage = attackDamage;
        bulletProperty.moveVector = targetPoint;
        bulletProperty.target = target;
        shootBullet.GetComponent<Rigidbody>().velocity = speed;
        //���A�j���[�V�����̃t���b�O
        GetComponent<Animator>().SetBool("shootingFlag", false);
        //�����[�h����
        yield return new WaitForSeconds(attackSpeed);
        allowFire = true;
    }
    //�e�̐؂�ւ�
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
