using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GGMPool;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;


[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour, IPoolable
{
    [SerializeField] private AudioMixerGroup sfxGroup, musicGroup;
    [field:SerializeField] public PoolTypeSO PoolType { get; private set; }
    public GameObject GameObject => gameObject;
    private Pool _myPool;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundSO data)
    {
        if (data.audioType == AudioType.SFX)
        {
            _audioSource.outputAudioMixerGroup = sfxGroup;
        }
        else if(data.audioType == AudioType.Music)
        {
            _audioSource.outputAudioMixerGroup = musicGroup;
        }

        _audioSource.volume = data.volume;
        _audioSource.pitch = data.basePitch;
        if (data.randomizePitch)
        {
            _audioSource.pitch 
                += Random.Range(-data.randomPitchModifier, data.randomPitchModifier);
        }

        _audioSource.clip = data.clip;
        _audioSource.loop = data.loop;

        if (!data.loop)
        {
            float time = _audioSource.clip.length + 0.2f;
            DOVirtual.DelayedCall(time, () => _myPool.Push(this));
        }
        _audioSource.Play();
    }

    public void StopAndGoToPool()
    {
        _audioSource.Stop();
        _myPool.Push(this);
    }

    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }

    public void ResetItem()
    {
        
    }
}
