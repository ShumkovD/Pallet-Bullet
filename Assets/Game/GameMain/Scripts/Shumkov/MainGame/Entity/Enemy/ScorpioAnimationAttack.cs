using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpioAnimationAttack : MonoBehaviour
{
    //�U���v���p�e�B

    [Header("Attack Property")]
    public float attackDamage;
    [Header("Bullet Instantiation")]
    public GameObject enemyBullet;
    public Transform enemyGun;

    GameObject bulletObj;
    //�R���|�l���g
    Animator animator;
    //�O��
    EnemyMovement movement;
    // Update is called once per frame
    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
        enemyGun = gameObject.transform.Find("GunEnemy");
    }

    public IEnumerator Attack()
    {
          movement.isAttacking = true;
          var bullet = AttackMelee();
          Destroy(bullet, 0.1f);
          yield return new WaitForSeconds(0.1f);
          movement.isAttacking = false;
    }
    public GameObject AttackMelee()
    {
        var bullet = Instantiate(enemyBullet, enemyGun.position, enemyGun.rotation);
        bulletObj = bullet;
        bullet.GetComponent<AttackDamage>().damage = attackDamage;
        return bullet;
    }
    //�A�j���[�V�������g���āA�U�����܂�
    public void StopMovement()
    {
        animator.SetBool("isMoving", false);
        movement.isAttacking = true;
    }
    public void ResumeMovement()
    {
        animator.SetBool("isAttacking", false);
        movement.isAttacking = false;
    }
}
