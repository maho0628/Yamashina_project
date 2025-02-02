using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_UIController : MonoBehaviour
{
    //[SerializeField, Header("TimeとScoreを管理しているオブジェクトのスクリプト")]

    [SerializeField]
    private float time = 0.0f;
    public float Time
    {
        set
        {
            time = value;
        } 
    }

    [SerializeField]
    private int score = 0;
    public int Score
    {
        set
        {
            score = value;
        } 
    }

    [SerializeField, Header("txtScore")]
    private Text txtScore;
    [SerializeField, Header("txtTime")]
    private Text txtTime;

    void Start()
    {
        
    }

    void Update()
    {
        if(txtTime == null || txtScore == null)
        {
            Debug.Log("テキストが取得できていない");
            return;
        }

        // テキストを変更
        txtScore.text = "" + score;
        txtTime.text = "" + (int)time;
    }
}
