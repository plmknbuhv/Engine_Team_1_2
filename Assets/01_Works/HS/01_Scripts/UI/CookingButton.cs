using System.Collections;
using TMPro;
using UnityEngine;

public class CookingButton : MonoBehaviour
{
    private TextMeshProUGUI _buttonText;
    [SerializeField] private float timeToShow = 1.2f;
    private float _showTimer;
    private Coroutine _textDelayCor;

    private void Awake()
    {
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnClick(bool isCooking)
    {
        if (_textDelayCor != null)
        {
            StopCoroutine(_textDelayCor);
            _showTimer = 0;
        }
        _buttonText.text = isCooking ? "Success" : "Failed";
        _textDelayCor = StartCoroutine(ShowingTextCoroutine());
    }

    private IEnumerator ShowingTextCoroutine()
    {
        while (true)
        {
            yield return null;
            _showTimer += Time.deltaTime;
            if (_showTimer >= timeToShow)
            {
                _buttonText.text = "Cooking";
                _showTimer = 0;
                break;
            }
        }
    }
}
