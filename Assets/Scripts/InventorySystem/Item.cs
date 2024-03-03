using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Narrative/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea(3, 10)]
    public string itemDescription;
    public Sprite sprite;
}
