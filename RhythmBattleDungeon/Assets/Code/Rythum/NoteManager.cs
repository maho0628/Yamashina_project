using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }

    [SerializeField] private UIObjectPool<NoteUI> notePool;
    [SerializeField] private Transform noteParent;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnNote(Note noteData)
    {
        var noteUI = notePool.Get();
        noteUI.transform.SetParent(noteParent, false);
        noteUI.Pool = notePool;
        noteUI.Setup(noteData);
    }
}
