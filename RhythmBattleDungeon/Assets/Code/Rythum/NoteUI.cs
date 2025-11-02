using UnityEngine;

/// <summary>
/// ノーツ一個一個のオブジェクトプールつきクラス
/// ノーツの初期設定反映やノーツが消えるまでの処理を行う
/// </summary>
public class NoteUI : MonoBehaviour, IPoolable<NoteUI>
{
    /// <summary>
    /// ノーツの位置情報（レクトトランスフォーム型)
    /// </summary>
    [SerializeField, Tooltip("ノーツの位置")]
    private RectTransform noteRectTransform;

    /// <summary>
    /// ノーツが判定ラインに到達する予定時刻
    /// </summary>
    private float targetTime;

    /// <summary>
    /// ノーツがスクロールして到達するまでの時間
    /// </summary>
    private float scrollDuration;

    /// <summary>
    /// ノーツの開始位置（RectTransform のアンカー座標）
    /// </summary>
    private Vector2 startPosition;

    /// <summary>
    /// ノーツの終了位置（RectTransform のアンカー座標）
    /// </summary>
    private Vector2 endPosition;

    //生成するノーツ（音符）のデータ
    private Note linkedNote;

    //ノーツのオブジェクトプール
    private UIObjectPool<NoteUI> noteUIPool;

    /// <summary>
    /// ノーツの基本情報を初期化し、開始位置を設定する関数。
    /// </summary>
    /// <param name="targetTime">ノーツが判定ラインに到達する予定時刻</param>
    /// <param name="scrollDuration">ノーツがスクロールして到達するまでの時間</param>
    /// <param name="startPos">ノーツの開始位置（RectTransform のアンカー座標）</param>
    /// <param name="endPos">ノーツの終了位置（RectTransform のアンカー座標）</param>
    /// <param name="note">生成するノーツ（音符）のデータ</param>
    public void Setup(float targetTime, float scrollDuration, Vector2 startPos, Vector2 endPos, Note note)
    {

        this.targetTime = targetTime;
        this.scrollDuration = scrollDuration;
        this.startPosition = startPos;
        this.endPosition = endPos;
        this.linkedNote = note;

        // ノートの初期位置を設定（スクロール開始地点に配置）
        noteRectTransform.anchoredPosition = startPos;
    }

    /// <summary>
    /// プールに戻す関数
    /// </summary>
    private void Deactivate()
    {
        noteUIPool?.Return(this);
        linkedNote = null;
    }


    /// <summary>
    /// オブジェクトプールから作成された際に呼ばれる関数
    /// </summary>
    /// <param name="pool"></param>
    public void OnCreated(UIObjectPool<NoteUI> pool)
    {
        this.noteUIPool = pool;
    }

    private void Update()
    {
        float currentBgmTime = AudioManager.Instance.GetCurrentBGMTime();
        float spawnAt = Mathf.Max(0f, targetTime - scrollDuration);

        if (currentBgmTime < spawnAt)
        {
            return;
        }


        float t = Mathf.Clamp01((currentBgmTime - spawnAt) / scrollDuration);
        noteRectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);


        if (t >= 1f)
        {
            if (linkedNote != null)
                Deactivate();


        }
    }

}
