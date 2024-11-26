using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FirstCheck : MonoBehaviour
{
    public bool isFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            isFirst = bool.Parse(File.ReadAllText(@"Assets\FC\fc.txt"));
        }
        catch
        {
            File.WriteAllText(@"Assets\FC\fc.txt", "false");
            isFirst = true;
            return;
        }
        
        
    }

    
}
