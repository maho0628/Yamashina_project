/// <summary>
/// ノーツのコンボ数を管理するシングルトンクラス。
/// 現在のコンボ数、最大コンボ数、およびフルコンボ（全てのノーツをミスなく繋いだか）を判定・管理します。 
/// </summary>
public class ComboManager : SingletonMonoBehaviour<ComboManager>
{
    #region コンボ関連の内部情報処理変数

    /// <summary>
    /// 現在のコンボ数
    /// 成功したノーツ数を連続でカウントします。
    /// </summary>
    private int currentCombo = 0;

    /// <summary>
    /// 記録された最大コンボ数。
    /// ゲーム中の連続成功数の最高値を記録します。
    /// </summary>
    private int maxCombo = 0;

    #endregion


    #region  読み取り専用フィールド(コンボ関連の内部情報処理変数)

    /// <summary>
    /// 現在のコンボ数の読み取り専用
    /// </summary>
    internal int CurrentCombo => currentCombo;

    /// <summary>
    ///記録された最大コンボ数の読み取り専用
    /// </summary>
    internal int MaxCombo => maxCombo;

    #endregion


    #region 外部で呼び出し可能なコンボ処理に関する関数

    /// <summary>
    /// コンボ数を1増加させます。
    /// 成功時に呼び出され、最大コンボ数の更新も行います。
    internal void IncrementCombo()
    {
        currentCombo++;
        if (currentCombo > maxCombo)
            maxCombo = currentCombo;
    }

    /// <summary>
    /// コンボ数をリセットします。
    /// ミスなどでコンボが途切れたときに呼び出されます。
    internal void ResetCombo()
    {
        currentCombo = 0;
    }

    /// <summary>
    /// フルコンボを達成しているかを判定します。
    /// 最大コンボ数が総ノーツ数と一致する場合、フルコンボとみなします。
    /// </summary>
    /// <returns>フルコンボなら true、それ以外は false</returns>
    internal bool IsFullCombo()
    {
        return maxCombo == NoteManager.Instance.TotalNoteCount; 
    }

    /// <summary>
    /// 現在のコンボ数と最大コンボ数を両方リセットします。
    /// ゲーム開始時やリトライ時に呼び出します。
    internal void ResetAll()
    {
        currentCombo = 0;
        maxCombo = 0;
    }

    #endregion
}
