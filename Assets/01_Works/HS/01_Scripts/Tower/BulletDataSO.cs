using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BulletDataSO")]
public class BulletDataSO : ScriptableObject
{
    public List<Sprite> bulletSprites = new List<Sprite>();
}
