using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static TextCustomSettings;
using System.Linq;

/// <summary>
/// TextAnimationConfigを編集する際に使用するカスタムエディタ
/// </summary>
[CustomEditor(typeof(TextAnimationConfig))]
public class TextAnimationConfigEditor : Editor
{
    private TextAnimationConfig config;

    private void OnEnable()
    {
        config = (TextAnimationConfig)target;
    }

    /// <summary>
    /// 基本設定の描画
    /// </summary>
    private void DrawBasicSettings()
    {
        EditorGUILayout.LabelField("Text Animation Config", EditorStyles.boldLabel);
        GUILayout.Space(4);

        // 基本設定のプロパティを表示
        var basicProperty = serializedObject.FindProperty("basicSettings");
        Debug.Assert(basicProperty != null);
        // アニメーションテキスト
        EditorGUILayout.PropertyField(basicProperty.FindPropertyRelative("displayText"));

        // テキストカラー
        EditorGUILayout.PropertyField(basicProperty.FindPropertyRelative("textColor"));

        // フォントサイズ
        EditorGUILayout.PropertyField(basicProperty.FindPropertyRelative("fontSize"));

        // フォントアセット
        EditorGUILayout.PropertyField(basicProperty.FindPropertyRelative("fontAsset"));

        // フォントスタイル
        EditorGUILayout.PropertyField(basicProperty.FindPropertyRelative("displayFontStyles"));

        GUILayout.Space(8);
    }

    /// <summary>
    /// タイミング設定の描画
    /// </summary>
    private void DrawTimingSettings()
    {
        EditorGUILayout.LabelField("Timing Settings", EditorStyles.boldLabel);
        GUILayout.Space(4);

        var timingProperty = serializedObject.FindProperty("timingSettings");
        if (timingProperty != null)
        {
            EditorGUILayout.PropertyField(timingProperty, true);
        }

        GUILayout.Space(8);
    }

    /// <summary>
    /// スケール設定の描画
    /// </summary>
    private void DrawScaleSettings()
    {
        EditorGUILayout.LabelField("Scale Settings", EditorStyles.boldLabel);
        GUILayout.Space(4);

        var scaleProperty = serializedObject.FindProperty("scaleSettings");
        if (scaleProperty != null)
        {
            EditorGUILayout.PropertyField(scaleProperty, true);
        }

        GUILayout.Space(8);
    }

    /// <summary>
    /// パンチ設定の描画
    /// </summary>
    private void DrawPunchSettings()
    {
        EditorGUILayout.LabelField("Punch Settings", EditorStyles.boldLabel);
        GUILayout.Space(4);

        var punchProperty = serializedObject.FindProperty("punchSettings");
        if (punchProperty != null)
        {
            EditorGUILayout.PropertyField(punchProperty, true);
        }

        GUILayout.Space(8);
    }

    /// <summary>
    /// レイアウト設定の描画
    /// </summary>
    private void DrawLayoutSettings()
    {
        EditorGUILayout.LabelField("Layout Settings", EditorStyles.boldLabel);
        GUILayout.Space(4);

        var layoutProperty = serializedObject.FindProperty("layoutSettings");
        if (layoutProperty != null)
        {
            EditorGUILayout.PropertyField(layoutProperty, true);
        }

        GUILayout.Space(8);
    }

    /// <summary>
    /// バリデーション設定の描画
    /// </summary>
    private void DrawValidationSettings()
    {
        EditorGUILayout.LabelField("Validation Settings", EditorStyles.boldLabel);
        GUILayout.Space(4);

        var validationProperty = serializedObject.FindProperty("validationSettings");
        if (validationProperty != null)
        {
            EditorGUILayout.PropertyField(validationProperty, true);
        }

        GUILayout.Space(8);
    }

