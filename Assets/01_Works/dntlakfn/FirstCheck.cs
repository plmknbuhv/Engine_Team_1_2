using System.IO;
using UnityEngine;

public class FirstCheck : MonoBehaviour
{
    public bool CheckIsFirstPlay()
    {
        try
        {
            return bool.Parse(File.ReadAllText(@"Assets\FC\fc.txt"));
        }
        catch
        {
            File.WriteAllText(@"Assets\FC\fc.txt", "false");
            return true;
        }   
    }
}
