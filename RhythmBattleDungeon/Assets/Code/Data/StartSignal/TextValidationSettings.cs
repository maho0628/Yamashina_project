using UnityEngine;

/// <summary>
/// アニメーション時間の妥当性チェッククラス
/// </summary>
[CreateAssetMenu(menuName = "TextAnimation/ValidationSettings")]
public class TextValidationSettings : ScriptableObject
{
    #region アニメーション時間の妥当性チェックの内部管理用変数

    /// <summary>
    /// アニメーション時間の妥当性チェック設定
    /// 短いと『短すぎる』とみなされる
    /// </summary>
    [Header(" アニメーション時間の妥当性チェック設定（秒）\n" +
        " - この設定は totalDuration の警告表示に使用されます\n" +
        " - 短すぎる → ⚠️ 短すぎる（min 未満）\n" +
        " - 長すぎる → ⚠️ 長すぎる（max 超過）")]

    [SerializeField, Tooltip("この時間より短いと『短すぎる』とみなされます")]
    [Range(0.1f, 5.0f)]
    private float minDurationThreshold = 0.5f;

    [Space(15)]

    /// <summary>
    /// アニメーション時間の妥当性チェック設定
    /// 長いと『長すぎる』とみなされる
    /// </summary>
    [SerializeField, Tooltip("この時間より長いと『長すぎる』とみなされます")]
    [Range(0.1f, 5.0f)]
    private float maxDurationThreshold = 3.0f;

    #endregion


    #region 読み取り専用プロパティ(アニメーション時間の妥当性チェックの内部管理用変数)

    /// <summary>
    /// アニメーション時間の妥当性チェック設定の読み取り専用
    /// 短いと『短すぎる』とみなされる
    /// </summary>
    internal float MinDurationThreshold
    {
        get { return minDurationThreshold; }
        set { minDurationThreshold = value; }
    }

    /// <summary>
    /// アニメーション時間の妥当性チェック設定の読み取り専用
    /// 長いと『長すぎる』とみなされる
    /// </summary>
    internal float MaxDurationThreshold
    {
        get { return maxDurationThreshold; }    
        set { maxDurationThreshold = value; }   
    }

    #endregion

}
