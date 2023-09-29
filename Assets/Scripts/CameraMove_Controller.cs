using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove_Controller : MonoBehaviour
{
    [Header("Camera Move Properties")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3 (0, 0, -10);
    [SerializeField] private float damping;

    [Space(10)]
    [Header("Toggles")]
    [SerializeField] bool followPlayer = true;
    Vector3 veclocity = Vector3.zero;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (followPlayer) { FollowPlayer(); }
    }

    private void FollowPlayer()
    {
        Vector3 movePOS = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePOS, ref veclocity, damping);
    }
}
