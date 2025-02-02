using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Onoe
{
    public class Score_GameA : MonoBehaviour
    {
        public int Score_Div = 3;
        public GameObject Score_Object = null;  //Text�I�u�W�F�N�g
        public static int ScoreGameA; //�X�R�A

        private void Start()
        {
            ScoreGameA = 0;
        }
        public void IncreaseScore(int score)
        {
            ScoreGameA += score / Score_Div;
            Debug.Log("Score: "+ScoreGameA);
        }

        private void Update()
        {
            Text score_text = Score_Object.GetComponent<Text>();
            score_text.text = ScoreGameA.ToString();
        }
    }
}
