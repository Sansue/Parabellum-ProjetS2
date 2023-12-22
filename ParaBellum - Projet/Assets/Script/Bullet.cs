using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float lifetime = 2f; // durée de vie de la balle en secondes
    public Animator animator;
    public int damage;
    private Animator animationPlayer;
    private GameObject player;

    

    private bool isDestroying = false;

    private void Start()

    {
        player = GameObject.FindGameObjectWithTag("Player");
        animationPlayer = player.GetComponent<Animator>();
        if (animationPlayer.GetBool("isUzi") == true)
        {
            damage = 3;
        }
        else if (animationPlayer.GetBool("isPistol") == true)
        {
            damage = 4;
        }
        else if (animationPlayer.GetBool("isShotgun") == true)
        {
            damage=2;
        }
        else if (animationPlayer.GetBool("isThomp") == true)
        {
            damage = 4;
        }
        else if (animationPlayer.GetBool("isSniper") == true)
        {
            damage = 100;
        }
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        StartCoroutine(DestroyAfterLifetime());
    }

    IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);

        if (!isDestroying)
        {
            isDestroying = true;
            if (animator != null)
            {
                animator.SetBool("IsDestroy", true);
            }
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        if (!isDestroying)
        {
            isDestroying = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            if (animator != null)
            {
                animator.SetBool("IsDestroy", true);
                StartCoroutine(DestroyAfterAnimation());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnDestroyAnimationStart()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

}