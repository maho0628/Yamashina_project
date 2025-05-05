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

/// <summary>
/// 1つのノーツ（音符）を表すデータ
/// </summary>
public class Note
{
    /// <summary>
    /// JsonPropertyにより、"time" というキーがこのプロパティにマッピングされる
    /// ノーツが出現するタイミング（秒）
    /// </summary>
    [JsonProperty("time")]
    public float SpawnTime { get; internal set; }

    /// <summary>
    /// JsonPropertyにより、"lane" というキーがこのプロパティにマッピングされる
    /// ノーツが出現するレーン番号（例：0〜3）
    /// </summary>
    [JsonProperty("lane")]
    public int LaneNumber { get; internal set; }
}


