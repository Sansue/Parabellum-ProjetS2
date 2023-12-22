using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageShamanProjectile : MonoBehaviour
{
    private GameObject player;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Détruire la balle après 5 secondes
        Invoke("DestroyBullet", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, force * Time.deltaTime);
    }

    // Détruire la balle
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
