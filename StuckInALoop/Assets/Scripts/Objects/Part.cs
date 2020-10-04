using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Part", menuName = "ScriptableObjects/NewPart", order = 2)]
public class Part :ScriptableObject
{
    public string partName;
    public Sprite icon;
}
