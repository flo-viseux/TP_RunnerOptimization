using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEditor.Rendering;
using UnityEngine;

public class PoolingSystem<T, J> where T : MonoBehaviour where J : Enum
{
    private Dictionary<J, List<T>> _PoolDictionary = new Dictionary<J, List<T>>();
    private Transform _PoolFolder;
    private Transform _ActivePoolFolder;
    private Dictionary<J, T> _PoolPrefabs;

    public PoolingSystem(
        [Tooltip("J = enum of element | T = type of element | int = Base number of elements in pool ")]
        Dictionary<J, Tuple<T, int>> poolPrefabs, 
        Transform poolFolder, Transform activePoolFolder)
    {
        _PoolFolder = poolFolder;
        _ActivePoolFolder = activePoolFolder;
        _PoolPrefabs = new Dictionary<J, T>();
        foreach (var element in poolPrefabs) _PoolPrefabs.Add(element.Key, element.Value.Item1);
        foreach(KeyValuePair<J, Tuple<T, int>> pair in poolPrefabs)
        {
            _PoolDictionary.Add(pair.Key, new List<T>());
            Debug.Log(_PoolDictionary[pair.Key].Count);
            for (int i = 0; i < pair.Value.Item2; i++)
            {
                _PoolDictionary[pair.Key].Add(UnityEngine.Object.Instantiate(poolPrefabs[pair.Key].Item1, _PoolFolder));
                _PoolDictionary[pair.Key][i].gameObject.SetActive(false);
            }
        }
    }

    public T Get(J type, Vector3 position)
    {
        foreach(var item in _PoolDictionary[type])
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.transform.position = position;
                item.gameObject.SetActive(true);
                item.transform.parent = _ActivePoolFolder.transform;
                return item;
            }
        }
        _PoolDictionary[type].Add(UnityEngine.Object.Instantiate(_PoolPrefabs[type],position, Quaternion.identity, _ActivePoolFolder));
        return _PoolDictionary[type].Last();
    }

    public void Release(T item)
    {
        item.gameObject.SetActive(false);
        item.transform.parent = _PoolFolder.transform;
    }

    
}
