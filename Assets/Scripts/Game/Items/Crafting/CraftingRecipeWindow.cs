using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeWindow : MonoBehaviour
{

    [SerializeField] CraftingRecipeUI recipeUI;

    public List<CraftingRecipe> craftingRecipes;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
