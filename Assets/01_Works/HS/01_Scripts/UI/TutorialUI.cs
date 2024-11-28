using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private List<Transform> tutorialTrm = new List<Transform>();
    [SerializeField] private WaveBtn waveBtn;
    [SerializeField] private FirstCheck firstCheck;
    [SerializeField] private Button nextButton;
    
    private int _tutorialNum;
    public bool isTutorialEnd;

    private void Start()
    {
        if (firstCheck.CheckIsFirstPlay())
            StartMove();
        else
            waveBtn.OnStartGame();
    }

    public void StartMove()
    {
        DOTween.KillAll();

        nextButton.gameObject.SetActive(true);
        if (_tutorialNum == 5)
        {
            isTutorialEnd = true;
            waveBtn.OnStartGame();
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
