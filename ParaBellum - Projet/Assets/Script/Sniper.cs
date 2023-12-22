using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;
    public bool canShoot = true;
    public Item sniper;
    public int cpt = 0;
    public GameObject itemDrops;
    public Transform dropPoint;
    private AnimationClip sniperAnimationClip;

    private void Start()
    {
        sniperAnimationClip = GetSniperAnimationClip();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("isSniper") == true)
        {
            animator.SetBool("isPistol", false);
            animator.SetBool("isUzi", false);
            animator.SetBool("IsThomp", false);
            animator.SetBool("isShotgun", false);
        }

        if (animator.GetBool("isSniper") == true && animator.GetBool("isUzi") == false && animator.GetBool("isPistol") == false && animator.GetBool("IsThomp") == false && animator.GetBool("isShotgun") == false)
        {
            GetComponent<Weapon>().enabled = false;
            GetComponent<Uzi>().enabled = false;
            GetComponent<Thompson>().enabled = false;
            GetComponent<Shotgun>().enabled = false;

            if (Input.GetButtonDown("Fire1") && animator.GetBool("isJumping") == false && animator.GetFloat("speed") < 0.01 && canShoot == true)
            {
                if (sniper.ammo > 0)
                {
                    animator.SetBool("Sniper_isFire", true);
                    sniper.ammo -= 1;
                    Shoot();
                    StartCoroutine(WaitForAnimationStart());
                }
            }

            if (!canShoot)
            {
                StartCoroutine(WaitForSniperAnimation(sniperAnimationClip.length));
                canShoot = true;
            }

            if (sniper.ammo == 0)
            {
                canShoot = false;
                
                StartCoroutine(WaitForSniperAnimation(sniperAnimationClip.length));
                StartCoroutine(WaitForSniperAnimation0Bullet(sniperAnimationClip.length));
                sniper.ammo += 1;

                GetComponent<Weapon>().enabled = true;
                GetComponent<Sniper>().enabled = false;
            }
        }
    }

    private IEnumerator WaitForAnimationStart()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = false;
    }

    private IEnumerator WaitForSniperAnimation(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);
        animator.SetBool("Sniper_isFire", false);

    }

    private IEnumerator WaitForSniperAnimation0Bullet(float animationLength)
    {
        animator.SetBool("isSniper", false);
        ItemDrop();
        yield return new WaitForSeconds(animationLength);
    }

    private AnimationClip GetSniperAnimationClip()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Sniper_Attack") 
            {
                return clip;
            }
        }
        return null;
    }

    private void ItemDrop()
    {
        if (itemDrops != null)
        {
            var drop = Instantiate(itemDrops, dropPoint.position, Quaternion.identity);
            Destroy(drop, 2);
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}