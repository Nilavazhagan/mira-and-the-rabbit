using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string charName;
    public Sprite dialogueBox;

    private static Character _main;
    public static Character main
    {
        get
        {
            if(_main == null)
            {
                try
                {
                    _main = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
                }
                catch (System.Exception)
                {
                    Debug.LogError("Error while trying to find main player!!");
                    throw;
                }            
            }
            return _main;
        }
    }

    private void Awake()
    {
        if(this.tag == "Player")
        {
            _main = this;
        }
    }
}
