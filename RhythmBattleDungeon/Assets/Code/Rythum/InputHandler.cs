using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの入力を受け取り、
/// 対応するレーンのノーツとタイミング判定を処理するクラス。
/// </summary>
public class InputHandler : MonoBehaviour
{
    #region 入力処理関連の内部情報処理変数

    /// <summary>
    /// Unity Input System によって自動生成された入力アクションのインスタンス。
    /// </summary>
    private PlayerInputActions inputActions;

    /// <summary>
    /// 各レーンに対応する入力アクションを格納した配列。
    /// </summary>
    private InputAction[] laneInputs;

    /// <summary>
    /// 現在のステージに存在するレーンの数。
    /// </summary>
    private int laneCount;

    /// <summary>
    /// 判定を行う際の最大許容時間（例：Miss判定の上限時間）。
    /// </summary>
    private float maxJudgementTime;

    #endregion

    /// <summary>
    /// オブジェクトが破棄される際に呼び出され、アクションを無効化します。
    /// </summary>
    private void OnDestroy()
    {
        foreach (var action in laneInputs)
        {
            action?.Disable();
        }
    }

    #region 初期化処理

    /// <summary>
    /// プレイヤー入力の初期化処理。
    /// 入力アクションをステージに応じてバインドし、各レーンに対応付けます。
    /// </summary>
    internal void InitializeInput()
    {
        //判定を行う際の最大許容時間を取得
        maxJudgementTime = JudgementManager.Instance.GetMaxJudgementTime();

        // 入力アクション初期化
        inputActions = new PlayerInputActions();
        inputActions.Gameplay.Enable();

        // 現在のステージのレーン数を取得（デフォルトは4）
        laneCount = StageManager.Instance.GetCurrentStageConfig()?.ScrollConfig?.GetLaneVisualConfig().LaneCount ?? 4;

        //各レーンに対応する入力アクションを格納した配列をレーン数分生成
        laneInputs = new InputAction[laneCount];

        for (int i = 0; i < laneCount; i++)
        {
            string actionName = $"Lane{i + 1}";

            DebugManager.Log($"アクション名: {actionName}");

            // アクション名に対応する入力アクションを取得
            laneInputs[i] = inputActions.FindAction(actionName);

            if (laneInputs[i] != null)
            {
                DebugManager.Log($"入力アクション登録: {laneInputs[i]}");

                // クロージャ対策のため、ローカル変数にコピー
                string actionCopy = actionName;

                // 入力イベントが発生したらノーツを判定
                laneInputs[i].performed += ctx => TryHitNoteByAction(actionCopy);
            }
            else
            {
                DebugManager.LogWarning($"[InputHandler] アクション {actionName} が見つかりませんでした。");
            }
        }
    }

    #endregion

    /// <summary>
    /// 指定されたアクション名に対応するノーツがタイミング的にヒット可能かどうかを判定します。
    /// </summary>
    /// <param name="actionName">レーンに対応する入力アクション名</param>
    private void TryHitNoteByAction(string actionName)
    {

        ///  現在の BGM 再生位置（秒）を取得
        float currentTime = AudioManager.Instance.GetCurrentBGMTime();

        // 近いタイミングのノーツを取得
        var note = NoteManager.Instance.GetNearestNoteByAction(actionName, currentTime, maxJudgementTime);

        // ヒットできるノーツが存在しない場合は処理しない
        if (note == null) return;

        // ノーツの出現時間（SpawnTime）と現在のBGM再生時間（currentTime）の差を絶対値で取得。
        float diff = Mathf.Abs(note.SpawnTime - currentTime);

        // 判定ランク（Perfect, Good など）を取得
        var judgement = JudgementManager.Instance.EvaluateTiming(diff);
        AudioManager.Instance.PlaySEById(judgement.JudgementSE);


        //判定ランクがないなら処理しない
        if (judgement == null) return;

        DebugManager.Log($"エフェクト再生: {judgement.Logic.JudgementName} on lane {note.LaneNumber}");

        // 既にヒット済みのノーツは処理しない
        if (note.IsHit) return;

        //ノーツがヒットしたと判定
        note.IsHit = true;

        DebugManager.Log($"note.IsHit: {note.IsHit}");

        // 判定適用と各種エフェクト表示
        JudgementManager.Instance.ApplyJudgement(judgement, note.LaneNumber);
        AnimationManager.Instance.ShowScoreEffect(judgement);
        AnimationManager.Instance.ShowJudgeEffect(judgement);

    }
}
