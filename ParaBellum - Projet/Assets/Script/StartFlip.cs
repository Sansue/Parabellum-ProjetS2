using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
