using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventorySlotPlayerInteractions :MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] InventorySlot _inventorySlot;
    [SerializeField] InventorySlotInteractionsUI _slotInteractionUI;
    [SerializeField] InputActionReference _inspectItemAction;
    private bool _isPointerIn = false;
    private Vector3 _uiPos;
    private void Start()
    {
        _uiPos = transform.position;
        _uiPos.x +=GetComponent<RectTransform>().rect.width/2+ _slotInteractionUI.GetComponent<RectTransform>().rect.width/2;
        _inspectItemAction.action.performed += OpenActionSelectionMenu;
    }
    public void SetInteractions(bool value)
    {
        if (value)
        {
            _inspectItemAction.action.Enable();
        }
        else _inspectItemAction.action.Disable();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerIn = false;
    }
    private void CloseActionSelectionMenu()
    {
        _slotInteractionUI.SetUIVisibility(false);
    }
    private void OpenActionSelectionMenu(InputAction.CallbackContext callbackContext)
    {
        if (!_inventorySlot.IsTaken) return;
        if (_isPointerIn)
        {
            _slotInteractionUI.SetUIPosition(_uiPos);
            _slotInteractionUI.SetUIVisibility(true);
            _slotInteractionUI.SetThrowButton(_inventorySlot,CloseActionSelectionMenu);
        }
    }
    private void OnDestroy()
    {
        _inspectItemAction.action.performed -= OpenActionSelectionMenu;
    }
}
