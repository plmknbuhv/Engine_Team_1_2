using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class t : MonoBehaviour
{
    

    public IEnumerator Show()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
