
using UnityEngine;

public interface IPickable
{
    public Sprite ItemSprite { get; }
    public string Name { get;}
    public string Description { get;}
    public void PickUp();
}