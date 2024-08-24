using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CameraController _cam;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;
    private Vector3 _velocity;
    // Update is called once per frame
    void Update()
    {
        RotateTowardsCamera();
    }
    public void Move(Vector2 direction)
    {
        _velocity.x = direction.x * _speed;
        _velocity.y = _rb.velocity.y;
        _velocity.z = direction.y * _speed;
        _velocity = _rb.transform.TransformVector(_velocity);
        _rb.velocity = _velocity;
    }
    private void RotateTowardsCamera()
    {
        Quaternion camRot = Quaternion.identity;
        camRot.eulerAngles = new Vector3(0, _cam.transform.rotation.eulerAngles.y, 0);
        _rb.rotation = camRot;
    }
}
