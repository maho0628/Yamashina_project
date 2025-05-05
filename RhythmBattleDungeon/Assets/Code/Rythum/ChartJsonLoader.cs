using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// Resources フォルダ内の JSON ファイルを読み込み、ChartData オブジェクトに変換する静的ユーティリティクラス。
/// </summary>
public static class ChartJsonLoader
{
    /// <summary>
    /// 指定したパスの JSON ファイルを読み込み、<see cref="ChartData"/> をデシリアライズして返す。
    /// </summary>
    /// <param name="filePath">
    /// Resources フォルダからの相対パス（拡張子を除く）。例: "Charts/level1"
    /// </param>
    /// <returns>
    /// JSON の内容を反映した <see cref="ChartData"/> オブジェクト。読み込み失敗時は null を返す。
    /// </returns>
    public static ChartData LoadChartData(string filePath)
    {
        // Resources フォルダから TextAsset として JSON ファイルを読み込む
        TextAsset jsonFile = Resources.Load<TextAsset>(filePath);

        // もしファイルが存在しない場合は、エラーログを出力して null を返す
        if (jsonFile == null)
        {
            Debug.LogError($"[ChartJsonLoader] JSON ファイルが見つかりません: '{filePath}'");
            return null;
        }

        // 以下、JSON文字列をオブジェクトに変換する処理。
        // 初めての try-catch 学習用にコメントを詳しく記載。
        try
        {
            // DeserializeObject<T> メソッドで JSON テキストから ChartData オブジェクトを生成
            ChartData chartData = JsonConvert.DeserializeObject<ChartData>(jsonFile.text);

            // 正常に変換できた場合は生成したオブジェクトを返す
            return chartData;
        }
        catch (JsonException e)
        {
            // JsonException：JSONの形式がおかしい場合などにスローされる例外
            Debug.LogError($"[ChartJsonLoader] JSON のパースに失敗しました: {e.Message}");
            return null;
        }
        catch (System.Exception e)
        {
            // その他の例外キャッチブロック
            Debug.LogError($"[ChartJsonLoader] JSON読み込み中に予期しないエラーが発生しました: {e.Message}");
            return null;
        }
    }
}
