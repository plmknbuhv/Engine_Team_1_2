using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoSingleton<MenuManager>
{
    public GameObject blurPanel;
    public RectTransform menuRectTrm;
    
    public bool isCanActiveSetting = true;
    public bool isActivating;
    
    private void Update()
    {
        OpenSetting();
    }

    private void OpenSetting()
    {
        if (!isCanActiveSetting) return;
        if (isActivating) return;
        
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            blurPanel.SetActive(!blurPanel.activeSelf);
            // Time.timeScale = blurPanel.activeSelf ? 0 : 1;
            menuRectTrm.DOMove(Vector3.down * 5, 1);
            StartCoroutine(OpenMenuCoroutine());
        }
    }

    private IEnumerator OpenMenuCoroutine()
    {
        isActivating = true;
        Tween moveTween = menuRectTrm.transform.DOScale(
            blurPanel.activeSelf ? Vector3.zero : Vector3.one, 1.25f);
        yield return moveTween.WaitForCompletion();
        isActivating = false;
    }
}
