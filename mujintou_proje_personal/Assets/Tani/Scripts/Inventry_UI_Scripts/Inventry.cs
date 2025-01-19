using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Inventry : SlotManager
{
        


}

#if UNITY_EDITOR
[CustomEditor(typeof(SlotManager))]
class InventryInspector : Editor
{
    SerializedProperty property_slot_editable;

    bool use_slotdata_maintaining = true;
    private void OnEnable()
    {
        var manager = target as SlotManager;
        property_slot_editable = serializedObject.FindProperty("slot_data_editable");

    }

    public override void OnInspectorGUI()
    {
        //  base.OnInspectorGUI();
        serializedObject.Update();

        var manager = target as SlotManager;
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.PropertyField(property_slot_editable, new GUIContent("�f�[�^��ҏW"));
            if (check.changed)
            {
                serializedObject.ApplyModifiedProperties();
                if (!property_slot_editable.boolValue)
                {
                    manager.SlotReconstruct();
                }
                serializedObject.Update();

            }

        }



        EditorGUI.BeginDisabledGroup(!property_slot_editable.boolValue);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("data"), new GUIContent("�X���b�g�Q�̃f�[�^"));

        EditorGUI.EndDisabledGroup();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Slots_Main"), new GUIContent("�\���̐؂�ւ���"));

        use_slotdata_maintaining = EditorGUILayout.Toggle("�A�C�e���f�[�^��ێ�����", use_slotdata_maintaining);
        if (use_slotdata_maintaining)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fileName"), new GUIContent("�f�[�^�ۑ���"));

        }


        serializedObject.ApplyModifiedProperties();

    }
}


#endif
