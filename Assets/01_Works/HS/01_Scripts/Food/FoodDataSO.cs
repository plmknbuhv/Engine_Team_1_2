using UnityEngine;

public enum  FoodType
{
    Egg,
    BoxLunch,
    GimBap,
    HotDog,
    HotDogBread,
    Mustard,
    Pizza,
    Yogurt,
    Cider,
    Cheese,
    Bacchus,
    Mandu,
    Ramen,
    CheesePizza,
    GimBap2XL,
    TwoEgg,
    BacchusCider,
    PizzaPan,
    RamenMandu
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
