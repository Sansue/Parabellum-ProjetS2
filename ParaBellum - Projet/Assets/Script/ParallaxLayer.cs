using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [ExecuteInEditMode]
 public class ParallaxLayer : MonoBehaviour
 
{
    public float parallaxFactor;

    /*public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;
        transform.localPosition = newPos;
    }*/

    public void Move(Vector2 delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta.x * parallaxFactor;
        newPos.y -= delta.y * parallaxFactor;
        transform.localPosition = newPos;
    }
}