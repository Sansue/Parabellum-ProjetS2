using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] patrolPoints;
	public SpriteRenderer graphics;
    public float waitTime;
    int currentPointIndex;
	bool once;
	public Animator animator;
	public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
	    graphics.flipX = !graphics.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x != patrolPoints[currentPointIndex].position.x)
		{
			transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
			animator.SetFloat("Speed", 1);
		}

		else
		{
			animator.SetFloat("Speed", 0);
			if (once == false)
			{
				once = true;
				StartCoroutine(Wait());
				graphics.flipX = !graphics.flipX;
			}
		}
    }

	IEnumerator Wait()
	{
		yield return new WaitForSecondsRealtime(waitTime);
		if (currentPointIndex + 1 < patrolPoints.Length)
		{
			currentPointIndex++;
		}

		else
		{
			currentPointIndex = 0;
		}
		once = false;
	}
}

/*
public class EnemyPatrol2 : MonoBehaviour
{
    public float speed;
    public Transform recallPoint;
    public SpriteRenderer graphics;
    public float waitTime;
    public int roamDistance;
    bool direction;
    bool once;
    public Animator animator;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = true;
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        while (roamDistance > Mathf.Abs(transform.position.x - recallPoint.position.x) && once == false)
        {
            if (direction == true)
            {
                transform.position.x += Vector2.right * speed * Time.deltaTime;
                animator.SetFloat("Speed", 1);
            }

            else
            {
                transform.position.x += Vector2.left * speed * Time.deltaTime; 
                animator.SetFloat("Speed", 1);
            }
        }
        
        StartCoroutine(Wait());

        if (once == true)
        {
            graphics.flipX = !graphics.flipX;
            transform.position = Vector2.MoveTowards(transform.position, recallPoint.position, speed * Time.deltaTime);
            direction = !direction;
        }
        
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        once = !once;
    }
}
 */
