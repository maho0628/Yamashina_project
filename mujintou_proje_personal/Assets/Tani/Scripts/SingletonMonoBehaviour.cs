using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シングルトンなMonoBehaviourの基底クラス
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = (T)FindObjectOfType(typeof(T));

            if (instance == null)
            {
                Debug.LogError(typeof(T) + "is nothing");
            }

            return instance;
        }
    }

    public static T InstanceNullable
    {
        get
        {
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
         //   Debug.LogError(typeof(T) + " is multiple created", this);
            Destroy(this.gameObject);
            return;
        }

        instance = this as T;
    }

    protected virtual void OnDestroy()
    {
        //Debug.Log("Singleton Destroyed");
        if (instance == this)
        {
            instance = null;
        }
    }
}
