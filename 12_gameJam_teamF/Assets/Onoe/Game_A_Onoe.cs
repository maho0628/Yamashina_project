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
            //���Ԃ��J�E���g�_�E������
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }

            //���ԕ\��
            //Debug.Log(countdown + "�b");
            Text counttext = CountDown.GetComponent<Text>();
            counttext.text = ""+(int)countdown;

            //countdoun��0�ɂȂ�����
            if (countdown <= 0)
            {
                //timeText.text = "�I��!";
                Debug.Log("�I��");
                SceneManager.LoadScene("02_Scene_KarumeEnd");
            }

        }
    }
}