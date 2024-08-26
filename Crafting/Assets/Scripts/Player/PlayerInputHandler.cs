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
    [SerializeField] PlayerInput _playerInput;
    private Vector2 _moveDirection;
    private bool _isInventoryOpen = false;
    private string _defaultActionMap;
    // Start is called before the first frame update
    void Start()
    {
        _defaultActionMap = _playerInput.defaultActionMap;
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
    public void OnInteract()
    {
        _playerInteractions.InteractWithSelectedItem();
    }
    public void OnOpenInventory()
    {
        _isInventoryOpen = !_isInventoryOpen;
        if (_isInventoryOpen)
        {
            _playerInput.actions.FindActionMap(_defaultActionMap).Disable();
            _playerInput.actions.FindActionMap("Inventory").Enable();
        }
        else
        {
            _playerInput.actions.FindActionMap("Inventory").Disable();
            _playerInput.actions.FindActionMap(_defaultActionMap).Enable();
        }
        _playerInventory.gameObject.SetActive(_isInventoryOpen);
    }
}
