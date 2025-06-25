#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextAnimationConfig))]
public class TextAnimationConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TextAnimationConfig config = (TextAnimationConfig)target;

        if (GUILayout.Button("▶ Preview Animation"))
        {
            TextAnimationPreviewer.Preview(config);
        }

        if (GUILayout.Button("✖ Clear Preview"))
        {
            TextAnimationPreviewer.ClearPreview();
        }
    }
}
#endif
