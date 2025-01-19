using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOptionButton : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return 2;
        print(GameData.CanReturnTitle);
           gameObject.SetActive(GameData.CanReturnTitle);
        
    }
}
