using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    
    public bool isInRange;
    public bool have = false;
    public Animator animator;
    public Item item;
    public bool isShotgun;
    public bool isUzi;
    public bool isThompson;
    public bool isSniper;
    
    
    
 

    public void check ()
    {
        if (item.name == "uzi")
        {
            isUzi =true;
            
        }
        if(item.name == "shotgun")
        {
            isShotgun = true;
        }
        if(item.name == "thompson")
        {
            isThompson = true;
        }
        if(item.name == "sniper")
        {
            isSniper = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            check();
            TakeItem();
        }
    }

    void TakeItem()
    {
        if (isUzi == true)
        {
            animator.SetBool("isUzi",true);
            animator.SetBool("isShotgun",false);
            animator.SetBool("IsThomp",false);
            animator.SetBool("isSniper",false);
            isThompson = false;
            isShotgun = false;
            isSniper = false;
            
            have = true;
            Destroy(gameObject);
        }

        if (isShotgun ==true)
        {
            animator.SetBool("isShotgun",true);
            animator.SetBool("isUzi",false);
            animator.SetBool("IsThomp",false);
            animator.SetBool("isSniper",false);
            isUzi =false;
            isThompson = false;
            isSniper = false;
            have = true;
            Destroy(gameObject);
        }
        if (isThompson ==true)
        {
            animator.SetBool("IsThomp",true);
            animator.SetBool("isUzi",false);
            animator.SetBool("isShotgun",false);
            animator.SetBool("isSniper",false);
            isUzi =false;
            isSniper = false;
            isShotgun = false;
            have = true;
            Destroy(gameObject);
        }
        if (isSniper ==true)
        {
            animator.SetBool("isSniper",true);
            animator.SetBool("isUzi",false);
            animator.SetBool("isShotgun",false);
            animator.SetBool("IsThomp",false);
            isUzi =false;
            isThompson = false;
            isShotgun = false;
            have = true;
            Destroy(gameObject);
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer==6)
        {
            
            isInRange = false;
        }
    }
    

    
}