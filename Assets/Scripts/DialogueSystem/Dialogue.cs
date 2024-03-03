using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public CharType charcterType;
    [TextArea(3, 10)]
    public string sentence;
}

public enum CharType
{
    Self,
    Main,
    Narrator
}