using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class ImpulseFeedback : Feedback
{
    [SerializeField] private float impulsePower;
    
    private CinemachineImpulseSource _source;

    private void Awake()
    {
        _source = GetComponent<CinemachineImpulseSource>();
    }

    public override void PlayFeedback()
    {
        _source.GenerateImpulse(impulsePower);
    }

    public override void StopFeedback()
    {
        
    }
}