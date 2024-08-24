using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputHandler : MonoBehaviour
{
    [SerializeField] CameraController _camera;
    void OnMouseDelta(InputValue value)
    {
        _camera.RotateMouse(value.Get<Vector2>());
    }
    private void OnValidate()
    {
        if (_camera == null) _camera = FindObjectOfType<CameraController>();
    }
}
