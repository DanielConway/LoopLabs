using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ScriptableObjects/NewRecipe", order = 1)]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public Sprite icon;
    public Part product;
    public Part[] ingredients;

    public GameObject productGO;
    public float timeToMake;
}
