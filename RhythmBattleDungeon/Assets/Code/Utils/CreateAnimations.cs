using UnityEngine;
using UnityEditor;

public class AnimationClipCreator
{
    [MenuItem("Tools/Create Simple Animations")]
    public static void CreateAnimations()
    {
        string animationsFolder = "Assets/Art/Animations";

        // フォルダがなければ作成
        if (!AssetDatabase.IsValidFolder(animationsFolder))
        {
            AssetDatabase.CreateFolder("Assets", "Animations");
        }
        // Idle: 何もしないクリップ（1フレーム）
        AnimationClip idleClip = new AnimationClip();
        idleClip.name = "Idle";
        idleClip.frameRate = 30;
        // ループ設定
        AnimationClipSettings idleSettings = AnimationUtility.GetAnimationClipSettings(idleClip);
        idleSettings.loopTime = true;
        AnimationUtility.SetAnimationClipSettings(idleClip, idleSettings);

        AssetDatabase.CreateAsset(idleClip, animationsFolder + "/Idle.anim");

        // Pose: 腕を少し上げるアニメ（1秒）
        AnimationClip poseClip = new AnimationClip();
        poseClip.name = "Pose";
        poseClip.frameRate = 30;

        // 例として腕ボーンの回転アニメーションを追加（"Arm.R"はボーン名に合わせてください）
        AnimationCurve curveX = AnimationCurve.Linear(0f, 0f, 1f, 30f);  // 0° → 30°
        poseClip.SetCurve("Arm.R", typeof(Transform), "localEulerAngles.x", curveX);

        AnimationClipSettings poseSettings = AnimationUtility.GetAnimationClipSettings(poseClip);
        poseSettings.loopTime = false;
        AnimationUtility.SetAnimationClipSettings(poseClip, poseSettings);

        AssetDatabase.CreateAsset(poseClip, "Assets/Animations/Pose.anim");

        // Jump: Y位置を上下させる簡単ジャンプ
        AnimationClip jumpClip = new AnimationClip();
        jumpClip.name = "Jump";
        jumpClip.frameRate = 30;

        AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0f, 0f, 0.5f, 1f);
        jumpCurve.AddKey(1f, 0f);
        jumpClip.SetCurve("", typeof(Transform), "localPosition.y", jumpCurve);

        AnimationClipSettings jumpSettings = AnimationUtility.GetAnimationClipSettings(jumpClip);
        jumpSettings.loopTime = false;
        AnimationUtility.SetAnimationClipSettings(jumpClip, jumpSettings);

        AssetDatabase.CreateAsset(jumpClip, "Assets/Animations/Jump.anim");

        AssetDatabase.SaveAssets();

        Debug.Log("Animations created successfully.");
    }
}
