using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private GameObject titleUI;
    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private List<Image> pizzaImages;

    private bool _isButtonMove;
    
    private Coroutine _coroutine;
    private Sequence _sequence;
    
    
    private void Start()
    {
        MoveTitle();
    }

    public void GameOver()
    {
        Application.Quit();
    }

    private void MoveTitle()
    {
        _coroutine = StartCoroutine(TitleCoroutine());
    }

    private IEnumerator TitleCoroutine()
    {
        Tween UITween = titleUI.transform.DOMove(Vector3.zero, 1.1f).SetEase(Ease.InOutBack);
        
        yield return UITween.WaitForCompletion();
        yield return new WaitForSeconds(0.4f);
        
        Sequence sequence = DOTween.Sequence();
        sequence.
            Append(titleUI.transform.DOMove(new Vector3(-4.5f, 3.6f), 0.85f).SetEase(Ease.InOutSine))
            .Join(titleUI.transform.DOScale(Vector3.one / 1.6f, 0.85f).SetEase(Ease.InOutSine));
        
        yield return sequence.WaitForCompletion();

        _sequence = DOTween.Sequence();
        _sequence.Append(titleUI.transform.DORotate(new Vector3(0, 0, -5), 1).SetEase(Ease.Linear))
            .AppendCallback(() =>
                titleUI.transform.DORotate(new Vector3(0, 0, 5), 2).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo));

        yield return new WaitForSeconds(0.3f);

        foreach (var button in buttons)
        {
            button.transform.DOMoveX(13.85f, 1).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void OnEnterButton(int num)
    {
        if (_isButtonMove) return;
        buttons[num].transform.DORotate(new Vector3(0,0,-11.2f), 0.475f).SetEase(Ease.InOutSine);
    }
    
    public void OnExitButton(int num)
    {
        buttons[num].transform.DORotate(new Vector3(0,0,0), 0.475f).SetEase(Ease.InOutSine);
    }

    public void OnStartButtonClicked()
    {
        StartCoroutine(StartTranslateCoroutine());
    }

    private IEnumerator StartTranslateCoroutine()
    {
        _isButtonMove = true;
        
        foreach (var button in buttons)
            button.transform.DORotate(new Vector3(0,0,0), 0.65f).SetEase(Ease.InOutSine);
        foreach (var button in buttons)
        {
            button.transform.DOMoveX(24.5f, 1).SetEase(Ease.InBack);
            yield return new WaitForSeconds(0.2f);
        }
        
        yield return new WaitForSeconds(0.45f);

        for (int i = 0; i < 4; i++)
        {
            pizzaImages[i].transform.DOLocalMoveX(-930 + (i * 610), 1.2f).SetEase(Ease.OutBack);
            pizzaImages[i].transform.DORotate(new Vector3(0, 0, 360), 1.2f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutBack);

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);
        
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        DOTween.KillAll();
        
        SceneManager.LoadScene("GameScene");
    }
}
