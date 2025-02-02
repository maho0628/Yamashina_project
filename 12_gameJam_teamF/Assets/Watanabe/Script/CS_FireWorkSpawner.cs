using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FireWorkSpawner : MonoBehaviour
{
    [SerializeField, Header("‰Ô‰Î")]
    private GameObject fireWorkPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FireSpawn1", 1);
    }

    void FireSpawn1()
    {
        Instantiate(fireWorkPrefab, Vector3.zero, Quaternion.identity);
        Invoke("FireSpawn2", 1);

    }

    void FireSpawn2()
    {
        Vector3 pos = new Vector3(-5.72f, 2.12f, 0);

        Instantiate(fireWorkPrefab, pos, Quaternion.identity);
        Invoke("FireSpawn3", 1);
    }

    void FireSpawn3()
    {
        Vector3 pos = new Vector3(3.88f, 3.42f, 0);

        Instantiate(fireWorkPrefab, pos, Quaternion.identity);

    }

}
