using System.IO;
using UnityEngine;

public class FirstCheck : MonoBehaviour
{
    public bool CheckIsFirstPlay()
    {
        try
        {
            return bool.Parse(File.ReadAllText(@"C:\Users\plmkn\문서"));
        }
        catch
        {
            File.WriteAllText(@"Assets\FC\fc.txt", "false");
            return true;
        }   
    }
}
