using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventorySlotInteractionsUI : MonoBehaviour
{
    public Action OnMenuClosed;
    [SerializeField] Button _throwButton;
    [SerializeField] InputActionReference _selectActionAction;
    private bool _isPointerIn = false;
    private void OnEnable()
    {
        _selectActionAction.action.performed += TryCloseMenu;
    }
    public void SetThrowButton(InventorySlot inventorySlot,Action actionToPerformOnPress=null)
    {
        _throwButton.onClick.RemoveAllListeners();
        _throwButton.onClick.AddListener(() => { inventorySlot.ThrowItemFromSlot(); actionToPerformOnPress?.Invoke(); });
    }
    public void SetUIPosition(Vector3 position)
    {
        GetComponent<RectTransform>().position = position;
    }
    public void SetUIVisibility(bool value)
    {
        gameObject.SetActive(value);
    }

    public void PointerIsIn(BaseEventData eventData)
    {
        _isPointerIn = true;
    }
    public void PointerIsOut(BaseEventData eventData)
    {
        _isPointerIn = false;
    }

    private void TryCloseMenu(InputAction.CallbackContext callback)
    {
        if (!_isPointerIn)
        {
            gameObject.SetActive(false);
            OnMenuClosed?.Invoke();
        }
    }
    private void OnDisable()
    {
        _selectActionAction.action.performed -= TryCloseMenu;
    }
}
