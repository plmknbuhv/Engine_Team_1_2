using System.IO;
using UnityEngine;

public class FirstCheck : MonoBehaviour
{
    public bool CheckIsFirstPlay()
    {
        try
        {
            return bool.Parse(File.ReadAllText(@"C:\fc.txt"));
        }
        catch
        {
            File.WriteAllText(@"C:\fc.txt", "false");
            return true;
        }   
    }
}
