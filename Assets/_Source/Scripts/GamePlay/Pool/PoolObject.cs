using System.Collections.Generic;
using UnityEngine;

public class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T[] _prefabs;

    private Queue<T> _poolObjects = new();

    private void Awake()
    {
        FillPool();
    }

    public T GetObject()
    {
        if (_poolObjects.Count == 0)
        {
            CreateObject();
        }

        var item = _poolObjects.Dequeue();

        item.gameObject.SetActive(true);

        return item;
    }

    public void ReturnObject(T item)
    {
        if (item != null)
        {
            item.transform.SetParent(null, false);
            item.gameObject.SetActive(false);
            _poolObjects.Enqueue(item);
        }
    }

    public void ReaturnAllObject()
    {
        T[] items = Object.FindObjectsOfType<T>(false);

        foreach (var obj in items)
        {
            if (obj != null)
            {
                ReturnObject(obj);
            }
        }
    }

    private void CreateObject()
    {
        T item;

        if (_poolObjects.Count != 0)
        {
            item = GetObject();
        }
        else
        {
            int randomItem = RandomNumber.Create(0, _prefabs.Length);
            item = Instantiate(_prefabs[randomItem]);
        }

        item.gameObject.SetActive(false);

        _poolObjects.Enqueue(item);
    }

    private void FillPool()
    {
        T item;

        for (int i = 0; i < _prefabs.Length; i++)
        {
            item = Instantiate(_prefabs[i]);
            item.gameObject.SetActive(false);
            _poolObjects.Enqueue(item);
        }
    }
}
