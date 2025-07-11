using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 個々のコメント記載
/// その他コメント細かくノーツマネージャーから
/// ハードコーディングチェック
/// </summary>
public class NoteManager : SingletonMonoBehaviour<NoteManager>
{
    #region ノーツ表示の内部情報処理変数

    /// <summary>
    /// ノーツUIオブジェクトのプール
    /// </summary>
    [SerializeField, Tooltip("ノーツUIオブジェクトのプールを入れる")]
    private NoteUIPool notePool;

    [Space(15)]

    /// <summary>
    /// ノーツを配置するレーンの親オブジェクト
    /// </summary>
    [SerializeField, Tooltip("生成したレーン要素の親になる RectTransform")]
    private RectTransform laneContainer;

    #endregion


    #region ノーツ状態管理の内部情報処理変数

    /// <summary>
    /// これから生成される予定のノーツ
    /// </summary>
    private List<Note> pendingNotes = new List<Note>();

    /// <summary>
    /// 現在画面上に存在するノーツ
    /// </summary>
    private List<Note> activeNotes = new List<Note>();

    /// <summary>
    /// ノーツデータとUIの対応マップ
    /// </summary>
    private Dictionary<Note, NoteUI> noteToUIMap = new Dictionary<Note, NoteUI>();

    /// <summary>
    /// 全ノーツが生成されたかどうか
    /// </summary>
    private bool notesSpawned = false;

    #endregion


    #region 設定データ（Config系）の内部情報処理変数

    /// <summary>
    // 譜面データ（json などからロード）
    /// </summary>
    private ChartData chartData;

    /// <summary>
    /// ノーツスクロール設定データ
    /// </summary>
    private NoteScrollConfig scrollConfig;

    /// <summary>
    /// レーンの見た目の設定データ
    /// </summary>
    private LaneVisualConfig laneVisualConfig;

    /// <summary>
    /// ノーツのタイミング設定データ
    /// </summary>
    private NoteTimingConfig noteTimingConfig;

    /// <summary>
    /// キーラベル設定データ
    /// </summary>
    private KeyLabelConfig keyLabelConfig;

    /// <summary>
    /// ミス判定データ
    /// </summary>
    private JudgementConfig missJudgementConfig;

    #endregion


    #region フラグ系の内部情報処理変数

    /// <summary>
    /// ノーツ生成が許可されているか
    /// </summary>
    private bool canSpawnNotes = false;

    /// <summary>
    /// 初期化完了フラグ
    /// </summary>
    private bool isInitialized = false;

    #endregion


    #region イベント

    /// <summary>
    /// 全てのノートが生成し終わったタイミングで呼ばれるイベント。
    /// 外部でこのイベントを購読することで、譜面の生成完了を検知可能。 
    /// </summary>
    private event Action OnNotesSpawned;

    /// <summary>
    /// ノートマネージャーの初期化が完了したときに呼ばれるイベント。
    /// 初期化後に実行すべき処理を外部でフックする用途に使用。    /// </summary>
    private event Action OnInitialized;

    #endregion


    #region 読み取り専用プロパティ（状態・カウント）

    /// <summary>
    /// 全ノーツが生成されたかどうかの読み取り専用
    /// </summary>
    internal bool NotesSpawned => notesSpawned;

    /// <summary>
    /// 初期化完了フラグの読み取り専用
    /// </summary>
    internal bool IsInitialized => isInitialized;

    /// <summary>
    /// ノーツの合計値の読み取り専用
    /// </summary>
    internal int TotalNoteCount => chartData?.Notes?.Length ?? 0;

    #endregion


    #region 外部で呼び出し可能な関数（ノート生成関連）

    /// <summary>
    /// ノートの生成（Spawn）処理を許可するフラグを立てる。
    /// 初期化完了後、音楽再生とタイミングを合わせてノート生成を開始させるために使用。    /// </summary>
    internal void AllowNoteSpawning() => canSpawnNotes = true;

    #endregion


    #region 外部で呼び出し可能な関数（初期化・ノーツ取得・リセット）

    /// <summary>
    /// 初期化処理を非同期で実行
    /// </summary>
    internal void Initialize()
    {
        StartCoroutine(InitializeRoutine());
    }


    /// <summary>
    /// 指定されたアクション名に対応するレーン内で、
    /// 現在時刻に最も近いノーツを取得する。
    /// </summary>
    /// <param name="actionName">検索対象のアクション名（例："Lane1"）</param>
    /// <param name="currentTime">現在の再生時間（秒）</param>
    /// <param name="maxJudgementTime">許容する最大の判定時間差（秒）</param>
    /// <returns>最も近いノーツ。該当なしの場合は null を返す。</returns>
    internal Note GetNearestNoteByAction(string actionName, float currentTime, float maxJudgementTime)
    {
        // アクション名からレーンインデックスを取得
        int laneIndex = GetLaneIndexFromAction(actionName);
        if (laneIndex < 0) return null;

        Note nearest = null;
        float closest = maxJudgementTime;

        // アクティブなノーツの中から対象レーンの未ヒットノーツを探す
        foreach (var note in activeNotes)
        {
            if (note.IsHit || note.LaneNumber != laneIndex) continue;

            // 現在時刻との時間差を計算し、最も近いノーツを記録
            float diff = Mathf.Abs(note.SpawnTime - currentTime);
            if (diff < closest)
            {
                closest = diff;
                nearest = note;
            }
        }
        return nearest;
    }

