using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public abstract void PlayFeedback();
    public abstract void StopFeedback();

    private void OnDisable()
    {
        StopFeedback();
    }
}