using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossShamanMain : MonoBehaviour
{
    private EnemyTakeDamage enemyTakeDamage;
    private Summon summon;
    public bool evolveOnce = false;
    public bool isEvolving = false;
    private Transform player;
    private Animator animator;
    private GameObject boss;

    private void Start()
    {
        summon = GetComponent<Summon>();
        enemyTakeDamage = GetComponent<EnemyTakeDamage>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    private void Update()
    {
        if (enemyTakeDamage.currentHealth <= (enemyTakeDamage.maxHealth / 2) && !evolveOnce)
        {
            isEvolving = true;
            StartCoroutine(Phase2Animation());
        }
        
        if (enemyTakeDamage.currentHealth <= 0)
        {
            summon.canShoot = false;
            summon.canSummon = false;
            SceneManager.LoadScene("Chateau");
        }
        
    }

    private IEnumerator Phase2Animation()
    {
        enemyTakeDamage.NoDmgEvolve = false;
        yield return new WaitForSeconds(5f);
        summon.canShoot = true;
        summon.canSummon = true;
        evolveOnce = true;
        isEvolving = false;
        enemyTakeDamage.NoDmgEvolve = true;
        //pas prendre de degat pdt evolution
    }
}
