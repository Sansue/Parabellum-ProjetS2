using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sprite; 
    public CharacterController2D controller;
    public float runSpeed =40f;
    float horizontalMove = 0f;
    bool jump = false;
    public Animator animator;
    public bool isJumping;
    [SerializeField] private AudioSource FootSteps;
  

    // Update is called once per frame
    void Update()
    {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
       animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        FootSteps.Play();

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping",true);
            isJumping = true;
        }
        animator.SetFloat("ySpeed", rb.velocity.y);
        FootSteps.Play();
    }

    public void OnLanding ()
    {
        animator.SetBool("isJumping",false);
        isJumping = false;
        FootSteps.Play();
    }

    void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        FootSteps.Play();
    }
}
