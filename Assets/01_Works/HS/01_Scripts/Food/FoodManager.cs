using System;
using System.Collections.Generic;

public class FoodManager : MonoSingleton<FoodManager>
{
    private Dictionary<FoodType, int> _foods = new Dictionary<FoodType, int>();

    private void Awake()
    {
        _foods.Add(FoodType.Egg, 0);
        _foods.Add(FoodType.Pizza, 0);
        _foods.Add(FoodType.Yogurt, 0);
        _foods.Add(FoodType.HotDog, 0);
        _foods.Add(FoodType.BoxLunch, 0);
        _foods.Add(FoodType.GimBap, 0);
    }
}
