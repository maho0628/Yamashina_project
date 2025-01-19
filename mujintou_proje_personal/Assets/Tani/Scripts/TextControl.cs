using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(EventTrigger))]
public class TextControl : MonoBehaviour
{


    [SerializeField]
    float interval = 0.15f;
    [SerializeField]
    protected List<string> strs;
    [SerializeField]
    bool auto_font_size = true;
    [SerializeField]
    int min_font_size = 80;
    [SerializeField]
    int Partition_Num = 8;


    [SerializeField]
    TextInputType inputType = TextInputType.DefaultText;
    [SerializeField]
    TextAsset textAsset;
    [SerializeField]
    EventData eventData;

    [SerializeField]
    bool use_end_event = false;
    [SerializeField]
    public UnityEvent EndEvent;

    [SerializeField]
    bool use_back_log_text = true;
    [SerializeField]
    GameObject backLogObject;
    //�R�i�ǉ���

    [SerializeField] public anotherSoundPlayer soundPlayer;

    public UnityEvent ClickEventAfterTextsEnd { get; private set; } = new UnityEvent();

    Text text;
    TextGenerator generator;
    BackLog backLog;
    public enum TextInputType
    {
        None, DefaultText, DirectList, TextAsset, EventData
    }
    int str_range = 0;//�������ڂ܂ŕ\�����邩
    int str_page = 0;//strs��index
    int default_font_size;
    float time_sum = 0;
    bool is_one_sentence_end = false;
    bool is_all_texts_end = false;
    bool is_first_font_updated = false;
    Vector2 text_size;


