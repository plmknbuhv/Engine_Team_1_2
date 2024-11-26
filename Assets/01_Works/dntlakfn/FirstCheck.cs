using System.IO;
using System.Text;
using UnityEngine;

public class FirstCheck : MonoBehaviour
{
    public bool CheckIsFirstPlay()
    {
        // Unity 권장 경로로 파일 저장
        string filePath = Path.Combine(Application.persistentDataPath, "fc.txt");

        try
        {
            // 파일 읽기
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
            {
                return bool.Parse(reader.ReadToEnd());
            }
        }
        catch
        {
            // 파일 생성 및 쓰기
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.Write("false");
            }
            return true;
        }
    }
}