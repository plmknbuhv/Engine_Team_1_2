using UnityEngine;

public enum  FoodType
{
    Egg,
    BoxLunch,
    GimBap,
    HotDog,
    Pizza,
    Yogurt
}

[CreateAssetMenu(menuName = "SO/FoodDataSO")]
public class FoodDataSO : ScriptableObject
{
    public Sprite sprite;
    public FoodType foodType;
}
