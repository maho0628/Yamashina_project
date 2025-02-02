using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace Sakamoto
{
    public class Game_Maneger : MonoBehaviour
    {
        //[Header("����1�C�̓_��")]
 
        public static int scoreGameB;
        public static bool gameEndB;
        [SerializeField] GameObject Start_Gamen;
        [SerializeField] GameObject Game_Clirea_Gamen;
        [SerializeField] GameObject Score;
        [SerializeField] Text Score_Data;

        [SerializeField] Text Time_Text,ScoreText;
        [SerializeField] Text countDownText;

        //[SerializeField] AudioSource BGM;
        [SerializeField] AudioSource SE;
        [SerializeField] AudioClip SE_CountDown;
        //[SerializeField] AudioSource SE_start;
        [SerializeField] AudioClip SE_Start;
        //[SerializeField] AudioSource SE_end;
        [SerializeField] AudioClip SE_End;

        //�X�^�[�g��ʂ̕`��t���O
        bool startGamen_f;
        //�^�C�}�[��START����邽�߂̊֐�
        public bool Timer_Start_f;
        //�^�C�}�[���X�^�[�g����܂ł̎���
        float Defolt_Time_Tnp ;
        //�����̔������Ǘ�����t���O
        public bool Kingyo_Hassei_f;
        float Time_Tnp ;
         public bool Game_Main_f;

        float countTime = 0;
        [SerializeField] float countTime_Max;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("initialize");
            scoreGameB = 0;
            gameEndB = false;
            Start_Gamen.SetActive(true);
            Game_Clirea_Gamen.SetActive(false);
            Score.SetActive(false);
            startGamen_f = true;
            Timer_Start_f = false;
            Kingyo_Hassei_f = false;
            Defolt_Time_Tnp = 3;
            Time_Tnp = Defolt_Time_Tnp;       
            countTime = countTime_Max;
            Game_Main_f = true ;
            Time_Text.text = countTime_Max.ToString("F0");

            SE = GetComponent<AudioSource>();

           // SE_countdown.GetComponent < AudioSource > = SE_CountDown;
           // audioSource = GetComponent<AudioSource>();
        }

        //�摜���N���b�N�����瑀�������ʂ�������
        // Update is called once per frame
        void Update()
        {
            if (startGamen_f == true)
            {
                if (UnityEngine.Input.GetMouseButtonUp(0))
                {
                    Start_Gamen.SetActive(false);
                    startGamen_f = false;
                    StartCoroutine(CountDownForStart());
                }
            }
            //if (startGamen_f == false)
            //{
            //    if (Time_Tnp < 0)
            //    {
            //        Timer_Start_f = true; 
            //        Time_Tnp = 0;
            //        Kingyo_Hassei_f = true;
                    
            //    }
            //    Time_Tnp -= Time.deltaTime;
            //}

            if (Timer_Start_f)
            {
                // countTime�ɁA�Q�[�����J�n���Ă���̕b�����i�[
                //Time.deltaTime��1�t���[���ɂ�����������
                countTime -= Time.deltaTime;
                Time_Text.text = countTime.ToString("F0");

                ////����2���ɂ��ĕ\��
                //Debug.Log(countTime.ToString("F2"));

                if (!gameEndB)
                {
                    if (countTime <= 0)
                    {
                        countTime = 0;
                        SE.PlayOneShot(SE_End);
                        Game_Clirea_Gamen.SetActive(true);
                        Timer_Start_f = false;
                        Game_Main_f = false;
                        Score.SetActive(true);
                        Score_Data.text = scoreGameB.ToString("D4");
                    };

                }
            }

        }
       public void Pushu_Game_Clear()
        {
            gameEndB = true;
            Debug.Log("true");
        }
        public void AddScore(int score) 
        {
            scoreGameB += score;
            ScoreText.text = scoreGameB.ToString("D4");
        }

       IEnumerator CountDownForStart() 
        {      
            SE.PlayOneShot(SE_CountDown);
            countDownText.text = "3";
            yield return new WaitForSeconds(1f);

            SE.PlayOneShot(SE_CountDown);
            countDownText.text = "2";
            yield return new WaitForSeconds(1f);

            //audioSource.PlayOneShot(SE_CountDown);
            SE.PlayOneShot(SE_CountDown);
            countDownText.text = "1";
            yield return new WaitForSeconds(1f);

            //audioSource.PlayOneShot(SE_CountDown);
            countDownText.text = "";
            SE.PlayOneShot(SE_Start);
            yield return null;
            Timer_Start_f = true;
            Kingyo_Hassei_f = true;
        }
    }
}