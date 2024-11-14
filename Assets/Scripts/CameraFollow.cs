using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float yOffset = 1.0f;

    private Vector3 _lastPosition;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (!target)
        {
            transform.position = _lastPosition;
            return;
        }
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + yOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.fixedDeltaTime);
        _lastPosition = transform.position;
    }
}
