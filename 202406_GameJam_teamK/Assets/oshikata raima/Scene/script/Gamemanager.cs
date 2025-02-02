using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

enum Stete
{
    win,
    lose,
    end,
    start,
    play
}
public class Gamemanager : MonoBehaviour
{
    //void test()
    //{
    //    Vector3 currentPosition = transform.position;
    //    Vector3 enemyPosition = EnemyObject[enemyCount].transform.position;
    //    switch (stete)
    //    {
    //        case Stete.start:
    //            if (lapTime < fightStartTime && Input.GetKey(KeyCode.Space) && isStart && isSpace == false)
    //            {
    //                Debug.Log("フライング！！");

    //                isSpace = true;
    //            }

    //            // 早撃ち開始、時間リセット
    //            if (lapTime < fightStartTime)
    //            {
    //                return;
    //            }
    //            break;
    //        case Stete.lose:
    //            {
    //                //移動

    //                if (Enemy[enemyCount] < playertime)
    //                {
    //                    transform.position = new Vector3(teleportDistancelose, currentPosition.y, currentPosition.z);
    //                    EnemyObject[enemyCount].transform.position = new Vector3(teleportDistancewin, enemyPosition.y, enemyPosition.z);
    //                    Text_Katimake.SetActive(true);

    //                    Debug.Log("負け");
    //                    Text_Katimake.GetComponent<Image>().sprite = Make;  
    //                    //Syouri.text = "負け";

    //                    fading.fading_time = 0.5f;
    //                    fading.Fade(Fading.type.FadeOut);
    //                    fading.OnFadeEnd.AddListener(() =>
    //                    {
    //                        SceneManager.LoadScene(scene);
    //                    });
    //                    //fading.Fade(Fading.type.FadeIn);
    //                }
    //                Reset_();
    //            }
    //            break;
    //        case Stete.end:
    //            if (EnemyObject[enemyCount] == EnemyObject[4])
    //            {
    //                Debug.Log("クリア");

    //                fading.fading_time = 0.5f;
    //                fading.Fade(Fading.type.FadeOut);
    //                fading.OnFadeEnd.AddListener(() =>
    //                {
    //                    SceneManager.LoadScene(scene);
    //                });
    //                //fading.Fade(Fading.type.FadeIn);
    //            }
    //            EnemyObject[enemyCount].SetActive(true);

    //            if (!EnemyObject[enemyCount].activeSelf)
    //            {
    //                SceneManager.LoadScene(scene);
    //            }
    //            break;
    //        case Stete.win:

    //            if (Enemy[enemyCount] > playertime)
    //            {
    //                transform.position = new Vector3(teleportDistancewin, currentPosition.y, currentPosition.z);
    //                EnemyObject[enemyCount].transform.position = new Vector3(teleportDistancelose, enemyPosition.y, enemyPosition.z);
    //                Player.SetActive(false);
    //                WinPlayer.SetActive(true);
    //                fading.Fade(Fading.type.FadeOut);
    //                fading.OnFadeEnd.AddListener(() =>
    //                {
    //                    //  fading.fading_time = 3.0f;
    //                    Text_Katimake.SetActive(true);
    //                    Text_Katimake.GetComponent<Image>().sprite =Kati;

    //                    Debug.Log("勝ち");
    //                    Mark.SetActive(false);




    //                    Debug.Log("Text_SetActive");
    //                    Invoke("Text_SetActive", 3.0f);

    //                    fading.Fade(Fading.type.FadeIn);
    //                    isStart = true;
    //                    EnemyObject[enemyCount].SetActive(false);
    //                    // エネミーのカウントを進める
    //                    enemyCount++;
    //                    Debug.Log(enemyCount);

