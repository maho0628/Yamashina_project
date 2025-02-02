using System.Collections;
using System.Collections.Generic;
using Toshiki;
using UnityEngine;

public class CS_WaveController : MonoBehaviour
{
    [SerializeField, Header("横の基準")]
    private float widthMax = 9;

    [SerializeField, Header("Yの位置")]
    private float[] instanceY;

    [SerializeField, Header("召喚するオブジェクト")]
    private GameObject[] matoPrefabs;

    [SerializeField, Header("召喚する生成マックス")]
    private int instanceMax = 3;

    [SerializeField, Header("切り替える時間")]
    private float resetTime = 10;

    [SerializeField, Header("オブジェクトを消す時間")]
    private float allMatoDestroyTime = 5;

    [SerializeField, Header("半分のときオブジェクトを消す時間")]
    private float allMatoDestroyTime2 = 3;

    [SerializeField, Header("プレイ時間")]
    private float playTime = 60;

    private float outTimer = 0;
    private float timer = 0;
    private bool isWaveStart = false;
    private bool isRandomWave = false;
    public bool WaveEnd = false;

    GameObject[] game;

    void Start()
    {
    }

    public void WaveStart()
    {
        // 生成する
        Spawner();
        isWaveStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveEnd)
        {
            return;
        }
        if (game == null)
        {
            return;
        }
        if (isWaveStart)
        {
            outTimer += Time.deltaTime;
            if (isRandomWave)
            {
                // 時間経過を計測
                timer += Time.deltaTime;
                if (outTimer > playTime / 2)
                {
                    // リセットタイムを超えたらすべてのオブジェクトを削除
                    if (timer > allMatoDestroyTime2)
                    {
                        AllMatoDestroy();
                    }
                }
                else
                {
                    // リセットタイムを超えたらすべてのオブジェクトを削除
                    if (timer > allMatoDestroyTime)
                    {
                        AllMatoDestroy();
                    }
                }
            }
            // オブジェクトが存在しているか
            int tmp = 0;
            for (int i = 0; i < game.Length; ++i)
            {
                if (game[i] == null)
                {
                    tmp++;
                }
            }
            if (tmp >= game.Length)
            {
                Spawner();
            }

        }
    }

    // 生成する
    private void Spawner()
    {
        game = new GameObject[3];
        // ランダムウェーブの場合Off
        if (isRandomWave)
        {
            isRandomWave = false;
        }

        int wavePattern = Random.Range(0, matoPrefabs.Length);

        switch (wavePattern)
        {
            case 0:
                //ランダムスポーン
                RandomSpawan();
                break;
            case 1:
                MoveSpawan();
                break;
        }
        timer = 0;

    }

    // ランダム生成
    private void RandomSpawan()
    {
        for (int i = 0; i < instanceMax; ++i)
        {
            Vector3 pos = new Vector3(
                Random.Range(-widthMax, widthMax),
                instanceY[Random.Range(0, instanceY.Length)],
                0);
            int rnd = Random.Range(0, 2);
            Debug.Log(rnd);
            game[i] = Instantiate(matoPrefabs[rnd], pos, Quaternion.identity);
        }
        isRandomWave = true;
    }

    // 移動生成
    private void MoveSpawan()
    {
        for (int i = 0; i < instanceMax; ++i)
        {
            int tmp = Random.Range(0, 2);
            float x = 0;
            if (tmp == 0)
            {
                x = widthMax + 1 + i;
            }
            else if (tmp != 0)
            {
                x = (widthMax + 1 + i) * -1;
            }
            Vector3 pos = new Vector3(
                x,
                instanceY[Random.Range(0, instanceY.Length)],
                0);
            game[i] = Instantiate(matoPrefabs[2], pos, Quaternion.identity);
            if (outTimer > playTime / 2)
            {
                game[i].GetComponent<CS_MoveMato>().Speed = 5.0f;

                game[i].GetComponent<Shageki_mato>().AddScore(Shageki_Manager.instance.point_Fast);
            }
            else
            {
                game[i].GetComponent<CS_MoveMato>().Speed = 2.0f;
            }

        }
    }

    // すべてのオブジェクトお削除
    private void AllMatoDestroy()
    {
        for (int i = 0; i < game.Length; ++i)
        {
            Destroy(game[i]);
        }
        //Invoke("Spawner", 1);
        Spawner();
    }
}
