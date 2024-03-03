using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Narrative/Conversation")]
public class Conversation : ScriptableObject
{
    public Dialogue[] dialogues;
}