    //                    EnemyObject[enemyCount].SetActive(true);
    //                    Debug.Log(enemyCount);
    //                    transform.position = new Vector3(-teleportDistancewin, currentPosition.y, currentPosition.z);
    //                    WinPlayer.SetActive(false);
    //                    Player.SetActive(true);
    //                });
    //                //マップ移動
    //                if (EnemyObject[enemyCount] == EnemyObject[1])
    //                {
    //                    fading.fading_time = 0.5f;
    //                    fading.Fade(Fading.type.FadeOut);
    //                    fading.OnFadeEnd.AddListener(() =>
    //                    {
    //                        MainVisual.GetComponent<Image>().sprite = Back1;
    //                        EnemyObject[enemyCount].SetActive(true);
    //                    });
    //                    //fading.Fade(Fading.type.FadeIn);
    //                }
    //                //マップ移動二回目
    //                if (EnemyObject[enemyCount] == EnemyObject[3])
    //                {
    //                    fading.fading_time = 0.5f;
    //                    fading.Fade(Fading.type.FadeOut);
    //                    fading.OnFadeEnd.AddListener(() =>
    //                    {
    //                        MainVisual.GetComponent<Image>().sprite = Back2;
    //                        EnemyObject[enemyCount].SetActive(true);
    //                        multi.ChooseSongs_BGM(1);
    //                    });
    //                }
    //                Reset_();
    //            }
    //                break;
    //        case Stete.play:
    //            // スコアを加算
    //            score += playertime;
    //            Score.text = score.ToString();
    //            // Invoke("TextScoreRestart", 0.5f);
    //            Reset_();
    //            break;
    //    }
    //}
    Stete stete;
    public GameObject[] EnemyObject;
    public GameObject[] winEnemyObject;
    public GameObject[] loseEnemyObject;
    public GameObject WinPlayer;
    public GameObject Player;
    public GameObject losePlayer;
    [SerializeField] GameObject MainVisual;
    [SerializeField] Sprite Back1;
    [SerializeField] Sprite Back2;
    [SerializeField] SceneObject scene;
    [SerializeField] SceneObject scene1;
    [SerializeField] multiAudio multi;
    //[SerializeField] Text Syouri;
    [SerializeField] GameObject Text_Katimake;
    [SerializeField] GameObject Mark;
    [SerializeField] Text Score;

    [SerializeField] Sprite Kati;
    [SerializeField] Sprite Make;
    [SerializeField] Sprite Hikiwake;


    //[SerializeField] Sprite Back3;
    //勝った時移動距離
    public float teleportDistancewin = 7f;
    //負けた時移動距離
    public float teleportDistancelose = 4f;

    // 時間になったら早撃ち
    public float lapTime;
    // 指示する時間
    public float fightStartTime = 5;
    //　タイムをリセット
    public bool resettime;
    //敵を何体倒したか
    public int enemyCount = 0;

    //　敵の攻撃
    public float[] Enemy;
    //　playerの攻撃
    public float playertime;

    public bool isSpace = false;

    public static float score = 0.0f;
    public Fading fading;
    [SerializeField] GameObject ScoreOb;
    //  スタート
    public bool isStart = false;

    void Start()
    {
        multi = GameObject.FindAnyObjectByType<multiAudio>().GetComponent<multiAudio>();
        multi.ChooseSongs_BGM(0);
        fading = GameObject.FindAnyObjectByType<Fading>().GetComponent<Fading>();
        lapTime = 0;
        resettime = false;
        EnemyObject[enemyCount].SetActive(true);
        //EnemyObject[enemyCount].SetActive(false);
        fightStartTime = Random.Range(4.0f, 7.0f);
        score = 0.0f;
        WinPlayer.SetActive(false);
        Player.SetActive(true);
        fading.Fade(Fading.type.FadeIn);
    }

