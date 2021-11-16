using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCamera : MonoBehaviour
{
    //SmoothDamp ‚ÅŽg‚¤
    Vector3 velocity; 

    [Header("Speed Of Reposition")]
    public float time = 2f;

    Vector3 target;
    public Vector3 offset;

    private void Start()
    {
        target = transform.position + offset;
    }

    private void Update()
    {
        transform.LookAt(target - offset);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, time);
    }
}
