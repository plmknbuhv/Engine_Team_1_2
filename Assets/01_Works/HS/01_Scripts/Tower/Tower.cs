using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Dictionary<Type, ITowerComponent> _components;

    private void Awake()
    {
        _components = new Dictionary<Type, ITowerComponent>();
    
        GetComponentsInChildren<ITowerComponent>().ToList()
            .ForEach(x => _components.Add(x.GetType(), x));
    
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
    }
    
    public T GetCompo<T>() where T : class
    {
        Type type = typeof(T);
        if (_components.TryGetValue(type,out ITowerComponent compo))
        {
            return compo as T;
        }
    
        return default;
    }
}
