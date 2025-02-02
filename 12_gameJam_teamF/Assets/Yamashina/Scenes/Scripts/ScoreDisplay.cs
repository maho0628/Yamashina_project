using Onoe;
using Sakamoto;
using System;
using System.Collections;
using System.Collections.Generic;
using Toshiki;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace Yamashina
{
    public class ScoreDisplay : MonoBehaviour
    {
       
        public Text Rnum;
        public Text txtScore;
        private int TotalScore;
       [SerializeField] int tmpScore;

        int[] Rank = new int[6];
        public Text[] txtRank = new Text[5];

        void Start()
        {//アプリのデータ領域は存在するか
            if (PlayerPrefs.HasKey("1R"))
            {
                for (int idex = 1; idex <= 5; idex++)
                {
                    Rank[idex] = PlayerPrefs.GetInt(idex + "R");//データ領域読み込み
                }
               
                Debug.Log("データ領域を読み込みました");
            }
            else
            {
                for (int idex = 1; idex <= 5; idex++)
                {
                    Rank[idex] = 0;//ゼロで初期化
                    PlayerPrefs.SetInt(idex + "R", 0);//ゼロを格納する
                }
                Debug.Log("データ領域を初期化しました");
            }

            DisplayScore();
            
        }


        void Update()
        {//開発用：データ領域の初期化
            if (Input.GetKeyDown(KeyCode.Escape))
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
            TotalScore = Score_GameA.ScoreGameA+ Game_Maneger.scoreGameB + Shageki_Manager.scoreGameC;//ハイスコアの合算
        }


        void RankCheck()//ランクチェック
        {
            int newRank = 0; //まず今回のスコアを0位と仮定する
            for (int idex = 5; idex > 0; idex--)//逆順5....1
            {
                if (Rank[idex] < TotalScore)
                {
                    newRank = idex;//新しいランクとして判定する
                }
            }
            if (newRank!= 0)//0位のままでなかったらランクイン確定
            {
                for (int idex = 5; idex > newRank; idex--)
                {
                    Rank[idex] = Rank[idex - 1];//繰り下げ処理

                }
                Rank[newRank] = TotalScore;//新ランクに登録
                for (int idex = 1; idex <= 5; idex++)
                {
                    PlayerPrefs.SetInt(idex + "R", Rank[idex]);//ゼロを格納する

                }
               

            }

        }
        void Ranking()
        {
            for (int idex = 0; idex < 5; idex++)
            {
                txtRank[idex].text = Rank[idex + 1].ToString(); //ランク文字
                txtRank[idex].gameObject.SetActive(true);
                txtScore.text = "" + TotalScore; //暗示的キャスト
                
            }

        }
        void DisplayScore()
        {
            Ranking();
            PlusScore();
            RankCheck();
            Ranking();
           
        }
    }
}