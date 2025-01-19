using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class NightEventControll : MonoBehaviour
{
    [SerializeField] Fading fade;
    [SerializeField] GameObject prefab;

    public void GoNightEvent()
    {
        PlayerInfo.Instance.ActionValue--;
        Instantiate(prefab);
 
    }

}