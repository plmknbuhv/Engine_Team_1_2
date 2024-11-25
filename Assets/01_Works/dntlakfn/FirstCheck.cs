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
            File.OpenWrite(@"Assets\FC\fc.txt");
            File.WriteAllText(@"Assets\FC\fc.txt", "false");
            
            
        }
        catch
        {
            File.Create(@"Assets\FC\fc.txt");
            isFirst = true;
            return;
        }
        isFirst = bool.Parse(File.ReadAllText(@"Assets\FC\fc.txt"));
        
    }

    
}
