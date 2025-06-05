using System.Collections.Generic;
using UnityEngine;

public class UIObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T prefab;
    private readonly Queue<T> pool = new Queue<T>();

    public T Get()
    {
        T item = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab, transform);
        item.gameObject.SetActive(true);

        // IPoolable ‚É‘Î‰‚µ‚Ä‚½‚çƒv[ƒ‹“n‚·
        if (item is IPoolable<T> poolable)
        {
            poolable.OnCreated(this);
        }

        return item;
    }

    public void Return(T item)
    {
        pool.Enqueue(item);

    }
}
