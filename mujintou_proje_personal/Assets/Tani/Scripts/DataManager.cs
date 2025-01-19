using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    [HideInInspector] public SaveData data;     // json�ϊ�����f�[�^�̃N���X
    string filepath;                            // json�t�@�C���̃p�X
    string fileName = "Data.json";              // json�t�@�C����
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
    // �J�n���Ƀt�@�C���`�F�b�N�A�ǂݍ���
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        // �p�X���擾
        
        folderPath = Application.streamingAssetsPath + "/Saves/";
        filepath = folderPath + fileName;
    }
    private void Start()
    {


   
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);                 // json�Ƃ��ĕϊ�
        StreamWriter wr = new StreamWriter(filepath, false);    // �t�@�C���������ݎw��
        wr.WriteLine(json);                                     // json�ϊ�����������������
        wr.Close();                                             // �t�@�C������
    }

    // json�t�@�C���ǂݍ���
    public SaveData Load()
    {
        StreamReader rd = new StreamReader(filepath);               // �t�@�C���ǂݍ��ݎw��
        string json = rd.ReadToEnd();                           // �t�@�C�����e�S�ēǂݍ���
        rd.Close();                                             // �t�@�C������

        return JsonUtility.FromJson<SaveData>(json);            // json�t�@�C�����^�ɖ߂��ĕԂ�
    }

     public void InitializeSaveData()
    {

        File.Copy(folderPath + default_fileName, filepath,true);
        print("�Z�[�u�f�[�^�͏���������܂���");
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