using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thompson : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;
    public bool canShoot = true;
    public Item thompson;
    public int cpt = 0;
    public int raf = 0;
    public GameObject itemDrops;
    public Transform dropPoint;

    void Start()
    {
        thompson.ammo =6;
    }


    void Update()
    {
        if (animator.GetBool("IsThomp") == true)
        {
            animator.SetBool("isPistol", false);
            animator.SetBool("isUzi", false);
            animator.SetBool("isShotgun", false);
            animator.SetBool("isSniper",false);
        }

        if (animator.GetBool("IsThomp") == true && animator.GetBool("isUzi") == false && animator.GetBool("isShotgun") == false && animator.GetBool("isPistol") == false && animator.GetBool("isSniper") == false)
        {
            GetComponent<Weapon>().enabled = false;
            GetComponent<Uzi>().enabled = false;
            GetComponent<Shotgun>().enabled = false;
            GetComponent<Sniper>().enabled = false;
            

            if (Input.GetButtonDown("Fire1") && animator.GetBool("isJumping") == false && animator.GetFloat("speed") < 0.01 && canShoot == true)
            {
                animator.SetBool("IsThomp_isFire", true);
                StartCoroutine(ShootBurst());
                raf++;
                canShoot = false;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Thompson Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.SetBool("IsThomp_isFire", false);
                canShoot = true;
            }

            if (raf >= 2 && !animator.GetBool("IsThomp_isFire") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Thompson Attack"))
            {
                canShoot = false;
                StartCoroutine(FinishShooting());
            }
        }
    }

    IEnumerator ShootBurst()
    {
        for (int i = 0; i < 3; i++)
        {
            if (thompson.ammo > 0)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                thompson.ammo--;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator FinishShooting()
    {
        yield return new WaitForSeconds(0.1f);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Thompson Attack"))
        {
            canShoot = true;
            animator.SetBool("IsThomp", false);
            ItemDrop();
            thompson.ammo += 6;
            raf = 0;
            GetComponent<Weapon>().enabled = true;
            GetComponent<Thompson>().enabled = false;
        }
    }

    private void ItemDrop()
    {
        if (itemDrops != null)
        {
            var drop = Instantiate(itemDrops, dropPoint.position, Quaternion.identity);
            Destroy(drop, 2);
        }
    }
}