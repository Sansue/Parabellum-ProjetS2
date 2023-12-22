using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDemonMain : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float chargeSpeed;
    public float chargeDistance;
    public bool evolveOnce = false;
    private ArenaManagerBossDemon arenaManager;
    private BossDemonDash bossDash;
    private GameObject p;
    private Transform player;
    private Animator animator;
    private Animator animationPlayer;
    private GameObject boss;
    private bool NoDmgEvolve = true;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        p = GameObject.FindGameObjectWithTag("Player");
        arenaManager = GetComponent<ArenaManagerBossDemon>();
        bossDash = GetComponent<BossDemonDash>();
        animator = GetComponent<Animator>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        animationPlayer = p.GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentHealth <= (maxHealth / 2) && !evolveOnce)
        {
            evolveOnce = true;
            animator.SetBool("Evolve", true);
            StartCoroutine(Phase2Animation());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator Phase2Animation()
    {
        NoDmgEvolve = false;
        arenaManager.StopSpawningLightning();
        yield return new WaitForSeconds(5f);
        animator.SetBool("FinishTransi", true);
        arenaManager.ToggleSpawningLightning();
        bossDash.enabled = true;
        NoDmgEvolve = true;
        //New stats after evolve
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && NoDmgEvolve == true)
        {
            if (animationPlayer.GetBool("isUzi") == true)
            {
                Bullet bullet = other.GetComponent<Bullet>();
                if (bullet != null)
                {
                    TakeDamage(bullet.damage);
                }
            }
            else if (animationPlayer.GetBool("isPistol") == true)
            {
                Bullet bullet = other.GetComponent<Bullet>();
                if (bullet != null)
                {
                    TakeDamage(bullet.damage);
                }
            }
            else if (animationPlayer.GetBool("isShotgun") == true)
            {
                Bullet bullet = other.GetComponent<Bullet>();
                if (bullet != null)
                {
                    TakeDamage(bullet.damage);
                }
            }
            else if (animationPlayer.GetBool("isThomb") == true)
            {
                Bullet bullet = other.GetComponent<Bullet>();
                if (bullet != null)
                {
                    TakeDamage(bullet.damage);
                }
            }
            else if (animationPlayer.GetBool("isSniper") == true)
            {
                Bullet bullet = other.GetComponent<Bullet>();
                if (bullet != null)
                {
                    TakeDamage(bullet.damage);
                }
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Vérifie si le boss est mort
        if (currentHealth <= 0)
        {
            Die();
            SceneManager.LoadScene("WW2");
        }
    }

    private void Die()
    {
        Debug.Log("Le boss est mort !");
        animator.SetBool("Death", true);
        arenaManager.StopSpawningLightning();

        AnimationClip deathAnimationClip = GetDeathAnimationClip();
        StartCoroutine(WaitForDeathAnimation(deathAnimationClip.length));
    }

    private IEnumerator WaitForDeathAnimation(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);

        Destroy(boss);
    }

    private AnimationClip GetDeathAnimationClip()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "DemonDeath") 
            {
                return clip;
            }
        }
        return null;
    }
}
