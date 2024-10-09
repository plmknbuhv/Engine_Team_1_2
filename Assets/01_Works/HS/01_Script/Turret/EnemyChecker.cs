using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : MonoBehaviour, ITurretComponent
{
    private Turret _turret;
    
    public void Initialize(Turret turret)
    {
        _turret = turret;
    }
    
    public List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }
}
