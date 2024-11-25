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
            File.OpenWrite("C:/ch.txt");
            
        }
        catch
        {
            File.Create("C:/ch.txt");
            isFirst = true;
            return;
        }
        isFirst = false;
        
    }

    
}
