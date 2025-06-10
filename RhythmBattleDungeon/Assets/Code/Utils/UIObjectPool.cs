using System.Collections.Generic;
using UnityEngine;

public class UIObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T prefab;
    private readonly Queue<T> pool = new Queue<T>();
    [SerializeField] private int preloadCount = 10;

    private void Awake()
    {
        for (int i = 0; i < preloadCount; i++)
        {
            T instance = Instantiate(prefab, transform);
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
        }
    }

    public T Get()
    {
     
        T item = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab, transform);
        item.gameObject.SetActive(true);

        // IPoolable Ç…ëŒâûÇµÇƒÇΩÇÁÉvÅ[ÉãìnÇ∑
        if (item is IPoolable<T> poolAble)
        {
            poolAble.OnCreated(this);
        }
        if(item is IUIEffectPoolable<T> uiEffectPoolAble)
        {
            uiEffectPoolAble.OnCreated(this);
        }

        Debug.Log($"[Pool] Get called. Pool size: {pool.Count}");

        return item;
    }

    public void Return(T item)
    {
        pool.Enqueue(item);
        item.gameObject.SetActive(false);
        DebugManager.Log($"Returning to pool: {typeof(T).Name}, Current count: {pool.Count}");

    }
}
