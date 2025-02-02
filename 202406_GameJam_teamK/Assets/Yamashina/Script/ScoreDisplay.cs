using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace Yamashina
{
    public class ScoreDisplay : MonoBehaviour
    {

        //public Text[] Rnum;
        public Text txtScore;
        private float TotalScore;
        [SerializeField] int tmpScore;

        [SerializeField] float[] Rank = new float[6];
        public Text[] txtRank = new Text[5];

        void Start()
        {
            //アプリのデータ領域は存在するか
            if (PlayerPrefs.HasKey("1R"))
            {
                for (int idex = 1; idex <= 5; idex++)
                {
                    Rank[idex] = PlayerPrefs.GetFloat(idex + "R");//データ領域読み込み
                    Debug.Log(Rank[idex]);
                }

                Debug.Log("データ領域を読み込みました");
            }
            else
            {
                for (int idex = 1; idex <= 5; idex++)
                {
                    Rank[idex] = 0.0f;//ゼロで初期化
                    PlayerPrefs.SetFloat(idex + "R", 0.0f);//ゼロを格納する
                }
                Debug.Log("データ領域を初期化しました");
            }

            DisplayScore();

        }


        void Update()
        {//開発用：データ領域の初期化
            if (Input.GetKeyDown(KeyCode.A))
            {
                PlayerPrefs.DeleteAll();
                Debug.Log("データ領域を削除しました");

            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                TotalScore = tmpScore;
                RankCheck();
                Ranking();
            }
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    for(int idex = 0;idex <= 4; idex++)
            //    {
            //        Rank[idex] = idex * 10;
            //        PlayerPrefs.SetInt(idex + "位", Rank[idex]);
            //    Debug.Log("データ領域をkousinしました");
            //    }

            //}

            //DisplayScore();

        }

        public void PlusScore()
        {
            TotalScore = Gamemanager.score;
                }


        void RankCheck()//ランクチェック
        {
            int newRank = 0; //まず今回のスコアを0位と仮定する
            for (int index = 5; index > 0; index--)//逆順5....1
            {
                // スコアが低い人が新しいランクとして判断する
                if (Rank[index] > TotalScore)
                {
                    newRank = index;
                }
            }

            if (newRank != 0)//0位のままでなかったらランクイン確定
            {
                for (int index = 5; index > newRank; index--)
                {

                    Debug.Log(index);
                    Debug.Log(Rank[index]);
                    Rank[index] = Rank[index -1];//繰り下げ処理
                    //Rnum[idex-1].text = Rank[idex].ToString();    
                }
                Rank[newRank] = TotalScore;//新ランクに登録
                for (int index = 1; index <= 5; index++)
                {
                    PlayerPrefs.SetFloat(index + "R", Rank[index]);//ゼロを格納する
                    Debug.Log(Rank[index]);

                }


            }

        }
       

        void Ranking()
        {
            //for (int index = 0; index < 5; index++)
            //{
            //    Rank[index + 1] = TotalScore;
            //    txtRank[index].text = Rank[index + 1].ToString("F1"); // 小数点以下3桁まで表示
            //    txtRank[index].gameObject.SetActive(true);
            //    txtScore.text = "" + TotalScore; //暗示的キャスト
            //}

            // ランキングUIの表示
            for(int index = 0; index < Rank.Length -1; ++index)
            {
                txtRank[index].text = Rank[index].ToString("F1");
                txtRank[index].gameObject.SetActive(true);
                txtScore.text = "" + TotalScore; //暗示的キャスト
            }

        }
        void DisplayScore()
        {
           PlusScore();
          //  RankCheck();
            Ranking();

        }
    }
}