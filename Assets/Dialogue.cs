using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
[System.Serializable] 
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public List<string> sentences;
    
    //Intead of an array string[], in LLM you want 
    // to change it dynamically, and List<string> is good at this.
    // but if you won't use LLM at all, you can just 
    // change the list back to array.
    // It doesn't matter really
}
