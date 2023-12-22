using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    public EnemySight enemySight;
    private Animator animator;
	public float fireRate;

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemySight = GetComponent<EnemySight>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (enemySight.CanSeePlayer == true)
        {
            timer += Time.deltaTime;
            
            if (timer > fireRate)
            {
                timer = 0;
                Shoot();
            }
        }

		else
		{
			animator.SetBool("isShooting",false);
		}
    }

    void Shoot()
    {
        animator.SetBool("isShooting",true);
        AnimationClip shootAnimationClip = GetShootAnimationClip();
        StartCoroutine(WaitForShootAnimation(shootAnimationClip.length));
    }
    
    private IEnumerator WaitForShootAnimation(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);

        Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        
        animator.SetBool("isShooting",false);
    }

    private AnimationClip GetShootAnimationClip()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Dist_Attack") 
            {
                return clip;
            }
        }
        return null;
    }
}