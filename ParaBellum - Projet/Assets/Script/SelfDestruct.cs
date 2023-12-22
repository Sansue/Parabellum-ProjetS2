using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifetime;

    private void Start()
    {
        Invoke("DestroyObject", lifetime);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
