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




    [SerializeField, Tooltip("生成したレーン要素の親になる RectTransform")] private RectTransform laneContainer;

    private List<Note> pendingNotes = new List<Note>();
    private List<Note> activeNotes = new List<Note>();
    private Dictionary<Note, NoteUI>noteToUIMap  = new Dictionary<Note, NoteUI>();

    private ChartData chartData;
    private bool canSpawnNotes = false;

    private NoteScrollConfig scrollConfig;
    private LaneVisualConfig laneVisualConfig;
    private NoteTimingConfig noteTimingConfig;
    private KeyLabelConfig keyLabelConfig;
    private JudgementConfig missJudgementConfig;

    private bool notesSpawned = false;
    private bool isInitialized = false;

    public bool NotesSpawned => notesSpawned;
    public bool IsInitialized => isInitialized;

    public event Action OnNotesSpawned;
    public event Action OnInitialized;

    public void Initialize()
    {
        StartCoroutine(InitializeRoutine());
    }

    private IEnumerator InitializeRoutine()
    {
        if (!StageManager.Instance.IsStageSelected)
        {
            var testTable = Resources.Load<StageConfigTable>("ScriptableObject/stageConfig");
            StageManager.Instance.SetupStage(testTable, "test");
        }

        string chartFileName = StageManager.Instance.GetCurrentChartFileName();
        if (string.IsNullOrEmpty(chartFileName)) yield break;

        scrollConfig = StageManager.Instance.GetCurrentStageConfig()?.ScrollConfig;
        if (scrollConfig == null) yield break;

        laneVisualConfig = scrollConfig.GetLaneVisualConfig();
        noteTimingConfig = scrollConfig.GetNoteTimingConfig();
        missJudgementConfig = JudgementManager.Instance.GetMissJudgement();
        keyLabelConfig = scrollConfig.GetKeyLabelConfig();
        chartData = ChartJsonLoader.LoadChartData(chartFileName);
        if (chartData == null) yield break;

        CreateLanesAndLabels();

        pendingNotes = chartData.Notes.ToList();
        isInitialized = true;
        OnInitialized?.Invoke();
    }

    private void CreateLanesAndLabels()
    {
       

        int laneCount = laneVisualConfig.LaneCount;
        float laneWidth = laneVisualConfig.LaneWidth;
        float laneHeight = laneVisualConfig.LaneHeight;
        float totalWidth = laneContainer.rect.width;
        float startX = -totalWidth / 2f + laneWidth / 2f;

        for (int i = 0; i < laneCount; i++)
        {
            float posX = startX + i * laneWidth;

            GameObject laneImagePrefab = Instantiate(laneVisualConfig.LaneImagePrefab, laneContainer);
            RectTransform laneRT = laneImagePrefab.GetComponent<RectTransform>();
            laneRT.anchoredPosition = new Vector2(posX, noteTimingConfig.EndY);
            laneRT.sizeDelta = new Vector2(laneWidth, laneHeight);

            Image laneImage = laneImagePrefab.GetComponent<Image>();
            laneImage.sprite = laneVisualConfig.GetLaneSprite(i);
            laneImage.color = laneVisualConfig.GetLaneColor(i);

            if (laneVisualConfig.LaneLabelPrefab != null)
            {
                GameObject laneLabelPrefab = Instantiate(laneVisualConfig.LaneLabelPrefab, laneContainer);
                RectTransform labelRT = laneLabelPrefab.GetComponent<RectTransform>();
                labelRT.anchoredPosition = new Vector2(posX, noteTimingConfig.EndY);
                labelRT.sizeDelta = laneVisualConfig.LaneLabelSize;

                TextMeshProUGUI labelText = laneLabelPrefab.GetComponent<TextMeshProUGUI>();
                labelText.text = string.Format(keyLabelConfig.KeyLabels[i]);
                labelText.fontSize = keyLabelConfig.FontSize;
                labelText.color = keyLabelConfig.FontColor;
                labelText.font = keyLabelConfig.FontAsset;
                labelText.alignment = keyLabelConfig.Alignment;
            }
        }
    }

    private void Update()
    {
        if (!isInitialized || !canSpawnNotes) return;

        float currentTime = AudioManager.Instance.GetCurrentBGMTime();
        SpawnNotesIfNeeded(currentTime);
        CheckMissNotes(currentTime);
    }

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

    public void SpawnNote(Note noteData)
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

    public Vector2 GetLanePosition(int lane)
    {
        float totalWidth = laneContainer.rect.width;
        float laneWidth = totalWidth / laneVisualConfig.LaneCount;
        float posX = -totalWidth / 2f + laneWidth / 2f + lane * laneWidth;
        return new Vector2(posX, noteTimingConfig.EndY);
    }

    public RectTransform GetNoteParentTransform()
    {
        return laneContainer;
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
                if (index >= 0 && index < laneVisualConfig.LaneCount)
                    return index;
            }
        }
        return -1;
    }

    public int TotalNoteCount => chartData?.Notes?.Length ?? 0;

    public void AllowNoteSpawning() => canSpawnNotes = true;

    public void ResetForNewScene()
    {
        notesSpawned = false;
        isInitialized = false;
        canSpawnNotes = false;
        pendingNotes?.Clear();
        activeNotes?.Clear();
        noteToUIMap?.Clear();

        RefreshSerializedReferences();
    }

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

    private void OnDestroy()
    {
        noteToUIMap?.Clear();
        pendingNotes?.Clear();
        activeNotes?.Clear();
        notesSpawned = false;
        isInitialized = false;
        canSpawnNotes = false;
    }
}
