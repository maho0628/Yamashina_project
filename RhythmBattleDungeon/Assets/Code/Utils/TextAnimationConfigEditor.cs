using UnityEditor;
using UnityEngine;

/// <summary>
/// TextAnimationConfigを編集する際に使用するカスタムエディタ
/// </summary>
[CustomEditor(typeof(TextAnimationConfig))]
public class TextAnimationConfigEditor : Editor
{
    /// <summary>
    /// インスペクターの内容を書き換えるための関数
    /// </summary>
    public override void OnInspectorGUI()
    {
        
        // 元のインスペクターを表示
        base.OnInspectorGUI();

        // 説明文を表示（親切なラベル）
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("設定を変更した後、アニメーションのプレビューで確認できます。\n「▶」ボタンで再生、「✖」で停止します。", MessageType.Info);

        GUILayout.Space(16);

        //ボタンを横に並べ始める
        EditorGUILayout.BeginHorizontal();

        //プレビュー再生ボタン
        if (GUILayout.Button("▶ アニメーションをプレビュー", GUILayout.Height(30)))
        {
            TextAnimationPreviewer.Preview((TextAnimationConfig)target);
        }
        
        //プレビューを消すボタン
        if (GUILayout.Button("✖ プレビューをクリア", GUILayout.Height(30)))
        {
            TextAnimationPreviewer.ClearPreview();
        }

        //ボタンを横に並べる処理終了
        EditorGUILayout.EndHorizontal();
    }
}
