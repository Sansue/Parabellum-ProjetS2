using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private bool isInRange;

    public Animator animator;
    public GameObject itemDrops;
    public Transform dropPoint;
    
    void Start ()
    {
        itemDrops.SetActive(false);
    }

  

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            OpenChest();
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Chest_Open") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=1.0f)
        { 
            animator.SetBool("Open_Chest",false);
            No();
        }
         
    }

    void OpenChest()
    {
        animator.SetBool("Open_Chest",true);
        ItemDrop();
        
        GetComponent<BoxCollider2D>().enabled = false;
        
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

    public void No()
    {
        gameObject.SetActive(false);

    }
    private void ItemDrop()
    {
      itemDrops.SetActive (true);
    }
    
}