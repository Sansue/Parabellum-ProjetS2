using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public EnemySight enemySight;
    public float timer;
    public float moveSpeed;
    public float attackDistance;
    private GameObject target;
    private float distance;
    private Animator animator;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;

    void Awake()
    {
        intTimer = timer;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        enemySight = GetComponent<EnemySight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySight.CanSeePlayer == true)
        {
            inRange = true;
            EnemyLogic();
        }
        else
        {
            inRange = false;
        }

        if (inRange == false)
        {
            animator.SetBool("isRunning", false);
            animator.SetFloat("Speed", 0);
            StopAttack();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            animator.SetBool("isAttacking", false);
        }
    }

    void Move()
    {
        animator.SetBool("isRunning", true);
        animator.SetFloat("Speed", 1);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Cac_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;
        animator.SetBool("isRunning", false);
        animator.SetFloat("Speed", 0);
        animator.SetBool("isAttacking", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("isAttacking", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }
}
