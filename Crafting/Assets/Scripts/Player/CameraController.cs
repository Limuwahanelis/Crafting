using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _mouseRotationSpeed = 0.2f;
    [SerializeField] float _upperVerticalLimit;
    [SerializeField] float _lowerVerticalLimit;
    [SerializeField] GameObject _focalPoint;
    [SerializeField] GameObject _objectToFollow;

    private Vector3 _offset;
    float yaw = 0.0f;
    float pitch=0.0f;
    private Quaternion _rotation;
    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - _objectToFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = _objectToFollow.transform.position + (_rotation * _offset);
    }
    public void RotateMouse(Vector2 delta)
    {
        yaw += _mouseRotationSpeed * delta.x;
        pitch -= _mouseRotationSpeed * delta.y;
        // we are subtracting therfore we have to reverse the limits
        pitch = math.clamp(pitch, -_upperVerticalLimit, -_lowerVerticalLimit );
        Rotate();
    }
    private void Rotate()
    {
        if (pitch >= 360 || pitch < -360) pitch = 0;
        if (yaw > 360 || yaw < -360) yaw = 0;
        Quaternion rot = Quaternion.Euler(pitch, yaw, 0);
        _focalPoint.transform.rotation = rot;
    }
}
