using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sakamoto;

public class CS_BigAiGoldFish : MonoBehaviour
{
    [SerializeField, Header("ポイの位置")]
    private Transform poiTrs;
    private float speed;
    float defaltSpeed = 500;
    [SerializeField, Header("発生場所")]
    private RectTransform[] instanceTrs;
    public Vector3 GetPos
    {
        get
        {
            return poiTrs.position;
        }
    }
    [SerializeField]
    Game_Maneger game_Maneger;
   

    // Boost
    float boostSpeed = 700;
    float timer = 0;
    float boostTimer = 1;

    // UneUne
    Quaternion initializeRotation = Quaternion.identity;
    float sin = 0;

    enum Action
    {
        Boost,
        UneUne
    }
    
   Action action = Action.UneUne;

    void Start()
    {
        // 初期化
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // 移動処理
        Move();
    }

    // 移動関数
    private void Move()
    {
        if (game_Maneger.Kingyo_Hassei_f)
        {

            switch (action)
            {
                case Action.Boost:
                    // 向いている方向に進む
                    transform.position += transform.up * speed * Time.deltaTime;
                    timer += Time.deltaTime;
                    // 途中で加速する
                    if (timer > boostTimer)
                    {
                        speed = boostSpeed;
                    }
                    break;
                case Action.UneUne:
                    transform.position += transform.up * speed * Time.deltaTime;
                    timer += Time.deltaTime;
                    break;
            }
        }
    }

    // ランダムに次の位置に回転する
    private void RandomRot()
    {
        Vector3 pos = new Vector3(Random.Range(-9, 9),
            Random.Range(2, -2), 0);
        // 対象物へのベクトルを算出
        Vector3 toDirection = pos - transform.position;
        // 対象物へ回転する
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
        initializeRotation = transform.rotation;
    }

    private void Initialize()
    {
        int index = Random.Range(0, instanceTrs.Length);
        transform.position = instanceTrs[index].position;

        Vector3 toDirection = poiTrs.position - transform.position;
        // 対象物へ回転する
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
        initializeRotation = transform.rotation;

        int tmp = Random.Range(-1, 2);

        if(tmp == 0)
        {
            action = Action.Boost;
        }
        else if(tmp == 1)
        {
            action = Action.UneUne;
            Invoke("RandomRot", 2);
        }
        // 基準のスピードにする
        speed = defaltSpeed;
        timer = 0;

        int rnd = Random.Range(10, 20);
        Invoke("Initialize", rnd);
    }
}
