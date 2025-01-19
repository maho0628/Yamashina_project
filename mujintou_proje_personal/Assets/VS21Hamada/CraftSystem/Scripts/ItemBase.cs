using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public struct ItemBaseStruct
    {
        public enum Category
        {
            Sozai,
            Weapon,
            armor
        }
        public Category enCategory;
        public string sName;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
