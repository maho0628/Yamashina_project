using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// リザルト画面関連の設定値をまとめたデータオブジェクト。
/// </summary>
[CreateAssetMenu(fileName = "ResultSceneSettings", menuName = "Config/ResultScene Settings")]
public class ResultSceneSettings : ScriptableObject
{
    /// <summary>
    /// スクリーンショットがファイルとして保存されるまで待つ最大秒数
    /// </summary>
    [SerializeField, Header("スクリーンショットがファイルとして保存されるまで待つ最大秒数")]
    [Range(0.5f, 10f)]
    private float saveTimeout = 2f;

    /// <summary>
    /// スクリーンショット保存フォルダ名
    /// </summary>
    [SerializeField, Header("スクリーンショット保存フォルダ名")]
    private string folderName = "screenshot";


    /// <summary>
    /// タイトル画面に戻るまでの秒数
    /// </summary>
    [SerializeField, Header("タイトル画面に戻るまでの秒数")]
    private float returnToTitleDelay;



    /// <summary>
    /// スクリーンショットがファイルとして保存されるまで待つ最大秒数の読み取り専用
    /// </summary>
    public float SaveTimeOut
    {
        get { return saveTimeout; }
    }

    /// <summary>
    /// スクリーンショット保存フォルダ名の読み取り専用
    /// </summary>
    public string FolderName
    {
        get { return folderName; }
    }

    /// <summary>
    /// タイトル画面に戻るまでの秒数の読み取り専用
    /// </summary>
    public float ReturnToTitleDelay
    {
        get { return returnToTitleDelay; }
    }
}

