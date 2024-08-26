using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public struct CraftingRecipeShort
    {
        public CraftingResourceType[] resourceTypes;
        public int[] resourcesNum;
    }

    public List<CraftingResourceType> CraftingResources=>_craftingResources;
    [SerializeField] List<CraftingResourceType> _craftingResources;
    public GameObject Result=>_result;
    [SerializeField] GameObject _result;
    public Sprite ResultSprite => _resultSprite;
    [SerializeField] Sprite _resultSprite;

    public int SuccessChance=>_successChance;
    [SerializeField] int _successChance;
}
