using System.Collections.Generic;

public class FoodManager : MonoSingleton<FoodManager>
{
    private Dictionary<FoodType, int> _foods = new Dictionary<FoodType, int>();

    private void Awake()
    {
        
    }
}