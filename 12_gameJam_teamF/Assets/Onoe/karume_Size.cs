using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Onoe
{

    public class karume_Size : MonoBehaviour
    {
        [SerializeField] private GameObject SS; //小さい、拡大無し
        [SerializeField] private GameObject S;  //小さい
        [SerializeField] private GameObject SM; //中ぐらい、拡大無し
        [SerializeField] private GameObject M;  //中ぐらい
        [SerializeField] private GameObject ML; //大きい、拡大無し
        [SerializeField] private GameObject L;  //大きい
        private Image imageSS;
        private Image imageS;
        private Image imageSM;
        private Image imageM;
        private Image imageML;
        private Image imageL;
        public int Score;

        public void Getscore(int score)
        {
            Score = score;
        }

        private void Start()
        {
            Score = Score_GameA.ScoreGameA;
            imageSS = SS.GetComponent<Image>();
            imageSS.enabled = false;
            imageS = S.GetComponent<Image>();
            imageS.enabled = false;
            imageSM = SM.GetComponent<Image>();
            imageSM.enabled = false;
            imageM = M.GetComponent<Image>();
            imageM.enabled = false;
            imageML = ML.GetComponent<Image>();
            imageML.enabled = false;
            imageL = L.GetComponent<Image>();
            imageL.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Score <= 1500)
            {
                imageSS.enabled = true;
            }
            else if (Score >= 1500 && Score <= 2000)
            {
                imageSS.enabled = false;
                imageS.enabled = true;
            }
            else if (Score >= 2000 && Score <= 2500)
            {
                imageS.enabled = false;
                imageSM.enabled = true;
            }
            else if (Score >= 2500 && Score <= 3000)
            {
                imageSM.enabled = false;
                imageM.enabled = true;
            }
            else if (Score >= 3000 && Score <= 3500)
            {
                imageM.enabled = false;
                imageML.enabled = true;
            }
            else if (Score > 3500)
            {
                imageML.enabled = false;
                imageL.enabled = true;
            }
        }
    }
}
