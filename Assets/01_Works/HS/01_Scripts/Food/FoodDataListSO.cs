using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/FoodDataListSO")]
public class FoodDataListSO : ScriptableObject
{
    public List<FoodDataSO> normalFoodDataList = new List<FoodDataSO>();
    public List<FoodDataSO> fusionFoodDataList = new List<FoodDataSO>();
}
