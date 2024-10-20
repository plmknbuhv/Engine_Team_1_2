using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    private Tower _tower;

    private void Awake()
    {
        _tower = GetComponent<Tower>();
    }
}
