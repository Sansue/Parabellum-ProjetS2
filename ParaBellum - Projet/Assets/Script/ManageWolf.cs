using UnityEngine;

public class ManageWolf : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    public float moveSpeed = 5f;
    public float attackDistance = 2f;
    private bool isAttacking = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;

            // Vérifier si le joueur est devant l'ennemi
            if (Vector2.Dot(direction, transform.right) < 0)
            {
                // Se déplacer vers le joueur
                MoveTowardsPlayer(direction);

                // Vérifier la distance entre l'ennemi et le joueur pour l'attaque
                if (direction.magnitude <= attackDistance)
                {
                    if (!isAttacking)
                    {
                        // Déclencher l'animation d'attaque
                        animator.SetBool("Attack", true);
                        isAttacking = true;
                    }
                }
                else
                {
                    // Arrêter l'attaque
                    animator.SetBool("Attack", false);
                    isAttacking = false;
                }
            }
            else
            {
                // Se retourner si le joueur n'est plus devant l'ennemi
                Flip();
            }
        }
    }

    private void MoveTowardsPlayer(Vector2 direction)
    {
        float moveX = direction.normalized.x * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(moveX, 0f, 0f);
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

}
