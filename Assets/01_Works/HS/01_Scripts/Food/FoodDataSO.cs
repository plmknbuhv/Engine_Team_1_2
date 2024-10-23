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
    public float attackCooldown;
    public int damage;
    public int width;
    public int height;
}