    /// <summary>
    /// 
    /// </summary>
    internal void ResetForNewScene()
    {
        notesSpawned = false;
        isInitialized = false;
        canSpawnNotes = false;
        pendingNotes?.Clear();
        activeNotes?.Clear();
        noteToUIMap?.Clear();

        RefreshSerializedReferences();
    }

    #endregion


    #region プライベート関数（初期化）

    /// <summary>
    /// 非同期の初期化処理
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator InitializeRoutine()
    {
        // ステージが未設定なら仮の設定を読み込む
        if (!StageManager.Instance.IsStageSelected)
        {
            var testTable = Resources.Load<StageConfigTable>("ScriptableObject/stageConfig");
            StageManager.Instance.SetupStage(testTable, "test");
        }

        //各ステージの譜面の名前を取得する
        string chartFileName = StageManager.Instance.GetCurrentChartFileName();

        //譜面の名前に何も入らないなら処理しない
        if (string.IsNullOrEmpty(chartFileName)) yield break;

        //各ステージのノーツスクロール設定データを取得
        scrollConfig = StageManager.Instance.GetCurrentStageConfig()?.ScrollConfig;

        //ノーツスクロール設定データがないなら処理しない
        if (scrollConfig == null) yield break;

        //ノーツスクロール設定データ内のレーンの見た目の設定データを取得
        laneVisualConfig = scrollConfig.GetLaneVisualConfig();

        //ノーツスクロール設定データ内のノーツのタイミング設定データを取得
        noteTimingConfig = scrollConfig.GetNoteTimingConfig();

        //ノーツスクロール設定データ内のキーラベル設定データを取得
        keyLabelConfig = scrollConfig.GetKeyLabelConfig();

        // Miss 判定の設定を取得
        missJudgementConfig = JudgementManager.Instance.GetMissJudgement();

        // 譜面データ読み込み
        chartData = ChartJsonLoader.LoadChartData(chartFileName);

        //譜面データがないなら処理しない
        if (chartData == null) yield break;

        // レーンとラベルを作成
        CreateLanesAndLabels();

        // ノーツリストを待機リストへ
        pendingNotes = chartData.Notes.ToList();

        //初期化完了にする
        isInitialized = true;

        //初期化が完了したときに呼ばれるイベントを発火
        OnInitialized?.Invoke();
    }

    /// <summary>
    /// シリアライズ済み参照を再取得する
    /// </summary>
    private void RefreshSerializedReferences()
    {
        if (notePool == null)
        {
            notePool = FindAnyObjectByType<NoteUIPool>();
        }

        if (laneContainer == null)
        {
            var containerGO = GameObject.Find("NoteContainer");
            if (containerGO != null)
            {
                laneContainer = containerGO.GetComponent<RectTransform>();
            }
        }
    }

    #endregion


    #region プライベート関数(レーンとラベル生成関連)

    /// <summary>
    /// レーン画像とキーラベルを生成
    /// </summary>
    private void CreateLanesAndLabels()
    {
        //レーンの見た目の設定データ内のレーン数を入れる
        int laneCount = laneVisualConfig.LaneCount;

        //レーンの見た目の設定データ内の幅を入れる
        float laneWidth = laneVisualConfig.LaneWidth;

        //レーンの見た目の設定データ内の高さを入れる
        float laneHeight = laneVisualConfig.LaneHeight;

        // レーンコンテナ全体（親要素）の幅を取得
        float totalWidth = laneContainer.rect.width;
        // レーン全体を中央揃えに配置するための、1レーン目の中央X座標を取得
        float startX = laneVisualConfig.GetStartX(totalWidth);

        // 全レーン分繰り返して、背景画像とラベルを生成・配置する
        for (int i = 0; i < laneCount; i++)
        {
            // 各レーンのX座標を計算（レーン中央揃え）

            float posX = startX + i * laneVisualConfig.LaneWidth;

            // レーンの背景画像を生成し、位置・サイズ・見た目を設定
            GameObject laneImagePrefab = Instantiate(laneVisualConfig.LaneImagePrefab, laneContainer);
            RectTransform laneRT = laneImagePrefab.GetComponent<RectTransform>();
            laneRT.anchoredPosition = new Vector2(posX, noteTimingConfig.EndY);
            laneRT.sizeDelta = new Vector2(laneWidth, laneHeight);

            Image laneImage = laneImagePrefab.GetComponent<Image>();
            // レーンごとのスプライトを設定
            laneImage.sprite = laneVisualConfig.GetLaneSprite(i);

            // レーンごとの色を設定
            laneImage.color = laneVisualConfig.GetLaneColor(i);

            // キー入力ラベルが設定されている場合、対応ラベルを生成
            if (keyLabelConfig.LaneLabelPrefab != null)
            {
                // ラベルUIを生成し、位置とサイズを設定
                GameObject laneLabelPrefab = Instantiate(keyLabelConfig.LaneLabelPrefab, laneContainer);
                RectTransform labelRT = laneLabelPrefab.GetComponent<RectTransform>();
                labelRT.anchoredPosition = new Vector2(posX, noteTimingConfig.EndY);
                labelRT.sizeDelta = keyLabelConfig.LaneLabelSize;

                // テキスト情報を設定（キー文字・フォントなど）
                TextMeshProUGUI labelText = laneLabelPrefab.GetComponent<TextMeshProUGUI>();
                labelText.text = string.Format(keyLabelConfig.KeyLabels[i]);
                labelText.fontSize = keyLabelConfig.FontSize;
                labelText.color = keyLabelConfig.FontColor;
                labelText.font = keyLabelConfig.FontAsset;
                labelText.alignment = keyLabelConfig.Alignment;
            }
        }
    }

