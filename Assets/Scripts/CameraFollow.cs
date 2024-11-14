using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameSettingsSO gameSettingsData;

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
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + gameSettingsData.YOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, gameSettingsData.Speed * Time.fixedDeltaTime);
        _lastPosition = transform.position;
    }
}
