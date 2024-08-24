using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionDescription { get;}

    public void Interact();
    public void Select();
    public void Deselect();
}
