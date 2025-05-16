using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// ノーツ（音符）のデータを表すクラスです。
/// ノーツは特定のタイミングとレーンで発生し、ゲームの中で移動していきます。
/// </summary>
public class Note
{
    /// <summary>
    /// ノーツが出現する時間（秒）です。
    /// プレイヤーがノーツをタイミングよくヒットするために使用します。
    /// </summary>
    [JsonProperty("time")]
    public float SpawnTime { get; internal set; }

    /// <summary>
    /// ノーツが出現するレーン番号です。
    /// ゲーム内でノーツは複数のレーンを通るので、各レーンに対応する番号を持っています。
    /// 例えば、0番が左端のレーン、3番が右端のレーンといった形です。
    /// </summary>
    [JsonProperty("lane")]
    public int LaneNumber { get; internal set; }

    /// <summary>
    /// ノーツがヒットしたかどうかを示すフラグです。
    /// プレイヤーがノーツをヒットした場合に更新されますが、ゲームロジック内でのみ使用されます。
    /// </summary>
    [JsonIgnore]
    internal bool IsHit { get; set; }

 
}

