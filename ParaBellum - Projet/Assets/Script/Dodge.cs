using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    public bool isDodging = false;
    public PlayerMouvement PL;
    public Rigidbody2D rg;
    public Animator anim;
    public BoxCollider2D regularColl;
    public CircleCollider2D regularColle;
    public BoxCollider2D slideColl;
    public float slideSpeed = 5f;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.LeftShift))
        prefromSlide();
    }

    private void prefromSlide()
    {
        isDodging = true;
        anim.SetBool ("isDodge",true);
        regularColl.enabled = false;
        regularColle.enabled = false;
        slideColl.enabled = true;

        if(!PL.sprite.flipX)
        {
            rg.AddForce (Vector2.right*slideSpeed);
        }
        else
        {
            rg.AddForce(Vector2.left * slideSpeed);
        }
        StartCoroutine("stopSlide");
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds (0.8f);
        anim.SetBool("isDodge",false);
        regularColl.enabled = true;
        regularColle.enabled =true;
        slideColl.enabled = false;
        isDodging = false;

    }
}
