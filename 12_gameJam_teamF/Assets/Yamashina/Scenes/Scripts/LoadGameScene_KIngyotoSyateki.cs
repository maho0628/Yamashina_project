using Sakamoto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Yamashina
{
    public class LoadGameScene_KingyotoSyateki : LoadScene
    {
        public void OnClick()
        {
            ChangeScene();
        }
        public void ChangeScene()
        {
           if (Game_Maneger.gameEndB)
            {
                LoadingScene();


            }
        }
    }
}
