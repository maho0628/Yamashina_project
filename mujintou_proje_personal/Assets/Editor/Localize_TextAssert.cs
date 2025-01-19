using UnityEngine;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using Newtonsoft.Json.Linq;
#endif

[CreateAssetMenu(fileName = "SheetData", menuName = "ScriptableObjectの生成/SheetDataの生成", order = 0)]
public class Localize_TextAssert : ScriptableObject
{
    public SheetDataRecord[] sheetDataRecord;
    [SerializeField] string spreadsheetId; // スプレッドシートのID
    [SerializeField] string apiKey;        // Google Sheets APIキー
    [SerializeField] string sheetName;     // シート名

    [System.Serializable]
    public class SheetDataRecord
    {
        public TextAsset Ja;
        public TextAsset En;

        public void SetEventData(TextAsset ja, TextAsset en)
        {
            this.Ja = ja;
            this.En = en;
        }
    }


#if UNITY_EDITOR

    public void WritesheetData()
    {
        string range = $"{sheetName}!A1:C100";

        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{spreadsheetId}/values/{range}?key={apiKey}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        if (request.result == UnityWebRequest.Result.Success)
        {
            var jsonResponse = request.downloadHandler.text;
            Debug.Log("Response: " + jsonResponse); // レスポンスをログに出力

            var jsonData = JObject.Parse(jsonResponse);
            var rows = jsonData["values"] as JArray;

            if (rows == null)
            {
                Debug.LogError("Rows are null");
                return;
            }

            var assetsOutPutPath = "Assets/Resources/Sheets";
            Debug.Log(assetsOutPutPath.ToString());
            for (int i = 1; i < rows.Count; i++) // 0行目はヘッダなので1行目から読込む
            {
                var rowData = rows[i] as JArray;

                if (rowData == null)
                {
                    Debug.LogError($"Row {i} is null");
                    continue;
                }

                if (rowData.Count < 3)
                {
                    Debug.LogError($"Row {i} does not have enough columns");
                    continue;
                }
            }
        }
                SheetDataRecord sheetDataRecord =new SheetDataRecord();
        if (!sheetDataRecord.En)
        {
         var da =    File.ReadAllText(sheetDataRecord.En.text);

        }


    }
    public void LoadSheetData()
    {
        // シート名と範囲を動的に設定
        // 範囲を具体的な行数を指定してみる (例: A1:C100)
        string range = $"{sheetName}!A1:C100";
        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{spreadsheetId}/values/{range}?key={apiKey}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();
        while (!request.isDone) { }

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {request.error}\nURL: {url}");
            return;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            var jsonResponse = request.downloadHandler.text;
            Debug.Log("Response: " + jsonResponse); // レスポンスをログに出力

            var jsonData = JObject.Parse(jsonResponse);
            var rows = jsonData["values"] as JArray;

            if (rows == null)
            {
                Debug.LogError("Rows are null");
                return;
            }

            var assetsOutPutPath = "Assets/Resources/Sheets";
            Debug.Log(assetsOutPutPath.ToString());
            for (int i = 1; i < rows.Count; i++) // 0行目はヘッダなので1行目から読込む
            {
                var rowData = rows[i] as JArray;

                if (rowData == null)
                {
                    Debug.LogError($"Row {i} is null");
                    continue;
                }

                if (rowData.Count < 3)
                {
                    Debug.LogError($"Row {i} does not have enough columns");
                    continue;
                }
                        SheetDataRecord sheetData = new SheetDataRecord();

                var filePathJa = assetsOutPutPath + "/" + rowData[0].ToString() + "_ja.txt";
                var filePathEn = assetsOutPutPath + "/" + rowData[0].ToString() + "_en.txt";
                Debug.Log(filePathEn.ToString());

                // 日本語テキストファイルを作成
                File.WriteAllText(filePathJa, rowData[1].ToString());
                // 英語テキストファイルを作成
                File.WriteAllText(filePathEn, rowData[2].ToString());
                Debug.Log(filePathEn);
                // 作成したファイルをTextAssetとして読み込む
                var textAssetJa = AssetDatabase.LoadAssetAtPath<TextAsset>(filePathJa);
                var textAssetEn = AssetDatabase.LoadAssetAtPath<TextAsset>(filePathEn);
                Debug.Log(textAssetEn);

                // 必要に応じてsheetDataRecordに追加
                if (sheetDataRecord.Length <= i - 1)
                {
                    System.Array.Resize(ref sheetDataRecord, i);
                }
                sheetDataRecord[i - 1] = new SheetDataRecord { Ja = textAssetJa, En = textAssetEn };
            }
            AssetDatabase.SaveAssets();
            Debug.Log("データの更新を完了しました");
        }
    }

    public static void Execute()
    {
        var asset = Selection.activeObject as Localize_TextAssert;

        if (asset == null)
        {
            Debug.LogError("Selected object is not a Localize_TextAssert asset.");
            return;
        }

        // スプレッドシートIDとAPIキーを設定
        asset.spreadsheetId = "150n0k79I9V7WSz6WbAENfVaVXHI7ErAcpiHdIh-H8OU"; // スプレッドシートのIDをここに設定
        asset.apiKey = "AIzaSyDgnxaj1AOSYROgHUQv5xmpVo_SnfAiepE";               // APIキーをここに設定
        asset.sheetName = "TextAsset";                  // シート名をここに設定（例: Sheet1）

        // データの読み込み
        asset.LoadSheetData();
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(Localize_TextAssert))]
public class SheetDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("データ更新"))
        {
            ((Localize_TextAssert)target).LoadSheetData();
        }
    }
}

#endif
