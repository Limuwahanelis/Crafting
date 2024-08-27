using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Crafting resource type",fileName ="New crafting resource ")]
public class CraftingResourceType : ScriptableObject
{
    public Sprite ResourceSprite => _resourceSprite;
    [SerializeField] Sprite _resourceSprite;
}
