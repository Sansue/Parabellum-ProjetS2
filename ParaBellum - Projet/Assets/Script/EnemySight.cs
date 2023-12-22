using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif



public class EnemySight : MonoBehaviour
{
    public float radius;
    [Range(1, 360)] public float angle;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public GameObject playerRef;
    public bool CanSeePlayer { get; private set; }
    public SpriteRenderer graphics;
    private bool currentFlip;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        currentFlip = false;
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        if (rangeCheck != null && rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.right * (currentFlip ? -1 : 1), directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget * (currentFlip ? -1 : 1),
                        distanceToTarget, obstructionLayer))
                {
                    CanSeePlayer = true;
                }
                else
                {
                    CanSeePlayer = false;
                }
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else if (CanSeePlayer)
        {
            CanSeePlayer = false;
        }
        
        currentFlip = graphics.flipX;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
        Vector3 angle01 = DirectionFromAngle(-angle / 2);
        Vector3 angle02 = DirectionFromAngle(angle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (playerRef != null && CanSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float angleInDegrees)
    {
        return (Vector2)(Quaternion.Euler(0, 0, angleInDegrees) * (currentFlip ? -transform.right : transform.right));
    }
}