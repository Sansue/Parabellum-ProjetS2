using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_tank : MonoBehaviour
{
    EnemyTakeDamage enemyTakeDamage;
    private bool firstPhase = true;
    public bool evolveOnce = false;
    private Animator animator;
    private GameObject boss;
    private BossTankMovement bossDash;
    private BossTankFire bossTankFire;
    


    // Start is called before the first frame update
    void Start()
    {
        enemyTakeDamage = GetComponent<EnemyTakeDamage>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossDash = GetComponent<BossTankMovement>();
        animator = GetComponent<Animator>();
        bossTankFire = GetComponent<BossTankFire>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTakeDamage.currentHealth <= enemyTakeDamage.maxHealth /2 && !evolveOnce)
        {
            firstPhase = false;
            bossTankFire.minShellInterval -= 1f;
            bossTankFire.maxShellInterval -= 1f;
            bossTankFire.fallingSpeed += 2;
        }

        if(!firstPhase && !evolveOnce)
        {
            bossDash.enabled = true;
            animator.SetBool("Evolve", true);
            bossDash.canMove = true;
            bossTankFire.canShoot = true;
            evolveOnce = true;
        }
        
        if (enemyTakeDamage.currentHealth <= 0)
        {
            bossDash.canMove = false;
            bossTankFire.canShoot = false;
        }
    }
}