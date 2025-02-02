using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Toshiki
{
    // START GAME OVER
    // START �{�^���\���@����\���@
    // GAME �E�F�[�u�J�n ��莞�Ԍo��->Over
    // OVER�@�{�^���\���@START�ɖ߂邩���ɑJ�ڂ���{�^����\������

    public class Shageki_Manager : MonoBehaviour
    {
        public enum STATE
        {
            START,
            GAME,
            OVER,
        }

        public static bool gameEndC = false;
        public static int scoreGameC;

        public STATE gameState;

        public static Shageki_Manager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddScore(int _point)
        {
            if (gameState == STATE.GAME)
            {
                scoreGameC += _point;
                ui.SetscoreText(scoreGameC);
            }
        }



        public Shateki_game shateki_game;

        [Header("(������)")]
        public float waveTime;
        [SerializeField] float waveTimer;
        [Header("���イ�̐��\")]
        public float ammo_graity;
        public float ammo_lifeTime;
        public int ammo_maxAmmo;

        public GameObject startCanvas;
        public GameObject EndCanvas;
        public Shageki_UI ui;

        [Header("Watanabes Spawner")]
        public CS_WaveController waveController;

        [Header("Hight Score Times")]
        public float highScoreTimes = 2f;
        public int point_Small = 50;
        public int point_Fast = 80;



        // Start is called before the first frame update
        void Start()
        {
            EndCanvas.gameObject.SetActive(false);

            STARTEnter();
            //GameEnter();
            waveTimer = waveTime;
            SoundManager.instance.Play("PlayBGM");
        }

        // Update is called once per frame
        void Update()
        {
            if (gameState == STATE.START)
            {

            }
            else if (gameState == STATE.GAME)
            {
                shateki_game.LocalUpdate();
                waveTimer -= Time.deltaTime;
                ui.TimeTextUpdate(waveTimer);
                if (waveTimer < 0)
                {
                    waveTimer = 0;
                    waveController.WaveEnd = true; //�ׂɂ����
                    SwitchState(STATE.OVER);
                    GameEnd();
                }


                ////Wave�̏I���t���O�����Ă�Over�ցB
                //if (waveController.WaveEnd)
                //{
                //    SwitchState(STATE.OVER);
                //    GameEnd();
                //}

            }
            else if (gameState == STATE.OVER)
            {

            }
        }


        void GameStart()
        {
            //Wave�J�n
            ui.SetscoreText(scoreGameC);
            gameState = STATE.GAME;
            shateki_game.aimPoint.gameObject.SetActive(true);
            waveController.WaveStart();
        }

        void GameEnd()
        {
            EndCanvas.gameObject.SetActive(true);
            // �J�[�\���\��
            Cursor.visible = true;
            shateki_game.aimPoint.gameObject.SetActive(false);
            //�L�����̃A�j���[�V���������s������
            shateki_game.anim.SetAnim(Anim.End);
        }

        void STARTEnter()
        {
            //StartCanvas�\��
            startCanvas.SetActive(true);
            EndCanvas.SetActive(false);
            gameEndC = false;

        }

        public void Button_Start()
        {
            //�X�^�[�g�{�^��
            //Game�֑J��
            startCanvas.SetActive(false);
            EndCanvas.SetActive(false);
            ui.gameObject.SetActive(true);
            shateki_game.anim.SetAnim(Anim.Start);
            // �J�[�\���\��
            Cursor.visible = false;

            scoreGameC = 0;
            ui.SetscoreText(scoreGameC);
            ui.TimeTextUpdate(waveTime);
            StartCoroutine(CountDownForStart()); //�J�E���g�_�E���J�n
        }

        IEnumerator CountDownForStart()
        {
            ui.countDownText.text = "3";
            yield return new WaitForSeconds(1f);
            ui.countDownText.text = "2";
            yield return new WaitForSeconds(1f);
            ui.countDownText.text = "1";
            yield return new WaitForSeconds(1f);
            ui.countDownText.text = "";
            GameStart();//�Q�[���X�^�[�g����
            yield return null;
        }



        //�֐���State��ύX����
        public void SwitchState(STATE _state)
        {
            if (_state == STATE.START)
            {
                gameState = STATE.START;
            }
            else if (_state == STATE.GAME)
            {
                gameState = STATE.GAME;
            }
            else if (_state == STATE.OVER)
            {
                gameState = STATE.OVER;
            }
        }

        public void OnNextStageButton()
        {
            gameEndC = true;
        }


    }
}
