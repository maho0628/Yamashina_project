using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Onoe
{
    public class Show_Score : MonoBehaviour
    {
        public GameObject Score_Object = null;  //Textオブジェクト
        public static int ShowScore;

     // Start is called before the first frame update
        void Start()
        {
            ShowScore = Score_GameA.ScoreGameA;
        }

        // Update is called once per frame
        void Update()
        {
            Text score_text = Score_Object.GetComponent<Text>();
            score_text.text = ShowScore.ToString();
        }

        public void Show(int Score)
        {
            ShowScore = Score;
        }
    }
}
