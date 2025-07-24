using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Observe1Day : MonoBehaviour
{
    [SerializeField]
    SceneObject op;
    private void Start()
    {
        Debug.Log(PlayerInfo.Instance.Day.day.ToString());
        if(PlayerInfo.Instance.Day.day == 1)
        {
            PlayerInfo.Instance.DestroySelf();
            SceneManager.LoadScene(op);

        }
    }
}
