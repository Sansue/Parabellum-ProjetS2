using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mort : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject DeathUI;
    private Animator animator;
    private Rigidbody2D rb;
    private AnimationClip deathAnimationClip;
    private bool triggerDeathOnce = false;

    void Start()
    {
        DeathUI.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            if (!triggerDeathOnce)
            {
                animator.SetTrigger("death");
                triggerDeathOnce = true;
            }
            deathAnimationClip = GetDeathAnimationClip();
            StartCoroutine(WaitForDeathAnimation(deathAnimationClip.length));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap") || collision.gameObject.CompareTag("Boss"))
        {
            if (!triggerDeathOnce)
            {
                animator.SetTrigger("death");
                triggerDeathOnce = true;
            }
            deathAnimationClip = GetDeathAnimationClip();
            StartCoroutine(WaitForDeathAnimation(deathAnimationClip.length));
        }
    }

    private IEnumerator WaitForDeathAnimation(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);

        Dead();
    }

    private AnimationClip GetDeathAnimationClip()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Player_Death") 
            {
                return clip;
            }
        }
        return null;
    }

    public void Resume()
    {
        DeathUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Dead()
    {   
        Paused = true;
        DeathUI.SetActive(true);
        Time.timeScale = 0f;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void Menu()
    {
        DeathUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu_Principal");
    }
}
