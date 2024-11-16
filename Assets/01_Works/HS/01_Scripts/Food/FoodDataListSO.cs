using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FoodDataListSO")]
public class FoodDataListSO : ScriptableObject
{
    public List<FoodDataSO> normalFoodDataList = new List<FoodDataSO>();
    public List<FusionFoodDataSO> fusionFoodDataList = new List<FusionFoodDataSO>();
}
