using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BadEndControoler : MonoBehaviour
{

    [SerializeField]
    SceneObject title;

    private void Awake()
    {
        if (PlayerInfo.InstanceNullable)
        {
            PlayerInfo.Instance.DestroySelf();
        }
        
        DataManager.ErasePlayerSaveData();
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(title);

    }

}
