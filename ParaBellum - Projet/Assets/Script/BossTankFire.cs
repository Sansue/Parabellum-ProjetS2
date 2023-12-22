using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankFire : MonoBehaviour
{
    public GameObject shell;
    public float minShellInterval;
    public float maxShellInterval;
    private GameObject sp;
    private bool isSpawning = false;
    private Transform player;
    private Boss_tank bossTank;
    public int fallingSpeed;
    public bool canShoot = true;

    private void Start()
    {
        Rigidbody2D shellRigidbody = shell.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossTank = GetComponent<Boss_tank>();
        StartCoroutine(SpawnShellCoroutine());
        shellRigidbody.gravityScale = fallingSpeed;
    }
    
    private System.Collections.IEnumerator SpawnShellCoroutine()
    {
        while (true && canShoot)
        {
            if (!isSpawning)
            {
                SpawnShells();
            }
            
            float spawnInterval = Random.Range(minShellInterval, maxShellInterval);
            yield return new WaitForSeconds(spawnInterval);
            isSpawning = false;
        }

        yield return new WaitForSeconds(1f);
    }
    
    private void SpawnShells()
    {
        isSpawning = true;
        FindSpawnPoint();
        
        float yes = Random.Range(0, 2);
        if (yes == 1 || bossTank.evolveOnce)
        {
            float nb = Random.Range(0, 12);
            GameObject sp2 = GameObject.Find($"SpawnPoint ({nb})");
            
            if (sp2 != sp)
            {
                Instantiate(shell, sp2.transform.position, Quaternion.identity);
            }
        }
        Instantiate(shell, sp.transform.position, Quaternion.identity);
    }

    private void FindSpawnPoint()
    {
        GameObject spawn1 = GameObject.Find("SpawnPoint (5)");
        GameObject spawn2 = GameObject.Find("SpawnPoint (6)");
        
        float pX = player.position.x;
        
        if (Mathf.Abs(pX - spawn1.transform.position.x) < Mathf.Abs(pX - spawn2.transform.position.x))
        {
            spawn2 = spawn1;
            spawn1 = GameObject.Find("SpawnPoint (4)");
            int i = 3;
            while (i >= 0 && Mathf.Abs(pX - spawn1.transform.position.x) < Mathf.Abs(pX - spawn2.transform.position.x))
            {
                spawn2 = spawn1;
                spawn1 = GameObject.Find($"SpawnPoint ({i})");
                i--;
            }

            if (i == -1)
            {
                sp = spawn1;
            }
            else
            {
                sp = spawn2;
            }
        }
        
        else
        {
            spawn1 = spawn2;
            spawn2 = GameObject.Find("SpawnPoint (7)");
            int i = 8;
            while (i <= 11 && Mathf.Abs(pX - spawn1.transform.position.x) > Mathf.Abs(pX - spawn2.transform.position.x))
            {
                spawn1 = spawn2;
                spawn2 = GameObject.Find($"SpawnPoint ({i})");
                i++;
            }

            if (i == 12)
            {
                sp = spawn2;
            }
            else
            {
                sp = spawn1;
            }
        }
    }

}
