using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoSingleton<MenuManager>
{
    public RectTransform menuRectTrm;
    public Image recipeImage;
    public Transform recipeParent;
    public List<Image> pizzaImages;
    
    public bool isCanActiveMenu = true;
    public bool isMenuActivating;
    public bool isRecipeActivating;
    
    public bool isMenuOpen;
    private bool _isRecipeOpen;
    private bool _isStart;

    [SerializeField] private Vector3 defaultRotation = new Vector3(-1, 90, 17.5f);

    [SerializeField] private List<Graphic> gameOverGraphics;

    private void Start()
    {
        recipeImage.gameObject.SetActive(true);
        foreach (var pizza in pizzaImages)
            pizza.gameObject.SetActive(true);
        recipeImage.transform.rotation = Quaternion.Euler(defaultRotation);
        SetupRecipe();
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        for (int i = 0; i < 4; i++)
        {
            pizzaImages[i].transform.DOLocalMoveX(-3450 + (i * 610), 1.2f).SetEase(Ease.InBack);
            pizzaImages[i].transform.DORotate(new Vector3(0, 0, 360), 1.2f, RotateMode.FastBeyond360).SetEase(Ease.InBack);

            yield return new WaitForSeconds(0.2f);
        }

        _isStart = true;
    }

    private void SetupRecipe()
    {
        List<FoodDataSO> foodDataList = new List<FoodDataSO>();
        List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();
        
        foreach (var food in InventoryManager.Instance.foodDataList.normalFoodDataList)
            foodDataList.Add(food);
        foreach (var food in InventoryManager.Instance.foodDataList.fusionFoodDataList)
            foodDataList.Add(food);

        foreach (Transform recipe in recipeParent)
            textList.Add(recipe.GetComponent<TextMeshProUGUI>());
        
        for (int i = 0; i < 22; i++)
        {
            textList[i].text = $"공격속도 : {foodDataList[i].attackCooldown}초\n" +
                               $"데미지 : {foodDataList[i].damage}\n" +
                               $"{foodDataList[i].foodInstruction}";
        }
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            OpenSetting();
    }

    public void OpenSetting()
    {
        if (!_isStart) return;
        if (!isCanActiveMenu) return;
        if (isMenuActivating) return;
        if (isRecipeActivating) return;
        if (InventoryManager.Instance.kitchen.isOpen) return;
        
        StartCoroutine(OpenMenuCoroutine());
        if(_isRecipeOpen)
            StartCoroutine(OpenRecipeCoroutine());
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    private IEnumerator OpenMenuCoroutine()
    {
        isMenuActivating = true;

        if (_isRecipeOpen)
            yield return new WaitForSeconds(0.1f);
        
        Tween moveTween = menuRectTrm.transform.DOMoveY(
            isMenuOpen ? 16f : 0, 0.9f).SetEase(Ease.OutBack);
        isMenuOpen = !isMenuOpen;
        
        yield return moveTween.WaitForCompletion();
        
        isMenuActivating = false;
        
    }

    public void OpenRecipe()
    {
        StartCoroutine(OpenRecipeCoroutine());
    }

    private IEnumerator OpenRecipeCoroutine()
    {
        isRecipeActivating = true;
        Tween tween;
        if (!_isRecipeOpen)
            tween = recipeImage.transform.DORotate(new Vector3(0, 0, 0), 1);
        else
            tween = recipeImage.transform.DORotate(defaultRotation, 0.9f);
        
        _isRecipeOpen = !_isRecipeOpen;
        
        yield return tween.WaitForCompletion();
        
        isRecipeActivating = false;
    }

    public void GameOver()
    {
        StartCoroutine(StartDeadCoroutine());
    }
    
    private IEnumerator StartDeadCoroutine()
    {
        yield return new WaitForSeconds(0.35f);
            
        gameOverGraphics[2].gameObject.SetActive(true);
        gameOverGraphics[0].gameObject.SetActive(true);
        gameOverGraphics[0].DOFade(1, 1);
        gameOverGraphics[1].gameObject.SetActive(true);
        gameOverGraphics[1].DOFade(1, 1);
        
        yield return new WaitForSeconds(1.3f);

        var gameOverText = gameOverGraphics[1] as TextMeshProUGUI;
        
        Tween tween = DOTween.To(() => 10f, goldValue 
            => gameOverText.text = "전자레인지가 파괴되었습니다.\n" +
                                   $"잠시 후 타이틀 화면으로 돌아갑니다...{(int)goldValue}", 0f, 10f).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();
        DOTween.KillAll();
        
        SceneManager.LoadScene("TitleScene");
    }
}