    /// <summary>
    /// アクション名（例: "Lane1"）からレーンインデックスを取得
    /// </summary>
    private int GetLaneIndexFromAction(string actionName)
    {
        if (actionName.StartsWith("Lane"))
        {
            string numberStr = actionName.Substring("Lane".Length);
            if (int.TryParse(numberStr, out int num))
            {
                int index = num - 1;
                if (index >= 0 && index < laneVisualConfig.LaneCount)
                    return index;
            }
        }
        return -1;
    }

    #endregion


    #region プライベート関数(ノーツ生成関連)

    /// <summary>
    /// スクロール時間に基づきノーツを生成
    /// </summary>
    private void SpawnNotesIfNeeded(float currentTime)
    {
        while (pendingNotes.Count > 0 && pendingNotes[0].SpawnTime - currentTime <= noteTimingConfig.ScrollDuration)
        {
            var note = pendingNotes[0];
            pendingNotes.RemoveAt(0);
            SpawnNote(note);
            activeNotes.Add(note);
        }

        if (!notesSpawned && pendingNotes.Count == 0)
        {
            notesSpawned = true;
            OnNotesSpawned?.Invoke();
        }
    }

    /// <summary>
    /// ノーツを画面上に生成
    /// </summary>
    private void SpawnNote(Note noteData)
    {
        int lane = noteData.LaneNumber;
        if (lane < 0 || lane >= laneVisualConfig.LaneCount) return;

        float laneWidth = laneVisualConfig.LaneWidth;
        float totalWidth = laneContainer.rect.width;
        float posX = -totalWidth / 2f + laneWidth / 2f + lane * laneWidth;

        Vector2 startPos = new Vector2(posX, noteTimingConfig.StartY);
        Vector2 endPos = new Vector2(posX, noteTimingConfig.EndY);

        var noteUI = notePool.Get();
        noteUI.transform.SetParent(laneContainer, false);
        noteUI.GetComponent<RectTransform>().anchoredPosition = startPos;
        noteUI.Setup(noteData.SpawnTime, noteTimingConfig.ScrollDuration, startPos, endPos, noteData);
    }

    #endregion


    #region プライベート関数(判定関連)

    /// <summary>
    /// Miss 判定処理
    /// </summary>
    private void CheckMissNotes(float currentTime)
    {
        float missWindow = missJudgementConfig.Logic.SetMaxTimeDifference;

        for (int i = activeNotes.Count - 1; i >= 0; i--)
        {
            var note = activeNotes[i];
            if (note.IsHit) continue;

            if (currentTime - note.SpawnTime > missWindow)
            {
                JudgementManager.Instance.ApplyJudgement(missJudgementConfig, note.LaneNumber);
                AnimationManager.Instance.ShowScoreEffect(missJudgementConfig);
                AnimationManager.Instance.ShowJudgeEffect(missJudgementConfig);
                activeNotes.RemoveAt(i);

            }
        }
    }

    #endregion


    #region Unityコールバック

    private void Update()
    {
        if (!isInitialized || !canSpawnNotes) return;

        float currentTime = AudioManager.Instance.GetCurrentBGMTime();
        SpawnNotesIfNeeded(currentTime);
        CheckMissNotes(currentTime);
    }

    private void OnDestroy()
    {
        noteToUIMap?.Clear();
        pendingNotes?.Clear();
        activeNotes?.Clear();
        notesSpawned = false;
        isInitialized = false;
        canSpawnNotes = false;
    }

    #endregion

}
