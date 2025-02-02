using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Onoe
{
    public class Game_open : MonoBehaviour
    {
        public GameObject CoutStart = null;
        public float Cout_start = 3.0f;
        bool count = false;

        private void Start()
        {
            Cout_start = 3.0f;
            count = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                count = true;
            }

                Text counttext = CoutStart.GetComponent<Text>();
                counttext.text = "" + (int)Cout_start;

            if (count)
            {
                //時間をカウントダウンする
                if (Cout_start > 0)
                {
                    Cout_start -= Time.deltaTime;
                }

                if (Cout_start <= 0)
                {
                    SceneManager.LoadScene("02_Scene_KarumeGame");
                }
            }
        }
    }
}
