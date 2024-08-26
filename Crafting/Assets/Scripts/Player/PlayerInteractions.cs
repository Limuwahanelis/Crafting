using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] Transform _cameraTrans;
    [SerializeField] LayerMask _interactionMask;
    private IInteractable _interactable;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(_cameraTrans.position, _cameraTrans.forward, out RaycastHit hit, 10f, _interactionMask))
        {
            _interactable?.Deselect();
            _interactable = hit.transform.gameObject.GetComponent<IInteractable>();
            if (_interactable != null) _interactable.Select();
        }
        else
        {
            _interactable?.Deselect();
            _interactable = null;
        }
    }

    public void InteractWithSelectedItem()
    {
        if(_interactable==null) return;
        _interactable.Interact();
        
    }
    private void OnDrawGizmosSelected()
    {
        if(_cameraTrans != null) Gizmos.DrawRay(_cameraTrans.position, _cameraTrans.forward * 10f);

    }
}