    void Update()
    {
        if (lapTime < fightStartTime && Input.GetKey(KeyCode.Space) && isStart && isSpace == false)
        {
            Debug.Log("フライング！！");
            Player.SetActive(false);
            WinPlayer.SetActive(true);
            isSpace = true;
            multi.ChooseSongs_SE(1);
        }
        if (isStart == true)
        {
            lapTime += Time.deltaTime;
        }



        //テスト
        //Debug.Log("経過時間: " + LapTime.ToString("F1") + " 秒");


        // 早撃ち開始、時間リセット
        if (lapTime < fightStartTime)
        {
            return;
        }
        if (lapTime >= fightStartTime)
        {
            Debug.Log("!!");
            Mark.GetComponent<AudioSource>().clip = multi.audioClipSE[0];
            Mark.SetActive(true);

        }

        // プレイヤーの時間を増加
        playertime += Time.deltaTime;
        //Debug.Log("!!");
        //multi.ChooseSongs_SE(0);

        // 勝ち負けの判定
        if (Input.GetKey(KeyCode.Space) && isSpace == false)
        {
            //剣を振った音
            multi.ChooseSongs_SE(2);
            //移動
            Vector3 currentPosition = transform.position;
            Vector3 enemyPosition = EnemyObject[enemyCount].transform.position;
            Debug.Log("経過時間: " + playertime.ToString("F1") + " 秒");
            //勝った時
            if (Enemy[enemyCount] > playertime)
            {
                transform.position = new Vector3(teleportDistancewin, currentPosition.y, currentPosition.z);
                Debug.Log(transform.position);
                //EnemyObject[enemyCount].transform.position = new Vector3(teleportDistancelose, enemyPosition.y, enemyPosition.z);
                Player.SetActive(false);
                WinPlayer.SetActive(true);
                EnemyObject[enemyCount].SetActive(false);
                loseEnemyObject[enemyCount].SetActive(true);
                fading.Fade(Fading.type.FadeOut);
                fading.OnFadeEnd.RemoveAllListeners(); // 追加する前にクリア

                fading.OnFadeEnd.AddListener(() =>
                {
                    //  fading.fading_time = 3.0f;
                    Text_Katimake.SetActive(true);

                    Debug.Log("勝ち");
                    //Syouri.text = "勝ち";
                    Mark.SetActive(false);




                    Debug.Log("Text_SetActive");
                    Invoke("Text_SetActive", 2.0f);


                    fading.Fade(Fading.type.FadeIn);
                    isStart = true;



                    loseEnemyObject[enemyCount].SetActive(false);
                    // エネミーのカウントを進める
                    enemyCount++;
                    Debug.Log(enemyCount);
                    EnemyObject[enemyCount].SetActive(true);
                    loseEnemyObject[enemyCount].SetActive(false);
                    Debug.Log(enemyCount);
                    transform.position = currentPosition;
                    Debug.Log(transform.position);
                    WinPlayer.SetActive(false);
                    Player.SetActive(true);


                });
                //マップ移動
                if (EnemyObject[enemyCount] == EnemyObject[1])
                {
                    fading.fading_time = 3.3f;
                    fading.Fade(Fading.type.FadeOut);

                    fading.OnFadeEnd.AddListener(() =>
                    {
                        MainVisual.GetComponent<Image>().sprite = Back1;
                        //EnemyObject[enemyCount].SetActive(true);
                    });
                    //fading.Fade(Fading.type.FadeIn);
                }
                //マップ移動二回目
                if (EnemyObject[enemyCount] == EnemyObject[3])
                {
                    fading.fading_time = 3.3f;
                    fading.Fade(Fading.type.FadeOut);

                    fading.OnFadeEnd.AddListener(() =>
                    {
                        MainVisual.GetComponent<Image>().sprite = Back2;
                        //EnemyObject[enemyCount].SetActive(true);
                        multi.ChooseSongs_BGM(1);
                    });
                }
                //クリア
                if (EnemyObject[enemyCount] == EnemyObject[4])
                {
                    Debug.Log("クリア");
                    fading.fading_time = 3.3f;
                    fading.Fade(Fading.type.FadeOut);
                    fading.OnFadeEnd.RemoveAllListeners(); // 追加する前にクリア

                    fading.OnFadeEnd.AddListener(() =>
                    {
                        SceneManager.LoadScene(scene);
                    });
                    //fading.Fade(Fading.type.FadeIn);
                }

                //if (!EnemyObject[enemyCount].activeSelf)
                //{
                //    SceneManager.LoadScene(scene);
                //}
                // スコアを加算
                score += playertime;
                Score.text = "スコアは" + score.ToString("F1");
                // Invoke("TextScoreRestart", 0.5f);

            }
            else if (Enemy[enemyCount] == playertime)
            {
                Text_Katimake.SetActive(true);
                Debug.Log("引き分け");
                Text_Katimake.GetComponent<Image>().sprite = Hikiwake;

                //Syouri.text = "引き分け";


            }
            Reset_();
        }
        if (lapTime - fightStartTime > Enemy[enemyCount])
        {
            Text_Katimake.SetActive(true);
            Player.SetActive(false);
            WinPlayer.SetActive(false);
            losePlayer.SetActive(true);
            EnemyObject[enemyCount].SetActive(false);
            winEnemyObject[enemyCount].SetActive(true);
            Debug.Log("負け");
            //Syouri.text = "負け";
            Mark.SetActive(false);
            Text_Katimake.GetComponent<Image>().sprite = Make;

            fading.fading_time = 3.3f;
            fading.Fade(Fading.type.FadeOut);
            fading.OnFadeEnd.RemoveAllListeners(); // 追加する前にクリア

            fading.OnFadeEnd.AddListener(() =>
            {
                SceneManager.LoadScene(scene1);
            });
            //fading.Fade(Fading.type.FadeIn);

            Reset_();
        }

    }

    /// <summary>
    /// リセット
    /// </summary>
    void Reset_()
    {
        Debug.Log("Rest");
        lapTime = 0;
        playertime = 0;
        isStart = false;
        isSpace = false;
        fightStartTime = Random.Range(4.0f, 7.0f);
    }
    public void Text_SetActive()
    {
        Text_Katimake.SetActive(false);

    }
    public void TextScoreRestart()
    {
        ScoreOb.SetActive(false);
    }
}
