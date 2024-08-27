using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventorySlotDragging : MonoBehaviour
{
    [SerializeField] PlayerInventory _inventory;
    [SerializeField] InputActionReference _mousePosAction;
    [SerializeField] InputActionReference _hold;
    [SerializeField] GameObject _draggingSlotImage;
    private bool _isPointerOutsideInventory = false;
    private InventorySlot _currentlyPointerHoveringOverSlot;
    private InventorySlot _draggedSlot;
    private Vector2 _mousePos;
    private bool _isDraggingSlot = false;
    // Start is called before the first frame update
    void Start()
    {
        _hold.action.performed += DragPerformed;
        _hold.action.canceled += DragCancelled;
        _mousePosAction.action.performed += UpdateMousePos;
        foreach (InventorySlot slot in _inventory.InventorySlots)
        {
            slot.GetComponent<InventorySlotPlayerInteractions>().OnPointerEntered += PointerEnterSlot;
            slot.GetComponent<InventorySlotPlayerInteractions>().OnPointerExited += PointerExitSlot;
        }
    }
    private void Update()
    {
        if (_isDraggingSlot) _draggingSlotImage.transform.position = _mousePos;
    }
    public void OnPointerExitInventory(BaseEventData data)
    {
        _isPointerOutsideInventory = true;
    }
    public void OnPointerEnterInventory(BaseEventData data)
    {
        _isPointerOutsideInventory = false;
    }
    private void UpdateMousePos(InputAction.CallbackContext callback)
    {
        _mousePos = callback.ReadValue<Vector2>();
    }
    private void PointerEnterSlot(InventorySlot slot)
    {
        _currentlyPointerHoveringOverSlot = slot;
    }
    private void PointerExitSlot(InventorySlot slot)
    {
        if (_currentlyPointerHoveringOverSlot == slot) _currentlyPointerHoveringOverSlot = null;

    }
    private void DragPerformed(InputAction.CallbackContext callbackContext)
    {
        if (_currentlyPointerHoveringOverSlot != null && _currentlyPointerHoveringOverSlot.IsTaken)
        {
            _isDraggingSlot = true;
            _draggedSlot = _currentlyPointerHoveringOverSlot;
            _draggingSlotImage.GetComponent<Image>().sprite = _draggedSlot.ItemInSlot.ItemSprite;
            _draggingSlotImage.SetActive(true);

        }
    }
    private void DragCancelled(InputAction.CallbackContext callbackContext)
    {
        if (!_isDraggingSlot) return;
        _isDraggingSlot = false;
        if (_currentlyPointerHoveringOverSlot != null)
        {
            _draggingSlotImage.SetActive(false);
            if (_currentlyPointerHoveringOverSlot.IsTaken)
            {
                PickableItem tmp = _currentlyPointerHoveringOverSlot.ItemInSlot;
                _currentlyPointerHoveringOverSlot.SetSlot(_draggedSlot.ItemInSlot);
                _draggedSlot.SetSlot(tmp);
            }
            else
            {
                _currentlyPointerHoveringOverSlot.SetSlot(_draggedSlot.ItemInSlot);
                _draggedSlot.EmptySlot();
            }
        }
        else
        {
            if (_isPointerOutsideInventory)
            {
                _inventory.ThrowItem(_draggedSlot.ItemInSlot);
                _draggedSlot.EmptySlot();
            }
            _draggingSlotImage.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        _mousePosAction.action.performed -= UpdateMousePos;
        _hold.action.performed -= DragPerformed;
        _hold.action.canceled -= DragCancelled;
    }
}
