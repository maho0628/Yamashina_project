using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    [HideInInspector] public SaveData data;     // json変換するデータのクラス
    string filepath;                            // jsonファイルのパス
    string fileName = "Data.json";              // jsonファイル名
    string default_fileName = "default_save_data.json";
    string folderPath;
   
    List<string> DeleteWhenIntialized = new() { 
        "PlayerInventryData.txt",
        "BaseLocationStorageSlotData.txt", 
        "FoodUsageLog.txt" ,
        "CraftPanelItemData.txt" , 
        "cookngSlotData.txt",
        "MakeFireSlotData.txt",
        "AddFireSlotData.txt"
    };
    //-------------------------------------------------------------------
    // 開始時にファイルチェック、読み込み
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        // パス名取得
        
        folderPath = Application.streamingAssetsPath + "/Saves/";
        filepath = folderPath + fileName;
    }
    private void Start()
    {


   
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);                 // jsonとして変換
        StreamWriter wr = new StreamWriter(filepath, false);    // ファイル書き込み指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();                                             // ファイル閉じる
    }

    // jsonファイル読み込み
    public SaveData Load()
    {
        StreamReader rd = new StreamReader(filepath);               // ファイル読み込み指定
        string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
        rd.Close();                                             // ファイル閉じる

        return JsonUtility.FromJson<SaveData>(json);            // jsonファイルを型に戻して返す
    }

     public void InitializeSaveData()
    {

        File.Copy(folderPath + default_fileName, filepath,true);
        print("セーブデータは初期化されました");
        foreach (var item in DeleteWhenIntialized)
        {
            File.Delete(folderPath + item);
            File.Delete(folderPath + item + ".meta");
            print(folderPath + item + " was deleted");

        }
    }

    static public void ErasePlayerSaveData()
    {
        File.Delete(Application.streamingAssetsPath + "/Saves/Data.json");
    }
    

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    static public bool DoesSaveExist()
    {
        return File.Exists(Application.streamingAssetsPath + "/Saves/Data.json");
    }

}