using UnityEngine;

public class NoteInputManager : MonoBehaviour
{
    private NoteManager noteManager;

    private NoteScrollConfig config;

    private void Start()
    {
        noteManager = NoteManager.Instance;
        config = GameInitializer.Instance.GetNoteScrollConfig();
    }

    //    private void Update()
    //    {
    //        // 例えばキーボードの1〜4キーで4レーンを操作する例
    //        for (int laneId = 0; laneId < 4; laneId++)
    //        {
    //            if (Input.GetKeyDown(KeyCode.Alpha1 + laneId))
    //            {
    //                TryHitNote(laneId);
    //            }
    //        }
    //    }

    //    private void TryHitNote(int laneId)
    //    {
    //        float currentTime = (float)(AudioSettings.dspTime - AudioManager.Instance.GetCurrentBGMStartDSPTime());
    //        Note nearestNote = noteManager.GetNearestNoteInLane(laneId, currentTime);

    //        if (nearestNote != null)
    //        {
    //            nearestNote.IsHit = true;
    //            Debug.Log($"Hit! Lane: {laneId} Time: {nearestNote.SpawnTime}");
    //            // ヒット時の処理（スコア加算、エフェクト表示など）
    //        }
    //        else
    //        {
    //            Debug.Log("Miss!");
    //        }
    //    }
}
