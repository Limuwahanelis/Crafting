using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] PlayerInteractions _playerInteractions;
    [SerializeField] PlayerInventory _playerInventory;
    [SerializeField] InputActionReference _mouseDeltaAction;
    private Vector2 _moveDirection;
    private bool _isInventoryOpen = false;
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
    public void OnAttack()
    {
        _playerInteractions.InteractWithSelectedItem();
    }
    public void OnOpenInventory()
    {
        _isInventoryOpen = !_isInventoryOpen;
        if(_isInventoryOpen) _mouseDeltaAction.action.Disable();
        else _mouseDeltaAction.action.Enable();
        _playerInventory.gameObject.SetActive(_isInventoryOpen);
    }
}
