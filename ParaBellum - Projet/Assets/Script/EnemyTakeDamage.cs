using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    private Animator animator;
    private Animator animationPlayer;
    private GameObject player;
    public bool NoDmgEvolve = true;
    
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        animationPlayer = player.GetComponent<Animator>();
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
             else if (animationPlayer.GetBool("IsThomp") == true)
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy die");
        animator.SetBool("Death", true);
        AnimationClip deathAnimationClip = GetDeathAnimationClip();
        StartCoroutine(WaitForDeathAnimation(deathAnimationClip.length));
    }
    
    private IEnumerator WaitForDeathAnimation(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);

        Destroy(gameObject);
    }

    private AnimationClip GetDeathAnimationClip()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Death") 
            {
                return clip;
            }
        }
        return null;
    }


}