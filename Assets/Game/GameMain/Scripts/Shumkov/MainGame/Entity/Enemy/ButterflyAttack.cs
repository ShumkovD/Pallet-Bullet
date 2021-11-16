using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyAttack : MonoBehaviour
{
    //攻撃プロパティ
    [Header("Damage Property")]
    public Collider attackCollider;
    public int damage;
    //コンポネント
    Animator animator;
    public Transform player;
    //外部
    EnemyMovement movement;
    [Header("Used in \"Enemy Movement\"")]
    public bool isAttackingPlayer;
    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
        attackCollider.GetComponent<AttackDamage>().damage = damage;
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
    //攻撃接触を有効にする
    public void ActivateAtack()
    {
    
        isAttackingPlayer = true;
        attackCollider.enabled = true;
    }
    //攻撃接触を無効にする
    public void DeactivateAtack()
    {

        isAttackingPlayer = false;
        attackCollider.enabled = false;
        animator.SetBool("isAttacking", false);
    }
}
