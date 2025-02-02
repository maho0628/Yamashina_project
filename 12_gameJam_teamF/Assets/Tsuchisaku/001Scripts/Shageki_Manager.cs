using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Toshiki
{
    // START GAME OVER
    // START ボタン表示　解説表示　
    // GAME ウェーブ開始 一定時間経過->Over
    // OVER　ボタン表示　STARTに戻るか次に遷移するボタンを表示する

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

        [Header("(じかん)")]
        public float waveTime;
        [SerializeField] float waveTimer;
        [Header("じゅうの性能")]
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
                    waveController.WaveEnd = true; //べつにいらん
                    SwitchState(STATE.OVER);
                    GameEnd();
                }


                ////Waveの終わりフラグが立てばOverへ。
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
            //Wave開始
            ui.SetscoreText(scoreGameC);
            gameState = STATE.GAME;
            shateki_game.aimPoint.gameObject.SetActive(true);
            waveController.WaveStart();
        }

        void GameEnd()
        {
            EndCanvas.gameObject.SetActive(true);
            // カーソル表示
            Cursor.visible = true;
            shateki_game.aimPoint.gameObject.SetActive(false);
            //キャラのアニメーションを実行させる
            shateki_game.anim.SetAnim(Anim.End);
        }

        void STARTEnter()
        {
            //StartCanvas表示
            startCanvas.SetActive(true);
            EndCanvas.SetActive(false);
            gameEndC = false;

        }

        public void Button_Start()
        {
            //スタートボタン
            //Gameへ遷移
            startCanvas.SetActive(false);
            EndCanvas.SetActive(false);
            ui.gameObject.SetActive(true);
            shateki_game.anim.SetAnim(Anim.Start);
            // カーソル表示
            Cursor.visible = false;

            scoreGameC = 0;
            ui.SetscoreText(scoreGameC);
            ui.TimeTextUpdate(waveTime);
            StartCoroutine(CountDownForStart()); //カウントダウン開始
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
            GameStart();//ゲームスタート処理
            yield return null;
        }



        //関数でStateを変更する
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
