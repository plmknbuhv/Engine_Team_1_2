using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoSingleton<MenuManager>
{
    public RectTransform menuRectTrm;
    public Image recipeImage;
    public Transform recipeParent;
    
    public bool isCanActiveSetting = true;
    public bool isMenuActivating;
    public bool isRecipeActivating;
    
    private bool _isMenuOpen;
    private bool _isRecipeOpen;

    [SerializeField] private Vector3 defaultRotation = new Vector3(-1, 90, 17.5f);

    private void Awake()
    {
        recipeImage.gameObject.SetActive(true);
        recipeImage.transform.rotation = Quaternion.Euler(defaultRotation);
        SetupRecipe();
    }

    public void SetupRecipe()
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
        if (!isCanActiveSetting) return;
        if (isMenuActivating) return;
        if (isRecipeActivating) return;
        
        StartCoroutine(OpenMenuCoroutine());
    }

    private IEnumerator OpenMenuCoroutine()
    {
        isMenuActivating = true;
        
        Tween moveTween = menuRectTrm.transform.DOMoveY(
            _isMenuOpen ? 16f : 0, 0.9f).SetEase(Ease.OutBack);
        _isMenuOpen = !_isMenuOpen;
        
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
            tween = recipeImage.transform.DORotate(defaultRotation, 1);
        
        _isRecipeOpen = !_isRecipeOpen;
        
        yield return tween.WaitForCompletion();
        
        isRecipeActivating = false;
    }
}
