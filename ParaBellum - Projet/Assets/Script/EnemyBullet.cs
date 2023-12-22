using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Vector2 direction = player.transform.position - transform.position;
        rb.velocity = direction.normalized * force;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Détruire la balle après 5 secondes
        Invoke("DestroyBullet", 5f);
    }

    // Détruire la balle
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
