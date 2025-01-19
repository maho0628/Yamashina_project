using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperSave : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { DoSuperSave(); });
    }
    void DoSuperSave()
    {
        PlayerInfo.Instance.SavePalyerData();
        Debug.Log("SuperSave!!!");
    }
}
