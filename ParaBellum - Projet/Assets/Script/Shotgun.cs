using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
   public Transform firePoint;
   public GameObject bulletPrefab;
   public Animator animator;
   public bool canShoot = true;
   public Item shotgun;
   public int cpt =0 ;
   public GameObject itemDrops;
   public Transform dropPoint;
   public int bulletCount = 4;
    public float bulletSpreadAngle = 100f;
    public float bulletSpeedVariation = 20f;
   
    void Start()
    {
        shotgun.ammo =3;
    }


    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("isShotgun")== true)
        {
           animator.SetBool("isPistol",false);
           animator.SetBool("isUzi",false);
           animator.SetBool("IsThomp",false);
           animator.SetBool("isSniper",false);

        }
        
        if (animator.GetBool("isShotgun") == true && animator.GetBool("isUzi") == false && animator.GetBool("isPistol") == false && animator.GetBool("IsThomp") == false && animator.GetBool("isSniper") == false)
        {
            GetComponent<Weapon>().enabled = false;
            GetComponent<Uzi>().enabled = false;
            GetComponent<Thompson>().enabled = false;
             GetComponent<Sniper>().enabled = false;
            if (Input.GetButtonDown("Fire1") &&  animator.GetBool( "isJumping") == false && animator.GetFloat ("speed") < 0.01 && canShoot == true) 
            {
           
               animator.SetBool("Shotgun_isFire",true);
               shotgun.ammo -=1;
               for (int i = 0; i < bulletCount; i++)
               {
                    float angle = Random.Range(-bulletSpreadAngle / 2f, bulletSpreadAngle / 2f);

                    Vector3 spreadDirection = Quaternion.AngleAxis(angle, Vector3.forward) * firePoint.right;
                    spreadDirection = Quaternion.Euler(0, 0, transform.eulerAngles.z) * spreadDirection; // prendre en compte la rotation du personnage
                    float variation = Random.Range(-bulletSpeedVariation, bulletSpeedVariation);
                    float speed = bulletPrefab.GetComponent<Bullet>().speed + variation;
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                    bullet.transform.right = spreadDirection.normalized; // aligner la balle avec la direction
                    bullet.GetComponent<Rigidbody2D>().velocity = spreadDirection.normalized * speed;
                    Debug.DrawLine(firePoint.position, firePoint.position + spreadDirection * 10f, Color.red, 5f);
    

                }
                canShoot = false; 
        
            }

       

            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Shotgun_Fire") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=1.0f)
            { 
                 animator.SetBool("Shotgun_isFire",false);
                 canShoot = true;
        
            }
             if (shotgun.ammo == 0)
            {
                canShoot = false;
                 if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Shotgun_Fire") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=1.0f)
                { 
                    canShoot = true;
                    animator.SetBool("Shotgun_isFire",false);
                    animator.SetBool("isShotgun",false);
                    ItemDrop();
                    shotgun.ammo +=3;
                }
                
                GetComponent<Weapon>().enabled = true;
                GetComponent<Shotgun>().enabled = false;
            
            }

        }
        
    }
    
   

    private void ItemDrop()
    {
       var drop = Instantiate(itemDrops, dropPoint.position, dropPoint.rotation);
       Destroy(drop,2);
      
    }

    
  
    
    
    

    
}
