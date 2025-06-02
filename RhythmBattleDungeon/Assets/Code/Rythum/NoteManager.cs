using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : SingletonMonoBehaviour<NoteManager>
{
    [SerializeField] private NoteUIPool notePool;
    [SerializeField] private RectTransform parentRect; // Canvas配下の親RectTransform
    [SerializeField] private Image[] laneImages;
    private List<Note> pendingNotes = new List<Note>();
    private List<Note> activeNotes = new List<Note>();
    private Dictionary<Note, NoteUI> noteToUIMap = new Dictionary<Note, NoteUI>();

    private ChartData chartData;


    private NoteScrollConfig scrollConfig;
    private JudgementConfig missJudgementConfig;

    private bool notesSpawned = false;
    private bool isInitialized = false;

    public bool NotesSpawned => notesSpawned;
    public bool IsInitialized => isInitialized;



    public event Action OnNotesSpawned;

    public event Action OnInitialized; // ← 新しいイベント



    private void SetupLaneImages()
    {
        int laneCount = laneImages.Length;

        if (laneCount == 0)
        {
            Debug.LogError("[NoteManager] laneImages が空です。");
            return;
        }

        float totalWidth = parentRect.rect.width;
        float laneWidth = totalWidth / laneCount;
        float startX = -totalWidth / 2f + laneWidth / 2f;

        // scrollConfig.StartY を使用してレーンの開始位置（Y軸）を調整
        float startYPosition = scrollConfig.StartY; // 上からの位置
        float endYPosition = scrollConfig.EndY;     // 下からの位置

        for (int i = 0; i < laneCount; i++)
        {
            RectTransform laneRect = laneImages[i].rectTransform;
            // レーンの画像を設定
            Sprite laneSprite = scrollConfig.GetLaneSprite(i);
            if (laneSprite != null)
            {
                laneImages[i].sprite = laneSprite; // 画像を設定
            }
            // レーンの色を設定
            laneImages[i].color = scrollConfig.GetLaneColor(i);
            // レーンの Y 座標（startYと endYの間で調整）
            // Y位置を調整するため、scrollConfig.endYPosition を使って配置
            laneRect.anchoredPosition = new Vector2(startX + i * laneWidth, endYPosition);

            // 必要に応じてサイズを調整
            laneRect.sizeDelta = new Vector2(laneWidth, scrollConfig.LaneHeight);
        }
    }

    public void Initialize()
    {
        StartCoroutine(InitializeRoutine());
    }




    private IEnumerator InitializeRoutine()
    {
        // ステージ未初期化なら、テスト用ステージを強制ロード
        if (!StageManager.Instance.IsStageSelected)
        {
            var testTable = Resources.Load<StageConfigTable>("ScriptableObject/stageConfig");
            StageManager.Instance.SetupStage(testTable, "test");
        }

        string chartFileName = StageManager.Instance.GetCurrentChartFileName();
        if (string.IsNullOrEmpty(chartFileName))
        {
            Debug.LogWarning("[NoteManager] ChartFileName が設定されていません。");
            yield break;
        }

        missJudgementConfig = JudgementManager.Instance.GetMissJudgement();
        scrollConfig = StageManager.Instance.GetCurrentStageConfig()?.ScrollConfig;
        if (scrollConfig == null)
        {
            Debug.LogError("[NoteManager] scrollConfig が null！");
            yield break;
        }

        chartData = ChartJsonLoader.LoadChartData(chartFileName);
        if (chartData == null)
        {
            Debug.LogError("[NoteManager] チャートデータの読み込みに失敗しました");
            yield break;
        }

        pendingNotes = chartData.Notes.ToList();

        SetupLaneImages();

        isInitialized = true;
        OnInitialized?.Invoke(); // ← 初期化完了を通知
    }



    private void Update()
    {
        if (!isInitialized) return; 

        float currentTime = AudioManager.Instance.GetCurrentBGMTime();
        SpawnNotesIfNeeded(currentTime);
        CheckMissNotes(currentTime);
        Debug.Log($"[ScoreManager] TotalNoteCount: {TotalNoteCount}");
        Debug.Log($"[ScoreManager] 実際の chartData.Notes.Length: {chartData.Notes.Length}");

    }

    /// <summary>
    /// 時間になったノーツを出す（Updateから呼ばれる）
    /// </summary>
    private void SpawnNotesIfNeeded(float currentTime)
    {
        // NOTE: pendingNotes は SpawnTime が昇順で並んでいる前提
        while (pendingNotes.Count > 0 && pendingNotes[0].SpawnTime - currentTime <= scrollConfig.ScrollDuration)
        {
            var note = pendingNotes[0];
            pendingNotes.RemoveAt(0);
            SpawnNote(note);
            activeNotes.Add(note);
        }
        if (!notesSpawned && pendingNotes.Count == 0)
        {
            notesSpawned = true;
            Debug.Log($"[NoteManager] ActiveNotes.Count: {activeNotes.Count}, TotalNoteCount: {TotalNoteCount}");

            OnNotesSpawned?.Invoke();
        }
        notesSpawned = true; // フラグを立てる

        Debug.Log(OnNotesSpawned);
        OnNotesSpawned?.Invoke();
        Debug.Log("Invoke すべてのノーツ");
    }

    public void SpawnNote(Note noteData)
    {
        int lane = noteData.LaneNumber;
        float offsetTime = noteData.SpawnTime;

        if (lane < 0 || lane >= scrollConfig.LaneCount)
        {
            Debug.LogWarning($"[NoteManager] 無効なレーン番号: {lane}");
            return;
        }

        float totalWidth = parentRect.rect.width;
        float laneWidth = totalWidth / scrollConfig.LaneCount;
        float startX = -totalWidth / 2f + laneWidth / 2f + lane * laneWidth;

        Vector2 startPos = new Vector2(startX, scrollConfig.StartY);
        Vector2 endPos = new Vector2(startX, scrollConfig.EndY);

        var noteUI = notePool.Get();
        noteUI.transform.SetParent(parentRect, false);

        // noteData を NoteUI に渡す
        noteUI.Setup(offsetTime, scrollConfig.ScrollDuration, startPos, endPos, noteData);
    }
    private void CheckMissNotes(float currentTime)
    {

        float missWindow = missJudgementConfig.Logic.SetMaxTimeDifference;

        for (int i = activeNotes.Count - 1; i >= 0; i--)
        {
            var note = activeNotes[i];
            
            if (note.IsHit) continue;

            if (currentTime - note.SpawnTime > missWindow)
            {
                JudgementManager.Instance.ApplyJudgement(missJudgementConfig, activeNotes[i].LaneNumber);
                AnimationManager.Instance.ShowScoreEffect(missJudgementConfig);
                AnimationManager.Instance.ShowJudgeEffect(missJudgementConfig);
                activeNotes.RemoveAt(i);
            }
        }

    }

    public RectTransform GetNoteParentTransform()
    {
        Debug.Log($"parentRect:{parentRect}");
        return parentRect;

    }
    public Note GetNearestNoteByAction(string actionName, float currentTime, float maxJudgementTime)
    {
        int laneIndex = GetLaneIndexFromAction(actionName);
        if (laneIndex < 0) return null;

        Note nearest = null;
        float closest = maxJudgementTime;

        foreach (var note in activeNotes)
        {
            if (note.IsHit || note.LaneNumber != laneIndex) continue;

            float diff = Mathf.Abs(note.SpawnTime - currentTime);
            if (diff < closest)
            {
                closest = diff;
                nearest = note;
            }
        }
        return nearest;
    }

    private int GetLaneIndexFromAction(string actionName)
    {
        if (actionName.StartsWith("Lane"))
        {
            string numberStr = actionName.Substring("Lane".Length);
            if (int.TryParse(numberStr, out int num))
            {
                int index = num - 1;
                if (index >= 0 && index < scrollConfig.LaneCount)
                    return index;
            }
        }
        Debug.LogWarning($"[NoteManager] 無効なアクション名: {actionName}");
        return -1;
    }

    public int TotalNoteCount => chartData?.Notes?.Length ?? 0;

 
    public Vector2 GetLanePosition(int lane)
    {
        float totalWidth = parentRect.rect.width;
        float laneWidth = totalWidth / scrollConfig.LaneCount;
        float startX = -totalWidth / 2f + laneWidth / 2f + lane * laneWidth;
        return new Vector2(startX, scrollConfig.EndY);
    }

}
