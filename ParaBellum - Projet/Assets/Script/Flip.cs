using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public SpriteRenderer graphics;
    public float waitTime;
    public EnemySight enemySight;

    void Awake()
    {
        enemySight = GetComponent<EnemySight>();
    }
    
    void Start()
    {
        StartCoroutine(FlipCheck());
    }

    private IEnumerator FlipCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(waitTime);
        while (enemySight.CanSeePlayer == false)
        {
            FlipIt();
            yield return wait;
        }
    }

    private void FlipIt()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
