using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class LocalizationManager_Event : MonoBehaviour
//{

    //public static LocalizationManager_Event Instance { get; private set; }

//    // ローカライズデータの定義
//    private Dictionary<SystemLanguage, Dictionary<int, LocalizedString>> localizedData;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//            LoadLocalizedData();
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void LoadLocalizedData()
//    {
//        localizedData = new Dictionary<SystemLanguage, Dictionary<int, LocalizedString>>();

//        // ここでローカライズデータをロードして、localizedDataに追加します
//        // 例: 
//        // localizedData[SystemLanguage.Japanese] = new Dictionary<int, LocalizedString>
//        // {
//        //     { 1, new LocalizedString("イベントタイトル", "イベントテキスト", new List<LocalizedChoice> { new LocalizedChoice("選択肢1", 0), new LocalizedChoice("選択肢2", 0) }) },
//        //     ...
//        // };
//    }

//    public LocalizedString GetLocalizedEventData(EventDatas data, SystemLanguage language)
//    {
//        if (localizedData.TryGetValue(language, out var eventDictionary))
//        {
//            if (eventDictionary.TryGetValue(data.scene_id, out var localizedString))
//            {
//                return localizedString;
//            }
//        }
//        return null;
//    }
//}

//[System.Serializable]
//public class LocalizedString
//{
//    public string event_title;
//    public string main_text;
//    public List<LocalizedChoice> localizedResults;

//    public LocalizedString(string title, string text, List<LocalizedChoice> choices)
//    {
//        event_title = title;
//        main_text = text;
//        localizedResults = choices;
//    }
//}

//[System.Serializable]
//public class LocalizedChoice
//{
//    public string choise_text;
//    public int required_action_value;

//    public LocalizedChoice(string text, int actionValue)
//    {
//        choise_text = text;
//        required_action_value = actionValue;
//    }
//}

//}
