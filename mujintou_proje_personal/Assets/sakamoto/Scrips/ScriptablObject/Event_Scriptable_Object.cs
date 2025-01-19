using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EventData")]
public class EventData : ScriptableObject
{
    [Header("�C�x���gID")]
    public int Event_ID;
    [Header("�w�i")]
    public Sprite Event_BG;
    [Header("�C�x���g�C���X�g")]
    public Sprite Event_Ilast;
    [Header("�C�x���g�^�C�g��")]
    public string Event_Title;
    [Header("�e�L�X�g�{��")]
    public string Main_Text;
    [Header("�I�����P")]
    public string Sentakusi1;
    [Header("�I�����Q")]
    public string Sentakusi2;
    [Header("�L�����Z��")]
    public string Cancel;
    [Header("�I����1����")]
    public string Sentakusi1_Result1;
    [Header("�I����1����ID")]
    public int Sentakusi1_Zyouken;
    [Header("�I����1�����̌�")]
    public int Sentakusi1_Zyouken_num;
    [Header("��V�A�C�e��ID")]
    public int Sentakusi1_Reward1;
    [Header("��V�A�C�e���̌�")]
    public int Sentakusi1_Reward1_num;
    [Header("��V�A�C�e��ID")]
    public int Sentakusi1_Reward2;
    [Header("��V�A�C�e���̌�")]
    public int Sentakusi1_Reward2_num;
    [Header("��V�A�C�e��ID")]
    public int Sentakusi1_Reward3;
    [Header("��V�A�C�e���̌�")]
    public int Sentakusi1_Reward3_num;
    [Header("�̗�")]
    public int Sentakusi1_health;
    [Header("�H��")]
    public int Sentakusi1_hunger;
    [Header("����")]
    public int Sentakusi1_warter;
    [Header("�I����1�̓��t����")]
    public int Sentakusi1_day;
    [Header("���̃C�x���gID")]
    public int Sentakusi1_Next_Ivent_ID;
    [Header("���̃C�x���gID")]
    public int Sentakusi1_Next_Ivent_ID_2;
    [Header("�I����2����1")]
    public string Sentakusi2_Result1;
    [Header("�I����2����ID")]
    public int Sentakusi2_Zyouken;
    [Header("�I����2�����̌�")]
    public int Sentakusi2_Zyouken_num;
    [Header("��V�A�C�e��ID")]
    public int Sentakusi2_Reward1;
    [Header("��V�A�C�e���̌�")]
    public int Sentakusi2_Reward1_num;
    [Header("��V�A�C�e��ID")]
    public int Sentakusi2_Reward2;
    [Header("��V�A�C�e���̌�")]
    public int Sentakusi2_Reward2_num;
    [Header("��V�A�C�e��ID")]
    public int Sentakusi2_Reward3;
    [Header("��V�A�C�e���̌�")]
    public int Sentakusi2_Reward3_num;
    [Header("�̗�")]
    public int Sentakusi2_health;
    [Header("�H��")]
    public int Sentakusi2_hunger;
    [Header("����")]
    public int Sentakusi2_warter;
    [Header("�I����2�̓��t����")]
    public int Sentakusi2_day;
    [Header("���̃C�x���gID")]
    public int Sentakusi2_Next_Ivent_ID;
    [Header("���̃C�x���gID")]
    public int Sentakusi2_Next_Ivent_ID_2;
    [Header("�L�����Z������")]
    public string Cancel_Result;
    [Header("���̃C�x���gID")]
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
