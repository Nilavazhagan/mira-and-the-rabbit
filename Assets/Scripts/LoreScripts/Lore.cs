using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Narrative/Lore")]
public class Lore : ScriptableObject
{
    public string title;
    [TextArea(3, 10)]
    public string description;
}
