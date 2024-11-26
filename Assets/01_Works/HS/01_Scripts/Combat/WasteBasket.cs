using System;
using DG.Tweening;
using GGMPool;
using UnityEngine;

public class WasteBasket : MonoBehaviour
{
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO poolType;
    
    private FeedbackPlayer _feedbackPlayer;

    private void Awake()
    {
        _feedbackPlayer = GetComponentInChildren<FeedbackPlayer>();
    }

    public void ThrowGarbage(Vector3 position)
    {
        _feedbackPlayer.PlayFeedbacks();
        var effect = poolManager.Pop(poolType) as FoodEffect;
        effect.SetPositionAndPlay(position);
        transform.DOShakePosition(1f, 10, 10);
    }
}
