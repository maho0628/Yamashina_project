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
            //�A�v���̃f�[�^�̈�͑��݂��邩
            if (PlayerPrefs.HasKey("1R"))
            {
                for (int idex = 1; idex <= 5; idex++)
                {
                    Rank[idex] = PlayerPrefs.GetFloat(idex + "R");//�f�[�^�̈�ǂݍ���
                    Debug.Log(Rank[idex]);
                }

                Debug.Log("�f�[�^�̈��ǂݍ��݂܂���");
            }
            else
            {
                for (int idex = 1; idex <= 5; idex++)
                {
                    Rank[idex] = 0.0f;//�[���ŏ�����
                    PlayerPrefs.SetFloat(idex + "R", 0.0f);//�[�����i�[����
                }
                Debug.Log("�f�[�^�̈�����������܂���");
            }

            DisplayScore();

        }


        void Update()
        {//�J���p�F�f�[�^�̈�̏�����
            if (Input.GetKeyDown(KeyCode.A))
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
            TotalScore = Gamemanager.score;
                }


        void RankCheck()//�����N�`�F�b�N
        {
            int newRank = 0; //�܂�����̃X�R�A��0�ʂƉ��肷��
            for (int index = 5; index > 0; index--)//�t��5....1
            {
                // �X�R�A���Ⴂ�l���V���������N�Ƃ��Ĕ��f����
                if (Rank[index] > TotalScore)
                {
                    newRank = index;
                }
            }

            if (newRank != 0)//0�ʂ̂܂܂łȂ������烉���N�C���m��
            {
                for (int index = 5; index > newRank; index--)
                {

                    Debug.Log(index);
                    Debug.Log(Rank[index]);
                    Rank[index] = Rank[index -1];//�J�艺������
                    //Rnum[idex-1].text = Rank[idex].ToString();    
                }
                Rank[newRank] = TotalScore;//�V�����N�ɓo�^
                for (int index = 1; index <= 5; index++)
                {
                    PlayerPrefs.SetFloat(index + "R", Rank[index]);//�[�����i�[����
                    Debug.Log(Rank[index]);

                }


            }

        }
       

        void Ranking()
        {
            //for (int index = 0; index < 5; index++)
            //{
            //    Rank[index + 1] = TotalScore;
            //    txtRank[index].text = Rank[index + 1].ToString("F1"); // �����_�ȉ�3���܂ŕ\��
            //    txtRank[index].gameObject.SetActive(true);
            //    txtScore.text = "" + TotalScore; //�Î��I�L���X�g
            //}

            // �����L���OUI�̕\��
            for(int index = 0; index < Rank.Length -1; ++index)
            {
                txtRank[index].text = Rank[index].ToString("F1");
                txtRank[index].gameObject.SetActive(true);
                txtScore.text = "" + TotalScore; //�Î��I�L���X�g
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