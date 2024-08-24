using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    private Vector2 _moveDirection;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _playerMovement.Move(_moveDirection);
    }
    public void OnMove(InputValue inputValue)
    {
        _moveDirection = inputValue.Get<Vector2>();
        
    }
}
