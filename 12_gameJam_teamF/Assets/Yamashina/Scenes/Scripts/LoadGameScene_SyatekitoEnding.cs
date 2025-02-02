using System.Collections;
using System.Collections.Generic;
using Toshiki;
using UnityEngine;
using Yamashina;


public class LoadGameScene_SyatekitoEnding : LoadScene

{
   public void OnClick()
    {
        ChangeScene();
    }
    public void ChangeScene()
    {
       if (Shageki_Manager.gameEndC)
        {
            LoadingScene();


        }
    }
}

