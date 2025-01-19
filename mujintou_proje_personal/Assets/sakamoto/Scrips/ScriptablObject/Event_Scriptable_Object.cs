using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EventData")]
public class EventData : ScriptableObject
{
    [Header("イベントID")]
    public int Event_ID;
    [Header("背景")]
    public Sprite Event_BG;
    [Header("イベントイラスト")]
    public Sprite Event_Ilast;
    [Header("イベントタイトル")]
    public string Event_Title;
    [Header("テキスト本文")]
    public string Main_Text;
    [Header("選択肢１")]
    public string Sentakusi1;
    [Header("選択肢２")]
    public string Sentakusi2;
    [Header("キャンセル")]
    public string Cancel;
    [Header("選択肢1結果")]
    public string Sentakusi1_Result1;
    [Header("選択肢1条件ID")]
    public int Sentakusi1_Zyouken;
    [Header("選択肢1条件の個数")]
    public int Sentakusi1_Zyouken_num;
    [Header("報酬アイテムID")]
    public int Sentakusi1_Reward1;
    [Header("報酬アイテムの個数")]
    public int Sentakusi1_Reward1_num;
    [Header("報酬アイテムID")]
    public int Sentakusi1_Reward2;
    [Header("報酬アイテムの個数")]
    public int Sentakusi1_Reward2_num;
    [Header("報酬アイテムID")]
    public int Sentakusi1_Reward3;
    [Header("報酬アイテムの個数")]
    public int Sentakusi1_Reward3_num;
    [Header("体力")]
    public int Sentakusi1_health;
    [Header("食料")]
    public int Sentakusi1_hunger;
    [Header("水分")]
    public int Sentakusi1_warter;
    [Header("選択肢1の日付条件")]
    public int Sentakusi1_day;
    [Header("次のイベントID")]
    public int Sentakusi1_Next_Ivent_ID;
    [Header("次のイベントID")]
    public int Sentakusi1_Next_Ivent_ID_2;
    [Header("選択肢2結果1")]
    public string Sentakusi2_Result1;
    [Header("選択肢2条件ID")]
    public int Sentakusi2_Zyouken;
    [Header("選択肢2条件の個数")]
    public int Sentakusi2_Zyouken_num;
    [Header("報酬アイテムID")]
    public int Sentakusi2_Reward1;
    [Header("報酬アイテムの個数")]
    public int Sentakusi2_Reward1_num;
    [Header("報酬アイテムID")]
    public int Sentakusi2_Reward2;
    [Header("報酬アイテムの個数")]
    public int Sentakusi2_Reward2_num;
    [Header("報酬アイテムID")]
    public int Sentakusi2_Reward3;
    [Header("報酬アイテムの個数")]
    public int Sentakusi2_Reward3_num;
    [Header("体力")]
    public int Sentakusi2_health;
    [Header("食料")]
    public int Sentakusi2_hunger;
    [Header("水分")]
    public int Sentakusi2_warter;
    [Header("選択肢2の日付条件")]
    public int Sentakusi2_day;
    [Header("次のイベントID")]
    public int Sentakusi2_Next_Ivent_ID;
    [Header("次のイベントID")]
    public int Sentakusi2_Next_Ivent_ID_2;
    [Header("キャンセル結果")]
    public string Cancel_Result;
    [Header("次のイベントID")]
    public int Cancel_Next_Ivent_ID;

   
#if UNITY_EDITOR
    public void SetEventData(int eventID, string eventTitle, string mainText, string sentakushi1, string sentakussi2, string cansel,
                            string sentakusi1Result1,  
                            int sentakusi1Zyouken,int sentakusi1Zyouken_num, 
                            int sentakusi1Reward1,int sentakusi1Reward1_num,int sentakusi1Reward2,int sentakusi1Reward2_num,int sentakusi1Reward3,int sentakusi1Reward3_num,
                            int sentakusi1Health, int sentakusi1Hunger,int sentakusi1Warter,
                            int sentakusi1NextIventID, 
                            string sentakusi2Result1,
                            int sentakusi2Zyouken,int sentakusi2Zyouken_num,
                            int sentakusi2Reward1,int sentakusi2Reward1_num, int sentakusi2Reward2, int sentakusi2Reward2_num, int sentakusi2Reward3, int sentakusi2Reward3_num,
                            int sentakusi2Health, int sentakusi2Hunger, int sentakusi2Warter,
                            int sentakusi2NextIventID,
                            string cancelResult, int cancelNextIventID)
    {
        this.Event_ID = eventID;
        this.Event_Title = eventTitle;
        this.Main_Text = mainText;
        this.Sentakusi1 = sentakushi1;
        this.Sentakusi2 = sentakussi2;
        this.Cancel = cansel;
        this.Sentakusi1_Result1 = sentakusi1Result1;
        this.Sentakusi1_Zyouken = sentakusi1Zyouken;
        this.Sentakusi1_Zyouken_num = sentakusi1Zyouken_num;
        this.Sentakusi1_Reward1 = sentakusi1Reward1;
        this.Sentakusi1_Reward1_num = sentakusi1Reward1_num;
        this.Sentakusi1_Reward2 = sentakusi1Reward2;
        this.Sentakusi1_Reward2_num = sentakusi1Reward2_num;
        this.Sentakusi1_Reward3 = sentakusi1Reward3;
        this.Sentakusi1_Reward3_num = sentakusi1Reward3_num;
        this.Sentakusi1_health = sentakusi1Health;
        this.Sentakusi1_hunger = sentakusi1Hunger; 
        this.Sentakusi1_warter = sentakusi1Warter; 
        this.Sentakusi1_Next_Ivent_ID = sentakusi1NextIventID;
        this.Sentakusi2_Result1 = sentakusi2Result1;
        this.Sentakusi2_Zyouken = sentakusi2Zyouken;
        this.Sentakusi2_Zyouken_num = sentakusi2Zyouken_num;
        this.Sentakusi2_Reward1 = sentakusi2Reward1;
        this.Sentakusi2_Reward1_num = sentakusi2Reward1_num;
        this.Sentakusi2_Reward2 = sentakusi2Reward2;
        this.Sentakusi2_Reward2_num = sentakusi2Reward2_num;
        this.Sentakusi2_Reward3 = sentakusi2Reward3;
        this.Sentakusi2_Reward3_num = sentakusi2Reward3_num;
        this.Sentakusi2_health = sentakusi2Health;
        this.Sentakusi2_hunger = sentakusi2Hunger;
        this.Sentakusi2_warter = sentakusi2Warter;

        this.Sentakusi2_Next_Ivent_ID = sentakusi2NextIventID;
        this.Cancel_Result = cancelResult; 
        this.Cancel_Next_Ivent_ID = cancelNextIventID;
    }
#endif
}
