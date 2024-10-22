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
    public FoodType foodType;
    public Sprite sprite;
    public float damage;
    public float attackCooldown
}
