using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public Transform firePoint;
   public GameObject bulletPrefab;
   public Animator animator;
   public bool canShoot = true;
    [SerializeField] private AudioSource gun_shot;



    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("isUzi") == false && animator.GetBool("isShotgun") == false && animator.GetBool("IsThomp")== false && animator.GetBool("isSniper")== false)
        {
            animator.SetBool("isPistol",true);
            GetComponent<Uzi>().enabled = false;
            GetComponent<Shotgun>().enabled = false;
            GetComponent<Thompson>().enabled = false;
            GetComponent<Sniper>().enabled = false;
        }
        else
        {
            animator.SetBool("isPistol",false);
            if ( animator.GetBool("isUzi") == true)
            {
                GetComponent<Uzi>().enabled = true;
            }
            if ( animator.GetBool("isShotgun") == true)
            {
                GetComponent<Shotgun>().enabled = true;
            }
            if( animator.GetBool("IsThomp") == true)
            {
                
                GetComponent<Thompson>().enabled = true;
            }
            if (animator.GetBool("isSniper")==true)
            {
            
                GetComponent<Sniper>().enabled = true;
            }


        }
        if (animator.GetBool("isPistol") == true && animator.GetBool("isUzi") == false && animator.GetBool("isShotgun") == false && animator.GetBool("IsThomp")== false && animator.GetBool("isSniper")==false)
        {
            if (Input.GetButtonDown("Fire1") &&  animator.GetBool( "isJumping") == false && animator.GetFloat ("speed") < 0.01 && canShoot == true) 
            {
           
               animator.SetBool("Pistol_Isshooting",true);
               Shoot();
               gun_shot.Play();
               canShoot = false; 
        
            }

       

            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Pistol_Shoot") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=1.0f)
            { 
                 animator.SetBool("Pistol_Isshooting",false);
                 canShoot = true;

            }

        }
        
        
        
    }
    



    void Shoot()
    {
        
    

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        

    }
}
