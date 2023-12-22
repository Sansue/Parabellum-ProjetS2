using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Shaman_Health : MonoBehaviour
{

    public int maxHealth = 10;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            //Destroy(Shaman)
            //SceneManager.LoadScene("niveau_2");
        }
    }
    
}
