using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Range Settings")]
    public float sightRange;
    public float attackRange;
    public float walkingRange;
    [Header("Player Detection")]
    public LayerMask playerMask;
    public Transform player;

    public bool isAttacking = false;
    NavMeshAgent agent;
    Animator animator;
    UI_Time time;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        time = GameObject.Find("GameManager").GetComponent<UI_Time>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!time.gameOver)
        {
            //プレイヤーがみえるかどうか確認
            bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
            //攻撃できるかどうか確認
            bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);



            //キャラが遠い　-＞　
            if (!playerInSightRange && !playerInAttackRange && !isAttacking) AI_IdleMovement(walkingRange);
            //近いが攻撃が届かない　ー＞　
            if (playerInSightRange && !playerInAttackRange && !isAttacking) AI_ActiveMovement();
            //近い　ー＞ 
            if ((playerInSightRange && playerInAttackRange) || isAttacking)
            {
                animator.SetBool("isAttacking", true);
                agent.velocity = Vector3.zero;
                transform.LookAt(player.transform);
            }
        }
        else Destroy(gameObject);
    }

        Vector3 walkPoint;
        bool walkPointSet;
    public void AI_IdleMovement(float walkingRange)
    {
        //まず、移動位置がなかったら、この位置を作りましょう。
        if (!walkPointSet) SearchWalkPoint(walkingRange);
        //移動先が決まったら、移動する
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("isMoving", true);
        }
        //移動先までの距離
        Vector3 distance = transform.position - walkPoint;
        //着いたら、新しい移動先を作る。
        if (distance.magnitude < 0.1f)
        {
            walkPointSet = false;
            animator.SetBool("isMoving", false);
        }
    }
    private void SearchWalkPoint(float walkingRange)
    {
        LayerMask groundMask = LayerMask.GetMask("Ground");
        //ランダムな方向
        float randomZ = Random.Range(-walkingRange, walkingRange);
        float randomX = Random.Range(-walkingRange, walkingRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        NavMeshHit hit;

        //この位置の下に地面があったら、この場所を移動先として認める
        if (NavMesh.SamplePosition(walkPoint, out hit, 1f, NavMesh.AllAreas))
        {
            Debug.Log(hit.position);
            Debug.DrawRay(hit.position, Vector3.up, Color.red, 10f);
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }

    public void AI_ActiveMovement()
    {
        //追う
        agent.SetDestination(player.position);
        animator.SetBool("isMoving", true);
    }
}
