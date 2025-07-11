#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer(typeof(AnimatorStateDropdownAttribute))]
public class AnimatorStateDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        AnimatorStateDropdownAttribute dropdownAttr = (AnimatorStateDropdownAttribute)attribute;

        // AnimatorControllerを取得
        RuntimeAnimatorController runtimeController = GetAnimatorController(property, dropdownAttr.ControllerFieldName);

        if (runtimeController == null)
        {
            EditorGUI.LabelField(position, label.text, "AnimatorController が見つかりません");
            return;
        }

        // AnimatorControllerに変換
        AnimatorController controller = runtimeController as AnimatorController;
        if (controller == null && runtimeController is AnimatorOverrideController overrideController)
        {
            controller = overrideController.runtimeAnimatorController as AnimatorController;
        }

        if (controller == null)
        {
            EditorGUI.LabelField(position, label.text, "AnimatorController の取得に失敗しました");
            return;
        }

        // ステート名リストを取得
        List<string> stateNames = new List<string>();
        stateNames.Add("None"); // デフォルト選択肢を追加

        foreach (var layer in controller.layers)
        {
            foreach (var state in layer.stateMachine.states)
            {
                stateNames.Add(state.state.name);
            }
        }

        if (stateNames.Count <= 1)
        {
            EditorGUI.LabelField(position, label.text, "ステートが見つかりません");
            return;
        }

        // 現在の値のインデックスを取得
        int index = Mathf.Max(0, stateNames.IndexOf(property.stringValue));

        // Dropdown表示
        int newIndex = EditorGUI.Popup(position, label.text, index, stateNames.ToArray());

        // 値を更新
        if (newIndex != index && newIndex < stateNames.Count)
        {
            property.stringValue = newIndex == 0 ? "" : stateNames[newIndex];
        }
    }

    private RuntimeAnimatorController GetAnimatorController(SerializedProperty property, string controllerFieldName)
    {
        // まず、直接のターゲットオブジェクトから探す
        Object targetObject = property.serializedObject.targetObject;
        RuntimeAnimatorController controller = FindControllerInObject(targetObject, controllerFieldName);

        if (controller != null)
            return controller;

        // 見つからない場合、プロパティパスから親オブジェクトを探す
        string[] pathParts = property.propertyPath.Split('.');
        SerializedProperty currentProperty = property.serializedObject.GetIterator();

        // カスタム設定オブジェクトを探す
        foreach (string part in pathParts)
        {
            if (part == "customSettings")
            {
                SerializedProperty customSettingsProperty = property.serializedObject.FindProperty("customSettings");
                if (customSettingsProperty != null)
                {
                    SerializedProperty controllerProperty = customSettingsProperty.FindPropertyRelative("baseAnimatorController");
                    if (controllerProperty != null)
                    {
                        return controllerProperty.objectReferenceValue as RuntimeAnimatorController;
                    }
                }
                break;
            }
        }

        return null;
    }

    private RuntimeAnimatorController FindControllerInObject(Object obj, string fieldName)
    {
        if (obj == null) return null;

        // リフレクションでフィールドを探す
        FieldInfo field = obj.GetType().GetField(fieldName,
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        if (field != null)
        {
            return field.GetValue(obj) as RuntimeAnimatorController;
        }

        // プロパティも探す
        PropertyInfo property = obj.GetType().GetProperty(fieldName,
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        if (property != null)
        {
            return property.GetValue(obj) as RuntimeAnimatorController;
        }

        return null;
    }
}
#endif