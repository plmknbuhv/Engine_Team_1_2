using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : Enemy
{
    
    public override void UniqueSkill()
    {
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            //StartCoroutine("a");
            
        }
        
    }


    //public IEnumerator a()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    transform.position = new Vector3(Random.Range(-5, 5), transform.position.y);
    //    timer = 0;
    //}
}
