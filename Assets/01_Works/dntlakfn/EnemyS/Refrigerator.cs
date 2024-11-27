using DG.Tweening;
using GGMPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Refrigerator : Enemy
{
    bool bd;
    float time;
    
    // 보스, 큼, 넉백,스턴 안받음, 느림

    private void Start()
    {
        UniqueSkill();
        EnemySpawner.isBossLive = true;
    }

    protected override void UniqueSkill()
    {
        explosion.transform.localScale *= 3;
    }
    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, 0, () =>
        {
            if (isDead)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                isDead = false;
                speed = 0;
                animator.enabled = false;

                var boom = poolManager.Pop(explosion.PoolType);
                boom.GameObject.transform.position = transform.position;
                boom.GameObject.transform.localScale = new Vector3(3,3,3);
                bd = true;

                
                transform.DOShakePosition(5, 2.5f, 1000);
                var fade = FindAnyObjectByType<Fade>();
                fade.gameObject.SetActive(true);
                StartCoroutine(fade.FadeIn(5));
            }

        });
    }

    public override void Update()
    {
        base.Update();
        if (bd)
        {
            time += Time.deltaTime;
            if(time > 1f)
            {
                var boom = poolManager.Pop(explosion.PoolType);
                boom.GameObject.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-1f, 1f));
                boom.GameObject.transform.localScale = new Vector3(3, 3, 3);
                time = 0f;
            }
        }
    }

    private void OnDisable()
    {
        EnemySpawner.isBossLive = false;

    }

    


}
