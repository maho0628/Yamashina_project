using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inv_lock : MonoBehaviour
{
    [SerializeField]
    GameObject inv_lock;
   public void SetInventryLock(bool b)
    {
        inv_lock.SetActive(b);
    }
}