    /// <summary>
    /// 参考情報の描画
    /// </summary>
    private void DrawReferenceInfo()
    {
        EditorGUILayout.LabelField("Reference Info", EditorStyles.boldLabel);
        GUILayout.Space(4);

        var totalDurationProperty = serializedObject.FindProperty("totalDuration");
        var durationCheckProperty = serializedObject.FindProperty("durationCheck");

        if (totalDurationProperty != null)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(totalDurationProperty);
            EditorGUI.EndDisabledGroup();
        }

        if (durationCheckProperty != null)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(durationCheckProperty);
            EditorGUI.EndDisabledGroup();
        }

        GUILayout.Space(8);
    }

    /// <summary>
    /// カスタム設定の描画
    /// </summary>
    private void DrawCustomSettings()
    {
        EditorGUILayout.LabelField("Custom Settings", EditorStyles.boldLabel);
        GUILayout.Space(4);

        // SerializedPropertyを使ってカスタム設定にアクセス
        var customProperty = serializedObject.FindProperty("customSettings");
        if (customProperty != null)
        {
            EditorGUILayout.PropertyField(customProperty, true);
        }

        GUILayout.Space(8);
    }

    /// <summary>
    /// プレビューボタンの描画
    /// </summary>
    private void DrawPreviewButtons()
    {
        EditorGUILayout.HelpBox("設定を変更した後、アニメーションのプレビューで確認できます。\n「▶」ボタンで再生、「✖」で停止します。", MessageType.Info);

        GUILayout.Space(8);

        // ボタンを横に並べ始める
        EditorGUILayout.BeginHorizontal();

        // プレビュー再生ボタン
        if (GUILayout.Button("▶ アニメーションをプレビュー", GUILayout.Height(30)))
        {
            TextAnimationPreviewer.Preview((TextAnimationConfig)target);
        }

        // プレビューを消すボタン
        if (GUILayout.Button("✖ プレビューをクリア", GUILayout.Height(30)))
        {
            TextAnimationPreviewer.ClearPreview();
        }

        // ボタンを横に並べる処理終了
        EditorGUILayout.EndHorizontal();
    }
    /// <summary>
    /// アニメーション設定の描画
    /// </summary>
    /// </summary>
    private void DrawAnimationSettings(SerializedProperty animationTypeProperty)
    {
        EditorGUILayout.LabelField("Animation Settings", EditorStyles.boldLabel);
        GUILayout.Space(4);

        // アニメーションタイプ
        if (animationTypeProperty != null)
        {
            EditorGUILayout.PropertyField(animationTypeProperty);
        }

        // 背景画像
        var backgroundProperty = serializedObject.FindProperty("backGroundImage");
        if (backgroundProperty != null)
        {
            EditorGUILayout.PropertyField(backgroundProperty);
        }

        GUILayout.Space(8);
    }

    /// <summary>
    /// インスペクターの内容を書き換えるための関数
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 基本設定は常に表示
        DrawBasicSettings();

       
        // アニメーションタイプをSerializedPropertyから直接取得
        var animationTypeProperty = serializedObject.FindProperty("animationType");
        // アニメーション設定を表示
        DrawAnimationSettings(animationTypeProperty);
        // null チェックを追加
        if (animationTypeProperty == null)
        {
            EditorGUILayout.HelpBox("animationType プロパティが見つかりません。", MessageType.Error);
            return;
        }
        var animationType = (AnimationType)animationTypeProperty.enumValueIndex;

        // 通常時（Custom以外）は以下の設定を表示
        if (animationType != AnimationType.Custom)
        {
            DrawTimingSettings();
            DrawScaleSettings();
            DrawPunchSettings();
            DrawLayoutSettings();
            DrawValidationSettings();
            DrawReferenceInfo();
        }
        // Custom時は全ての設定を表示
        else
        {
            DrawTimingSettings();
            DrawScaleSettings();
            DrawPunchSettings();
            DrawCustomSettings();
            DrawLayoutSettings();
            DrawValidationSettings();
            DrawReferenceInfo();
        }

        // プレビューボタンは常に表示
        DrawPreviewButtons();

        // 変更を保存
        if (GUI.changed)
        {
            EditorUtility.SetDirty(config);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
