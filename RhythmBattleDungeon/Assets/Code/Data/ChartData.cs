using Newtonsoft.Json;

/// <summary>
/// 譜面全体のデータ（複数のノーツを含む）
/// </summary>
public class ChartData
{
    /// <summary>
    /// JsonProperty 属性により、"notes" というキーで Note[] をマッピング
    /// → JSONの"notes"配列をこのプロパティにデシリアライズできる
    /// ※ 学習用に詳細コメントを記載
    /// </summary>
    [JsonProperty("notes")]
    public Note[] Notes { get; internal set; }
}




