using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FusionFoodDataSO")]
public class FusionFoodDataSO : FoodDataSO
{
    public List<FoodDataSO> ingredients = new List<FoodDataSO>();
}
