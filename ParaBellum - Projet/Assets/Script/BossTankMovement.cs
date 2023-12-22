using System.Collections;
using UnityEngine;

public class BossTankMovement : MonoBehaviour
{
    public Transform dashStartPoint;
    public Transform dashEndPoint;
    private float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    private bool canDash = true;
    public bool canMove = true;

    private void Start()
    {
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        while (true && canMove)
        {
            if (canDash)
            {
                canDash = false;
                StartCoroutine(PerformDash());
            }

            yield return new WaitForSeconds(dashCooldown);
        }
    }

    private IEnumerator PerformDash()
    {
        float startTime = Time.time;
        Vector3 startPosition = dashStartPoint.position;
        Vector3 targetPosition = dashEndPoint.position;

        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float dashSpeed = journeyLength / dashDuration; // Ajuster la vitesse en fonction de la distance

        while (Time.time < startTime + dashDuration)
        {
            float fractionOfJourney = (Time.time - startTime) / dashDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        // Arrêt après le dash
        yield return new WaitForSeconds(dashDuration);

        // Inversion des points de départ et d'arrivée pour le prochain dash
        SwapDashPoints();

        canDash = true;
    }

    private void SwapDashPoints()
    {
        Transform temp = dashStartPoint;
        dashStartPoint = dashEndPoint;
        dashEndPoint = temp;
        transform.Rotate(0f, 180f, 0f);
    }
}