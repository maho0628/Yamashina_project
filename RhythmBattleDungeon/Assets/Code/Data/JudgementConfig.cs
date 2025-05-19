using UnityEngine;

/// <summary>
/// 判定一つ分の設定情報を表すクラス。
/// 判定名、許容タイミング、表示色、表示用アイコンを管理します。
/// </summary>
[System.Serializable]
public class JudgementConfig
{
    // === 判定の基本情報 ===

    /// <summary>
    /// 判定の名前（例: Perfect, Great, Missなど）
    /// </summary>
    [SerializeField, Header("判定の名前（例: Perfect / Great / Miss など）")]
    private string judgementName;

    /// <summary>
    /// 判定が成立する許容時間（理想タイミングから±何秒以内か）
    /// 例：0.05秒なら理想タイミングの前後0.05秒以内でこの判定になる。
    /// </summary>
    [Tooltip("この秒数以内に押せばこの判定になります（例: 0.05 = ±0.05秒）")]
    [SerializeField, Header("判定の許容時間（理想タイミングから何秒ズレてもOKか）")]
    private float maxTimeDifference;

    // === 見た目の設定 ===

    /// <summary>
    /// 判定の表示に使うカラー。UIの色分けなどに利用。
    /// </summary>
    [SerializeField, Header("判定の表示カラー（UIなどに使用）")]
    private Color displayColor;

    /// <summary>
    /// 判定のアイコン画像（任意）。
    /// 設定がない場合は名前のみ表示。
    /// </summary>
    [Tooltip("表示用の画像。設定しない場合は名前だけ表示されます。")]
    [SerializeField, Header("判定のアイコン（任意）")]
    private Sprite displayIcon;

    // === 読み取り専用プロパティ ===

    /// <summary>
    /// 判定名の読み取り専用
    /// </summary>
    internal string JudgementName => judgementName;

    /// <summary>
    /// 判定の許容時間の読み取り専用
    /// </summary>
    internal float MaxTimeDifference => maxTimeDifference;

    /// <summary>
    /// 判定表示用カラーの読み取り専用
    /// </summary>
    internal Color DisplayColor => displayColor;

    /// <summary>
    /// 判定表示用アイコンの読み取り専用
    /// </summary>
    internal Sprite DisplayIcon => displayIcon;

}