    virtual protected void Start()
    {
        text = GetComponent<Text>();
        generator = new TextGenerator();
        //�R�i�ǉ���

        soundPlayer = GameObject.FindAnyObjectByType<anotherSoundPlayer>().GetComponent<anotherSoundPlayer>(); if (use_back_log_text) { BackLogValidiate(); }


        default_font_size = text.fontSize;
        text_size = new Vector2(text.rectTransform.rect.width, text.rectTransform.rect.height);
        if (default_font_size < min_font_size)
        {
            //�f�t�H���g�l��min��菬�����ꍇ�f�t�H���g��min��min���f�t�H���g��
            (default_font_size, min_font_size) = (min_font_size, default_font_size);
        }

        //�e�L�X�g���N���b�N�����Ƃ���OnClick�֐����Ă΂��悤�ɂ���ݒ�
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnClick(); });
        eventTrigger.triggers.Add(entry);


        switch (inputType)
        {
            case TextInputType.None:
                //strs.Clear();
                //text.text = "";
                //is_one_sentence_end = true;
                break;
            case TextInputType.DefaultText:
                strs.Clear();
                strs.Add(text.text);
                break;
            case TextInputType.TextAsset:
                strs.Clear();
                string load_text;
                string[] split_text;

                if (!textAsset) return;
                load_text = textAsset.text;
                split_text = load_text.Split(char.Parse("\n"));
                foreach (var n in split_text)
                {
                    n.Replace("**", "\n");

                    if (n == "") continue;
                    strs.Add(n);
                }
                break;
            case TextInputType.EventData:
                strs.Clear();
                strs.Add(eventData.Main_Text);
        

                break;
        }

        ClickEventAfterTextsEnd.AddListener(OnClickAfterTextsEnd);

        // �f�o�b�O���O��ǉ����ă��X�i�[���ǉ����ꂽ���Ƃ��m�F
        Debug.Log("ClickEventAfterTextsEnd listener added in Start");

    }


    virtual protected void Update()
    {
        if (strs.Count == 0) return;
        if (!is_first_font_updated && auto_font_size)
        {
            UpdateFontSize(0, default_font_size);//����t�H���g�T�C�Y�̌���A�Ȍ�̓y�[�W�ς�邲��
            is_first_font_updated = true;
        }


        if (is_one_sentence_end) return;

        time_sum += Time.deltaTime;
        if (time_sum > interval)
        {

            time_sum = 0;
            str_range++;
            UpdateText(str_range);

        }
    }

    void UpdateText(int count)
    {
        text.text = strs[str_page][..count];
        //Debug.Log("count" + ..count);
        //Debug.Log("str_page" + str_page);
        //Debug.Log("strs[str_page]" + strs[str_page]);
        if (str_range >= strs[str_page].Length)
        {
            is_one_sentence_end = true;
            OnOneSentenceEnd();
            if (str_page >= strs.Count - 1)
            {
                is_all_texts_end = true;
                OnAllTextsEnd();
            }
        }
    }

    void UpdateFontSize(int page, int newSize)
    {
        if (!auto_font_size) return;

        generator.Invalidate();
        text.fontSize = newSize;
        text.text = strs[str_page];

        TextGenerationSettings settings
            = text.GetGenerationSettings(text_size);
        generator.Populate(text.text, settings);


        if (generator.characterCountVisible != strs[page].Length)
        {
            if (newSize < min_font_size)
            {
                text.fontSize = min_font_size;
                Debug.LogError("text Overflow");
                return;
            }
            //�ċA�֐��Ƃ��Ă̎g�p
            UpdateFontSize(page, newSize - (default_font_size - min_font_size) / Partition_Num);
        }
        else
        {
            text.text = "";
            return;//�e�L�X�g���ׂĕ\���\��Font�T�C�Y

        }

        text.text = "";
    }
    public void OnClick()
    {
        // Reset�������Click
        if (strs.Count == 0) return;

        //���͂��I����Ă��Ȃ��Ƃ��͕��͂�S�ĕ\��
        if (!is_one_sentence_end)
        {
            str_range = strs[str_page].Length;
            UpdateText(str_range);
        }
        else
        {



            //���͂��I����Ă���Ƃ��͎��̃y�[�W
            if (use_back_log_text) backLog.AddTextToBackLog(strs[str_page]);//�o�b�N���O�ɒǉ�
            str_page++;
            if (str_page < strs.Count)
            {
                is_one_sentence_end = false;
                str_range = 0;
                time_sum = 0;
                UpdateFontSize(str_page, default_font_size);
                //�R�i�ǉ���

                soundPlayer.ChooseSongs_SE(2);
            }
            else//���̃y�[�W���Ȃ���Ε��͂͂��̂܂܁A
            {

                str_page = strs.Count - 1;
                OnClickedAfterAllTextsEnd();

                //if (use_end_event) OnTextEnd();
            }
        }
    }
    virtual protected void OnAllTextsEnd()
    {
        Debug.Log("OnAllTextsEnd called");

        if (use_end_event) EndEvent.Invoke();
    }

    virtual protected void OnOneSentenceEnd()
    {
        Debug.Log("OnOneSentenceEnd called");

    }
    virtual protected void OnClickedAfterAllTextsEnd()
    {
        Debug.Log("OnClickedAfterAllTextsEnd called");
        ClickEventAfterTextsEnd.Invoke();
    }
    void OnClickAfterTextsEnd()
    {
        Debug.Log("OnClickAfterTextsEnd invoked");

        // ���̃e�L�X�g�����邩�m�F���A����ꍇ�͕\�����X�V����
        if (str_page < strs.Count - 1)
        {
            str_page++;
            str_range = 0;
            is_one_sentence_end = false;
            time_sum = 0;
            UpdateFontSize(str_page, default_font_size);
            UpdateText(str_range);
        }
        else
        {
            Debug.Log("All texts have been displayed.");
        }
    }

    public void BackLogValidiate()
    {


        if (!backLogObject)
        {
            Debug.LogError("BackLogObject is not set");
        }
        if (!backLogObject.TryGetComponent<BackLog>(out backLog))
        {
            backLogObject = null;
            Debug.LogError("BackLogObject has no BackLogComponent");
        }
    }

    public void ResetTextData()
    {
        strs.Clear();
        str_range = 0;
        str_page = 0;
        is_first_font_updated = false;
        is_one_sentence_end = false;
        is_all_texts_end = false;
    }
    public void AddTextData(string add_text)
    {
        strs.Add(add_text);
    }

    public bool GetIsTextEnd()
    {
        return is_all_texts_end;
    }

}


