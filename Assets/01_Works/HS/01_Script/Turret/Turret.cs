using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Dictionary<Type, ITurretComponent> _components;
    
    private void Awake()
    {
        GetComponentsInChildren<ITurretComponent>().ToList()
            .ForEach(x => _components.Add(x.GetType(),x));
        
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
    }

    public T GetCompo<T>() where T : class
    {
        Type type = typeof(T);
        if (_components.TryGetValue(type, out ITurretComponent compo))
        {
            return compo as T;
        }
        return default;
    }
}
