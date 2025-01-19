using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelDestroy : MonoBehaviour
{
    [SerializeField] GameObject parent;
    public void DestroyParent()
    {
       Destroy(parent);
    }

}