#if UNITY_EDITOR
[CustomEditor(typeof(TextControl))]
class TextControlInspector : Editor
{
    SerializedProperty property_auto_font_size;
    SerializedProperty property_inputType;
    SerializedProperty property_use_back_log_text;
    SerializedProperty property_use_end_event;


    private void OnEnable()
    {
        property_auto_font_size = serializedObject.FindProperty("auto_font_size");
        property_inputType = serializedObject.FindProperty("inputType");
        property_use_back_log_text = serializedObject.FindProperty("use_back_log_text");
        property_use_end_event = serializedObject.FindProperty("use_end_event");
    }


    public override void OnInspectorGUI()
    {
        // base.OnInspectorGUI();
        serializedObject.Update();


        EditorGUILayout.LabelField(new GUIContent("��������̊Ԋu"), EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("interval"));


        GUILayout.Space(5);
        EditorGUILayout.LabelField("�t�H���g�T�C�Y�֘A", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(property_auto_font_size, new GUIContent("���������̎g�p"));
        if (property_auto_font_size.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("min_font_size"), new GUIContent("�ŏ������T�C�Y"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Partition_Num"), new GUIContent("�T�C�Y���̕�����", "�傫���قǏ����͏d��"));
        }
        else
        {
            GUILayout.Space(40);
        }


        GUILayout.Space(10);
        EditorGUILayout.LabelField(new GUIContent("�������肷�镶����"), EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("use_default_text"), new GUIContent("�f�t�H���g�e�L�X�g�̎g�p","�e�L�X�g�R���|�[�l���g�̃e�L�X�g"));
        EditorGUILayout.PropertyField(property_inputType, new GUIContent("���͌`��"));
        switch (property_inputType.enumValueIndex)
        {
            case (int)TextControl.TextInputType.DefaultText:
                EditorGUILayout.LabelField(new GUIContent("Text�R���|�[�l���g�̃e�L�X�g�݂̂��g�p"), EditorStyles.boldLabel);
                break;
            case (int)TextControl.TextInputType.DirectList:
                EditorGUILayout.LabelField(new GUIContent("���X�g�ɒ��ڕ���������"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("strs"), new GUIContent("���X�g"));
                break;
            case (int)TextControl.TextInputType.TextAsset:
                EditorGUILayout.LabelField(new GUIContent("�e�L�X�g�A�Z�b�g�̎g�p"), EditorStyles.boldLabel);
                EditorGUILayout.LabelField(new GUIContent("������͉��s�ŋ�؂���"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("textAsset"));
                break;
            case (int)TextControl.TextInputType.EventData:
                EditorGUILayout.LabelField(new GUIContent("EventData(ScriptableObject)�̎g�p"), EditorStyles.boldLabel);
                EditorGUILayout.LabelField(new GUIContent("�e�L�X�g�{�����g����"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("eventData"));
                break;
        }


        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        GUILayout.Space(5);
        EditorGUILayout.PropertyField(property_use_end_event, new GUIContent("�G���h�C�x���g�̎g�p"));
        if (property_use_end_event.boolValue)
        {
            EditorGUILayout.LabelField(new GUIContent("�����ɓo�^���ꂽ�֐����e�L�X�g�I�����Ɉ��Ă΂�܂�"), EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("EndEvent"));

        }


        EditorGUILayout.LabelField(new GUIContent("�o�b�N���O"), EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(property_use_back_log_text, new GUIContent("�o�b�N���O���g�p����"));
        if (property_use_back_log_text.boolValue)
        {
            var control = target as TextControl;
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("backLogObject"), new GUIContent("�V�[����̃o�b�N���O"));
                if (check.changed)
                {
                    // ��U���f�����āA�����ȃo�b�N���O�łȂ����`�F�b�N
                    serializedObject.ApplyModifiedProperties();
                    control.BackLogValidiate();
                    serializedObject.Update();
                }
            }
        }


        serializedObject.ApplyModifiedProperties();
    }
}
#endif