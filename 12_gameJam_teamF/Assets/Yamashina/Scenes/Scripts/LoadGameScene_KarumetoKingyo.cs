using Onoe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yamashina;
namespace Yamashina { 
public class LoadGameScene_KarumetoKingyo : LoadScene
    {
        // Start is called before the first frame update

       public void OnClick()
        {
            ChangeScene();
        }
        public void ChangeScene()
        {
          if (Button.gameEndA)
            {
                LoadingScene();


            }
        }
    }
}