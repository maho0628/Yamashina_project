using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Onoe
{
    public class Game_A_Onoe : MonoBehaviour
    {
        public float countdown = 5.0f;
        public float CoutStart;
        
        public GameObject CountDown = null;
        int count;
        int totalTime;

        // Update is called once per frame
        void Update()
        {
            //時間をカウントダウンする
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }

            //時間表示
            //Debug.Log(countdown + "秒");
            Text counttext = CountDown.GetComponent<Text>();
            counttext.text = ""+(int)countdown;

            //countdounが0になった時
            if (countdown <= 0)
            {
                //timeText.text = "終了!";
                Debug.Log("終了");
                SceneManager.LoadScene("02_Scene_KarumeEnd");
            }

        }
    }
}