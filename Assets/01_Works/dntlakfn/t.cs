using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class t : MonoBehaviour
{
    TextMeshProUGUI text;
    

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        
    }


    public IEnumerator FadeInOut(float inTime, float wait, float outTime)
    {
        StartCoroutine(FadeIn(inTime));

        yield return new WaitForSeconds(inTime + wait);

        StartCoroutine(FadeOut(outTime));
    }

    public IEnumerator FadeIn(float duration)
    {
        if(duration == 0)
        {
            text.color = new Color(255, 255, 255, 255);
            yield break;
        }

        float time = 0;

        while(time / duration < 1f)
        {
            time += Time.deltaTime;
            float a = Mathf.Lerp(0, 1, time / duration);
            text.color = new Color(255, 255, 255, a);
            

            yield return null;
        }
        
    }
    public IEnumerator FadeOut(float duration)
    {
        if (duration == 0)
        {
            text.color = new Color(255, 255, 255, 0);
            yield break;
        }

        float time = 0;

        while (time / duration < 1f)
        {
            time += Time.deltaTime;
            float a = Mathf.Lerp(1, 0, time / duration);
            text.color = new Color(255, 255, 255, a);

            yield return null;
        }
        
    }
}
