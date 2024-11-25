using GGMPool;
using UnityEngine;

public class SoundFeedback : Feedback
{
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO poolType;
    [SerializeField] private SoundSO sound;
    
    public override void PlayFeedback()
    {
        SoundPlayer soundPlayer = poolManager.Pop(poolType) as SoundPlayer;
        
        soundPlayer?.PlaySound(sound);
    }

    public override void StopFeedback()
    {
        
    }
}
