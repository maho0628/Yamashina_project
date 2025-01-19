using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prehubdelete : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    public bool deleteSleepPanel_after;
    //public KyotenToOtherspace KyotenToOtherspace;

    private void Update()
    {
        deleteSleepPanel_after = false;
        
        
    }
   
    public void deletePrehub()
    {
        
        deleteSleepPanel_after = true;
        
            Destroy(prefab);


       
    }

}

