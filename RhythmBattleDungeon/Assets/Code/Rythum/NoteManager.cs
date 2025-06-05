using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : SingletonMonoBehaviour<NoteManager>
{
    [SerializeField] private NoteUIPool notePool;
    [SerializeField] private List<Image> laneImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> laneLabels = new List<TextMeshProUGUI>();

    [Header("▼ レーン生成用プレハブ設定")]
    [Tooltip("レーンの背景画像プレハブ (Image付き)")]
    [SerializeField] private GameObject laneImagePrefab;

    [Tooltip("レーンラベルのプレハブ (TextMeshPro付き)")]
    [SerializeField] private GameObject laneLabelPrefab;

    [Tooltip("生成したレーン要素の親になる RectTransform")]
    [SerializeField] private RectTransform laneContainer;

    [SerializeField] private RectTransform parentRect;


    private List<Note> pendingNotes = new List<Note>();
    private List<Note> activeNotes = new List<Note>();
    private Dictionary<Note, NoteUI> noteToUIMap = new Dictionary<Note, NoteUI>();

    private ChartData chartData;
    private bool canSpawnNotes = false;

    private NoteScrollConfig scrollConfig;

    private LaneVisualConfig laneVisualConfig;

    private NoteTimingConfig noteTimingConfig;
    private JudgementConfig missJudgementConfig;

    private bool notesSpawned = false;
    private bool isInitialized = false;

    public bool NotesSpawned => notesSpawned;
    public bool IsInitialized => isInitialized;



    public event Action OnNotesSpawned;

    public event Action OnInitialized; // ← 新しいイベント



    private void CreateLanesAndLabels()
    {
        laneImages.Clear();
        laneLabels.Clear();

        int laneCount = laneVisualConfig.LaneCount;
        float laneWidth = laneVisualConfig.LaneWidth;
        float laneHeight = laneVisualConfig.LaneHeight;
        float totalWidth = parentRect.rect.width;
        float startX = -totalWidth / 2f + laneWidth / 2f;

        for (int i = 0; i < laneCount; i++)
        {
            float posX = startX + i * laneWidth;

            // Lane Image
            GameObject laneGO = Instantiate(laneImagePrefab, laneContainer);
            RectTransform laneRT = laneGO.GetComponent<RectTransform>();
            laneRT.anchoredPosition = new Vector2(posX, noteTimingConfig.EndY);
            laneRT.sizeDelta = new Vector2(laneWidth, laneHeight);

            Image laneImage = laneGO.GetComponent<Image>();
            laneImage.sprite = laneVisualConfig.GetLaneSprite(i);
            laneImage.color = laneVisualConfig.GetLaneColor(i);
            laneImages.Add(laneImage);

            // Lane Label
            if (laneLabelPrefab != null)
            {
                GameObject labelGO = Instantiate(laneLabelPrefab, laneContainer);
                RectTransform labelRT = labelGO.GetComponent<RectTransform>();
                labelRT.anchoredPosition = new Vector2(posX, noteTimingConfig.EndY - laneVisualConfig.LaneLabelYOffset);
                labelRT.sizeDelta = laneVisualConfig.LaneLabelSize;

                TextMeshProUGUI labelText = labelGO.GetComponent<TextMeshProUGUI>();
                labelText.text = string.Format(laneVisualConfig.LaneLabelFormat, i + 1);
                laneLabels.Add(labelText);
            }
        }
    }
    public void Initialize()
    {
        StartCoroutine(InitializeRoutine());
    }
    public void AllowNoteSpawning() => canSpawnNotes = true;



    private IEnumerator InitializeRoutine()
    {
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

        scrollConfig = StageManager.Instance.GetCurrentStageConfig()?.ScrollConfig;
        laneVisualConfig = scrollConfig.GetLaneVisualConfig();
        noteTimingConfig = scrollConfig.GetNoteTimingConfig();
        missJudgementConfig = JudgementManager.Instance.GetMissJudgement();

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

        CreateLanesAndLabels();

        pendingNotes = chartData.Notes.ToList();
        isInitialized = true;
        OnInitialized?.Invoke();
    }

    private void OnDestroy()
    {
        // クリーンアップ処理
        if (noteToUIMap != null)
        {
            noteToUIMap.Clear();
        }

        pendingNotes?.Clear();
        activeNotes?.Clear();

        // フラグをリセット
        notesSpawned = false;
        isInitialized = false;
        canSpawnNotes = false;
    }

    // シーン変更時の初期化リセット
    public void ResetForNewScene()
    {
        // 状態をリセット
        notesSpawned = false;
        isInitialized = false;
        canSpawnNotes = false;

        // リストをクリア
        pendingNotes?.Clear();
        activeNotes?.Clear();
        noteToUIMap?.Clear();

        // 必要に応じてシリアライズフィールドの再取得
        RefreshSerializedReferences();
    }

    private void RefreshSerializedReferences()
    {
        // シリアライズフィールドが null の場合、再取得を試みる
        if (notePool == null)
        {
            notePool = FindAnyObjectByType<NoteUIPool>();
        }

        var parentGO = GameObject.Find("Noteparent"); // 明示的に子の GameObject 名
        if (parentGO != null)
        {
            parentRect = parentGO.GetComponent<RectTransform>();
            Debug.Log("[NoteManager] parentRect 再取得成功: " + parentRect.name);
        }
        else
        {
            Debug.LogError("NoteParent が見つかりませんでした");
        }

       
    }

    private void Update()
    {
        if (!isInitialized || !canSpawnNotes) return;

        float currentTime = AudioManager.Instance.GetCurrentBGMTime();
        SpawnNotesIfNeeded(currentTime);
        CheckMissNotes(currentTime);
      
    }

    /// <summary>
    /// 時間になったノーツを出す（Updateから呼ばれる）
    /// </summary>
    private void SpawnNotesIfNeeded(float currentTime)
    {
        // NOTE: pendingNotes は SpawnTime が昇順で並んでいる前提
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
            Debug.Log($"[NoteManager] ActiveNotes.Count: {activeNotes.Count}, TotalNoteCount: {TotalNoteCount}");

            OnNotesSpawned?.Invoke();
        }
        
    }

    public void SpawnNote(Note noteData)
    {
        int lane = noteData.LaneNumber;
        float offsetTime = noteData.SpawnTime;

        if (lane < 0 || lane >= laneVisualConfig.LaneCount)
        {
            Debug.LogWarning($"[NoteManager] 無効なレーン番号: {lane}");
            return;
        }

        float totalWidth = parentRect.rect.width;
        float laneWidth = laneVisualConfig.LaneWidth;
        float parentOffsetX = parentRect.anchoredPosition.x; // 216

        // レーン内での相対位置を計算
        float relativeStartX = -totalWidth / 2f + laneWidth / 2f + lane * laneWidth;

        // parentRect のオフセットは考慮しない（親の座標系内で計算）
        Vector2 startPos = new Vector2(relativeStartX, noteTimingConfig.StartY);
        Vector2 endPos = new Vector2(relativeStartX, noteTimingConfig.EndY);
        var noteUI = notePool.Get();
        noteUI.transform.SetParent(parentRect, false);

        noteUI.GetComponent<RectTransform>().anchoredPosition = startPos;



        // noteData を NoteUI に渡す
        noteUI.Setup(offsetTime, scrollConfig.GetNoteTimingConfig().ScrollDuration, startPos, endPos, noteData);
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
                if (index >= 0 && index <laneVisualConfig.LaneCount)
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
        float laneWidth = totalWidth / laneVisualConfig.LaneCount;
        float startX = -totalWidth / 2f + laneWidth / 2f + lane * laneWidth;
        return new Vector2(startX, noteTimingConfig.EndY);
    }

}
