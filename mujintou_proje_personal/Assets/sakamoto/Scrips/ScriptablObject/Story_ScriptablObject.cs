using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "MyScriptable/Create Story_Data")]
public class StoryData : ScriptableObject
{
    [Header("背景")]
    public Sprite BG;
    [Header("1行目の文")]
    public string FarstLine;
    [Header("2行目の文")]
    public string ScondLine;
    [Header("3行目の文")]
    public string ThirdLine;
    [Header("4行目の文")]
    public string FourthLine;
    [Header("5行目の文")]
    public string FifthLine;
    [Header("6行目の文")]
    public string SixthLine;
    [Header("7行目の文")]
    public string SeventhLine;
    [Header("8行目の文")]
    public string EightLine;
    [Header("9行目の文")]
    public string NinethLine;
    [Header("10行目の文")]
    public string TenthLine;
}
