using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : SingletonMonoBehaviour<PrefabManager>
{
    [System.Serializable]
    public class PrefabListData
    {
        public PrefabKey key = PrefabKey.None;
        public GameObject prefab = null;
    }
    public enum PrefabKey{
        None,PlayerPanel,WaterPanel,StoragePanel,RestPanel,LocationSelectPanel,FirePanel,CraftPanel,CookingPanel,
    }

    [SerializeField]
    List<PrefabListData> datas;

    Dictionary<PrefabKey, GameObject> prefabs = new Dictionary<PrefabKey, GameObject>();
    Dictionary<PrefabKey, GameObject> prefab_Instances = new Dictionary<PrefabKey, GameObject>();


    protected override void Awake()
    {
        base.Awake();
        foreach (var item in datas)
        {
            if(item.key == PrefabKey.None) Debug.LogError("PrefabKey is none");
            if(item.prefab == null) Debug.LogError("Prefab is null");

            prefabs.Add(item.key, item.prefab);
            prefab_Instances.Add(item.key, null);
        }

    }

    public GameObject GetPrefabInstance(PrefabKey key)
    {
        if (GetPrefabInstaceNullable(key) != null)
        {
            return GetPrefabInstaceNullable(key);
        }
        else
        {
            return InstantiatePrefabAndRegistInstance(key);
        }
    }

    public  GameObject GetPrefabInstaceNullable(PrefabKey key)
    {
        return prefab_Instances[key];
    }

    private GameObject InstantiatePrefabAndRegistInstance(PrefabKey key)
    {
        GameObject prefab = GetPrefabData(key);
        if (prefab)
        {
            var go =  Instantiate<GameObject>(prefab);
            prefab_Instances[key] = go;
            return go;
        }
        else
        {
            Debug.LogError("prefab is null");
            return null;
        }
    }

    public GameObject GetPrefabData(PrefabKey key)
    {
        GameObject go = null;
        if (prefabs.TryGetValue(key, out go))
        {
            return go;
        }
        else
        {
            Debug.LogError($"PrefabManager in this scene has no prefab ref of PrefabKey :  {key}");
            return null;
        }

    }
}
