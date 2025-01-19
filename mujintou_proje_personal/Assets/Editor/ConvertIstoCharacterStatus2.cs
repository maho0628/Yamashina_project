using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
//Unityのエディタ上だけで使える機能を追加
//※通常のゲーム本体（Editorフォルダの外）で使わない！
using UnityEditor;

//Excelのデータ読み込み用のdllから追加
using ExcelDataReader;

//ファイル入出力のために利用
using System.IO;
using UnityEngine.Localization.Settings;

public class ConvertXlsxToCharacterStatus
{
    // MenuItemのAttributeを付けると
    // Unityエディタ上でメニューを選んだときに関数を実行してもらえる
    [MenuItem("Tools/Excelのデータからステータスを生成")]
    public static void Execute()
    {
        var inputPath = EditorUtility.OpenFilePanel("Excelのファイルを開く", "", "xlsx");
        if (string.IsNullOrEmpty(inputPath))
        {
            return;
        }

        var outputPath = EditorUtility.OpenFolderPanel("出力先のフォルダを開く", "", "");
        if (string.IsNullOrEmpty(outputPath))
        {
            return;
        }

        // TODO: outputPathがAssetsフォルダ以下出ない場合はエラーにする
        // 今回は書き出し先をそれ以外にはしないということで割愛

        // フルパスになっているoutputPathをAssetsからのパスになるように変更
        var assetsOutPutPath = outputPath.Replace(Application.dataPath, "Assets");

        using (var inputStream = File.OpenRead(inputPath))
        {
            var reader = ExcelReaderFactory.CreateReader(inputStream);
            var dataset = reader.AsDataSet();
            var sheet = dataset.Tables[0];

            // 0行目は項目名なので飛ばして1行目から読込む
            for (int row = 1; row < sheet.Rows.Count; row++)
            {
                var rowData = sheet.Rows[row];

                // A列目が名前なのでそれをファイル名にする
                var filePath = assetsOutPutPath + "/" + rowData[0].ToString() + ".asset";

                // ファイル名のアセットがあるかどうかで分岐
                EventData datas = null;
                if (File.Exists(filePath))
                {
                    // あるなら更新のために読込む
                    datas = AssetDatabase.LoadAssetAtPath<EventData>(filePath);
                }
                else
                {
                    // なければ新規作成
                    datas = ScriptableObject.CreateInstance<EventData>();
                    AssetDatabase.CreateAsset(datas, filePath);
                }

                // Excelのデータからステータスにint変換して設定
                datas.SetEventData(
                    int.Parse(rowData[1].ToString()),
                    GetLocalizedString(rowData[2].ToString()),
                    rowData[3].ToString(),
                    rowData[4].ToString(),
                    rowData[5].ToString(),
                    rowData[6].ToString(),
                    rowData[7].ToString(),
                    int.Parse(rowData[8].ToString()),
                    int.Parse(rowData[9].ToString()),
                    int.Parse(rowData[10].ToString()),
                    int.Parse(rowData[11].ToString()),
                    int.Parse(rowData[12].ToString()),
                    int.Parse(rowData[13].ToString()),
                    int.Parse(rowData[14].ToString()),
                    int.Parse(rowData[15].ToString()),
                    int.Parse(rowData[16].ToString()),
                    int.Parse(rowData[17].ToString()),
                    int.Parse(rowData[18].ToString()),
                    int.Parse(rowData[19].ToString()),
                    rowData[20].ToString(),
                    int.Parse(rowData[21].ToString()),
                    int.Parse(rowData[22].ToString()),
                    int.Parse(rowData[23].ToString()),
                    int.Parse(rowData[24].ToString()),
                    int.Parse(rowData[25].ToString()),
                    int.Parse(rowData[26].ToString()),
                    int.Parse(rowData[27].ToString()),
                    int.Parse(rowData[28].ToString()),
                    int.Parse(rowData[29].ToString()),
                    int.Parse(rowData[30].ToString()),
                    int.Parse(rowData[31].ToString()),
                    int.Parse(rowData[32].ToString()),
                    rowData[33].ToString(),
                    int.Parse(rowData[34].ToString())
                );

                // ファイルの更新フラグを立てる
                EditorUtility.SetDirty(datas);
            }
            // アセットの保存
            AssetDatabase.SaveAssets();
        }
    }

    private static string GetLocalizedString(string key)
    {
        // ローカライズテーブルの参照を取得する（適切なテーブル名に置き換える）
        var table = LocalizationSettings.StringDatabase.GetTable("YourTableName");

        // テーブルが null でないか確認
        if (table == null)
        {
            Debug.LogError("Localization table not found.");
            return key; // テーブルが見つからない場合はキーそのものを返す
        }

        // キーに対応するローカライズされたエントリーを取得
        var entry = table.GetEntry(key);
        if (entry == null)
        {
            Debug.LogError($"Localization entry not found for key: {key}");
            return key; // エントリーが見つからない場合はキーそのものを返す
        }

        // ローカライズされた値を取得する
        return entry.LocalizedValue;
    }
}

