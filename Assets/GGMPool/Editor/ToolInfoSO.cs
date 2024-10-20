using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/ToolInfo")]
public class ToolInfoSO : ScriptableObject
{
    [Header("PoolManager")]
    public string poolingFolder = "Assets/01_Works/HS/08_SO/Pool";
    public string poolAssetName = "PoolManager.asset";
    public string typeFolder = "Types";
    public string itemFolder = "Items";
}
