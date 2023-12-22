using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBird : MonoBehaviour
{
    private Transform startPoint;
    private Transform endPoint;
    public float flightSpeed = 5f;
    private bool isFlying = false;
    private bool followPlayer = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPoint = GameObject.Find("StartPoint").transform;
        endPoint = GameObject.Find("EndPoint").transform;

        transform.position = startPoint.position;
        StartCoroutine(ActivateFollow());

        isFlying = true;
    }

    private void Update()
    {
        if (isFlying && !followPlayer)
        {
            Vector3 flightDirection = (endPoint.position - startPoint.position).normalized;
            transform.position += flightDirection * flightSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, endPoint.position) <= 0.1f)
            {
                Vector3 temp = startPoint.position;
                startPoint.position = endPoint.position;
                endPoint.position = temp;

                isFlying = false;
                transform.Rotate(0f, 180f, 0f);
                StartCoroutine(FlightCooldown());
            }
        }
        else if (isFlying && followPlayer)
        {
            Vector3 targetPosition = player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, flightSpeed * Time.deltaTime);

            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 180f, Vector3.forward);
        }
    }

    private IEnumerator FlightCooldown()
    {
        yield return new WaitForSeconds(2f);
        isFlying = true;
    }

    private IEnumerator ActivateFollow()
    {
        yield return new WaitForSeconds(10f);
        followPlayer = true;
    }
}
