using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpioAnimationAttack : MonoBehaviour
{
    //攻撃プロパティ

    [Header("Attack Property")]
    public float attackDamage;
    [Header("Bullet Instantiation")]
    public GameObject enemyBullet;
    public Transform enemyGun;

    GameObject bulletObj;
    //コンポネント
    Animator animator;
    //外部
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
    //アニメーションを使って、攻撃します
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
