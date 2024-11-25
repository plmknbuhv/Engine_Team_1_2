using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private List<Transform> tutorialTrm = new List<Transform>();
    private int _tutorialNum;

    private void Start()
    {
        StartMove();
    }

    public void StartMove()
    {
        DOTween.KillAll();

        if (_tutorialNum == 3)
        {
            gameObject.SetActive(false);
            return;
        }
            
        if (_tutorialNum - 1 >= 0)
            tutorialTrm[_tutorialNum - 1].gameObject.SetActive(false);
        tutorialTrm[_tutorialNum].gameObject.SetActive(true);

        foreach (Transform trm in tutorialTrm[_tutorialNum])
        {
            if (trm.TryGetComponent<InfoText>(out var text))
            {
                text.transform.DOMoveX(text.transform.position.x + (text.isRight ? -0.425f : 0.425f), 0.7f)
                    .SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
            }
        }

        _tutorialNum++;
    }
}
