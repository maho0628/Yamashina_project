using System.Collections.Generic;
using UnityEngine;

public class UIObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T prefab;
    private readonly Queue<T> pool = new Queue<T>();

    public T Get()
    {
        T item = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab);
        item.gameObject.SetActive(true);
        return item;
    }

    public void Return(T item)
    {
        item.gameObject.SetActive(false);
        pool.Enqueue(item);
    }
}
