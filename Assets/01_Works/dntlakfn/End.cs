using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class End : MonoBehaviour
{
    float time;
    public Animator animator;
    public TextMeshProUGUI t;
    // Start is called before the first frame update
    void Start()
    {
        animator.speed = 0;
        t.gameObject.SetActive(false);
        StartCoroutine(GetComponent<Fade>().FadeOut(3));
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 3)
        {
            animator.speed = 1;
            
        }
        if(time > 5.5f)
        {
            t.gameObject.SetActive(true);
            t.transform.DOScale(new Vector3(1, 1, 1), 1.5f);
        }
        if(time > 7)
        {

            StartCoroutine(WariGari());
            time = 3f;
                
        }
        
        

        IEnumerator WariGari()
        {


            float r = Random.Range(10, 13);

            t.transform.DORotate(new Vector3(0, 0, r), 2);

            yield return new WaitForSeconds(2);

            t.transform.DORotate(new Vector3(0, 0, -r), 2);
            
        }

    }
}
