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
        {//�A�v���̃f�[�^�̈�͑��݂��邩
            if (PlayerPrefs.HasKey("1R"))
            {
                for (int idex = 1; idex <= 5; idex++)
                {
                    Rank[idex] = PlayerPrefs.GetInt(idex + "R");//�f�[�^�̈�ǂݍ���
                }
               
                Debug.Log("�f�[�^�̈��ǂݍ��݂܂���");
            }
            else
            {
                for (int idex = 1; idex <= 5; idex++)
                {
                    Rank[idex] = 0;//�[���ŏ�����
                    PlayerPrefs.SetInt(idex + "R", 0);//�[�����i�[����
                }
                Debug.Log("�f�[�^�̈�����������܂���");
            }

            DisplayScore();
            
        }


        void Update()
        {//�J���p�F�f�[�^�̈�̏�����
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerPrefs.DeleteAll();
                Debug.Log("�f�[�^�̈���폜���܂���");

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
            //        PlayerPrefs.SetInt(idex + "��", Rank[idex]);
            //    Debug.Log("�f�[�^�̈��kousin���܂���");
            //    }

            //}

            //DisplayScore();

        }

        public void PlusScore()
        {
            TotalScore = Score_GameA.ScoreGameA+ Game_Maneger.scoreGameB + Shageki_Manager.scoreGameC;//�n�C�X�R�A�̍��Z
        }


        void RankCheck()//�����N�`�F�b�N
        {
            int newRank = 0; //�܂�����̃X�R�A��0�ʂƉ��肷��
            for (int idex = 5; idex > 0; idex--)//�t��5....1
            {
                if (Rank[idex] < TotalScore)
                {
                    newRank = idex;//�V���������N�Ƃ��Ĕ��肷��
                }
            }
            if (newRank!= 0)//0�ʂ̂܂܂łȂ������烉���N�C���m��
            {
                for (int idex = 5; idex > newRank; idex--)
                {
                    Rank[idex] = Rank[idex - 1];//�J�艺������

                }
                Rank[newRank] = TotalScore;//�V�����N�ɓo�^
                for (int idex = 1; idex <= 5; idex++)
                {
                    PlayerPrefs.SetInt(idex + "R", Rank[idex]);//�[�����i�[����

                }
               

            }

        }
        void Ranking()
        {
            for (int idex = 0; idex < 5; idex++)
            {
                txtRank[idex].text = Rank[idex + 1].ToString(); //�����N����
                txtRank[idex].gameObject.SetActive(true);
                txtScore.text = "" + TotalScore; //�Î��I�L���X�g
                
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