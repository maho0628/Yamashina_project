using UnityEngine;

public class NoteUI : MonoBehaviour
{
    private Note noteData;
    public UIObjectPool<NoteUI> Pool { get; set; }

    public void Setup(Note data)
    {
        noteData = data;
        // 表示や位置初期化など
    }

    public void Deactivate()
    {
        Pool?.Return(this);
    }

    private void Update()
    {
        // 移動処理など
        if (/* 判定エリア通過など */ false)
        {
            Deactivate();
        }
    }
}
