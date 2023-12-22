using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : MonoBehaviour
{
    public Transform firePoint;
   public GameObject bulletPrefab;
   public Animator animator;
   public bool canShoot = true;
   float FireRate  = 10;  
   float lastfired ; 
   public Item uzi;
   public int cpt =0 ;
   public GameObject itemDrops;
   public Transform dropPoint;
   
   

   void Start()
    {
        uzi.ammo =10;
    }

   
    

    void Update()
    {
        if (animator.GetBool("isUzi") == true)
        {
            animator.SetBool("isPistol",false);
            animator.SetBool("isShotgun",false);
            animator.SetBool("IsThomp",false);
            animator.SetBool("isSniper",false);
            
        }
        if (animator.GetBool("isUzi") == true && animator.GetBool("isPistol") == false && animator.GetBool("isShotgun") == false && animator.GetBool("IsThomp") == false && animator.GetBool("isSniper") == false);
        {
            GetComponent<Weapon>().enabled = false;
            GetComponent<Shotgun>().enabled = false;
            GetComponent<Thompson>().enabled = false;
            GetComponent<Sniper>().enabled = false;
            
            if(Input.GetButton("Fire1") &&  animator.GetBool( "isJumping") == false && animator.GetFloat ("speed") < 0.01 && canShoot == true) 
            {
           
            animator.SetBool("Uzi_isFire",true);
            Shoot();
            
        
            }
            
            if (Input.GetButtonUp("Fire1") )
            {
                canShoot = false;
                animator.SetBool("Uzi_isFire",false);
                animator.SetBool("Uzi_isStopping",true);
                
            }

            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Uzi_Stop") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=1.0f)
            { 
                canShoot = true;
                animator.SetBool("Uzi_isStopping",false);
            }


            
             if (cpt == 11)
            {
                canShoot = false;
                animator.SetBool("Uzi_isFire",false);
                animator.SetBool("Uzi_isStopping",true);
                
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Uzi_Stop") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=1.0f)
                { 
                    canShoot = true;
                    animator.SetBool("Uzi_isStopping",false);
                    animator.SetBool("isUzi",false);
                    ItemDrop();
                    uzi.ammo +=10;
                    cpt-=10;
                }
                GetComponent<Weapon>().enabled = true;
                GetComponent<Uzi>().enabled = false;
            }

        }
        
    }

    
    

    void Shoot()
    {
        if (Time.time - lastfired > 1 / FireRate)
        {
            lastfired = Time.time;
            uzi.ammo -=1;
            cpt+=1;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        }
       
       
        
        
    }
    private void ItemDrop()
    {
       var drop = Instantiate(itemDrops, dropPoint.position, Quaternion.identity);
       Destroy(drop,2);
    }
    
}
