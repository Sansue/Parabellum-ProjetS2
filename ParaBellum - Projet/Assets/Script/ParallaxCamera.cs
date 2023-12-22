using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [ExecuteInEditMode]
 public class ParallaxCamera : MonoBehaviour 
 {
     //public delegate void ParallaxCameraDelegate(float deltaMovement);
    public delegate void ParallaxCameraDelegate(Vector2 deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    //private float oldPosition;
    private Vector2 oldPosition;

    void Start()
    {
        //oldPosition = transform.position.x;
        oldPosition = (Vector2)transform.position;
    }

    //void Update()
    void FixedUpdate()
    {
        //if(transform.position.x != oldPosition)
        if((Vector2)transform.position != oldPosition)
        {
            if(onCameraTranslate != null)
            {
                //float delta = oldPosition - transform.position.x;
                Vector2 delta = oldPosition - (Vector2)transform.position;
                onCameraTranslate(delta);
            }
            //oldPosition = transform.position.x;
            oldPosition = (Vector2)transform.position;
        }
    }
 }