using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolingSystem<T, J> where T : MonoBehaviour where J : Enum
{
    public Dictionary<J, List<T>> _PoolDictionary = new Dictionary<J, List<T>>();
    public int _BasePoolSize;
    
    public T Get(J type)
    {
        return _PoolDictionary[type].First();
    }
    
}
