using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEndWarp : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            PlayerInfo.Instance.Health = 0;
        }
    }
}
