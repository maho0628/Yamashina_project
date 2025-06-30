 #if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Reflection;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(AnimatorStateDropdownAttribute))]
public class AnimatorStateDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        AnimatorStateDropdownAttribute dropdownAttr = (AnimatorStateDropdownAttribute)attribute;

        // アタッチされたクラスのインスタンス
        Object target = property.serializedObject.targetObject;

        // AnimatorControllerを取得
        var controllerField = target.GetType().GetField(dropdownAttr.ControllerFieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var controller = controllerField?.GetValue(target) as AnimatorController;

        if (controller == null)
        {
            EditorGUI.LabelField(position, label.text, "AnimatorController が見つかりません");
            return;
        }

        // ステート名リストを取得
        List<string> stateNames = new List<string>();
        foreach (var layer in controller.layers)
        {
            foreach (var state in layer.stateMachine.states)
            {
                stateNames.Add(state.state.name);
            }
        }

        // 現在の値のインデックスを取得
        int index = Mathf.Max(0, stateNames.IndexOf(property.stringValue));

        // Dropdown表示
        index = EditorGUI.Popup(position, label.text, index, stateNames.ToArray());

        // 値を更新
        property.stringValue = stateNames[index];
    }
}
#endif