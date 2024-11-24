using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    Image fade;


    private void Awake()
    {
        fade = GetComponent<Image>();

    }


    public IEnumerator FadeInOut(float inTime, float wait, float outTime)
    {
        StartCoroutine(FadeIn(inTime));

        yield return new WaitForSeconds(inTime + wait);

        StartCoroutine(FadeOut(outTime));
    }

    public IEnumerator FadeIn(float duration)
    {
        if (duration == 0)
        {
            fade.color = new Color(255, 255, 255, 255);
            yield break;
        }

        float time = 0;

        while (time / duration < 1f)
        {
            time += Time.deltaTime;
            float a = Mathf.Lerp(0, 1, time / duration);
            fade.color = new Color(255, 255, 255, a);


            yield return null;
        }

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Ending Scene");

    }
    public IEnumerator FadeOut(float duration)
    {
        
        if (duration == 0)
        {
            fade.color = new Color(255, 255, 255, 0);
            yield break;
        }

        float time = 0;

        while (time / duration < 1f)
        {
            time += Time.deltaTime;
            float a = Mathf.Lerp(1, 0, time / duration);
            fade.color = new Color(255, 255, 255, a);

            yield return null;
        }

    }
}
