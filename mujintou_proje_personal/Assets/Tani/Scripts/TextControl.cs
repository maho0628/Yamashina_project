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
    //山品追加分

    [SerializeField] public anotherSoundPlayer soundPlayer;

    public UnityEvent ClickEventAfterTextsEnd { get; private set; } = new UnityEvent();

    Text text;
    TextGenerator generator;
    BackLog backLog;
    public enum TextInputType
    {
        None, DefaultText, DirectList, TextAsset, EventData
    }
    int str_range = 0;//何文字目まで表示するか
    int str_page = 0;//strsのindex
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
        //山品追加分

        soundPlayer = GameObject.FindAnyObjectByType<anotherSoundPlayer>().GetComponent<anotherSoundPlayer>(); if (use_back_log_text) { BackLogValidiate(); }


        default_font_size = text.fontSize;
        text_size = new Vector2(text.rectTransform.rect.width, text.rectTransform.rect.height);
        if (default_font_size < min_font_size)
        {
            //デフォルト値がminより小さい場合デフォルトをminにminをデフォルトに
            (default_font_size, min_font_size) = (min_font_size, default_font_size);
        }

        //テキストをクリックしたときにOnClick関数が呼ばれるようにする設定
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

        // デバッグログを追加してリスナーが追加されたことを確認
        Debug.Log("ClickEventAfterTextsEnd listener added in Start");

    }


    virtual protected void Update()
    {
        if (strs.Count == 0) return;
        if (!is_first_font_updated && auto_font_size)
        {
            UpdateFontSize(0, default_font_size);//初回フォントサイズの決定、以後はページ変わるごと
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
            //再帰関数としての使用
            UpdateFontSize(page, newSize - (default_font_size - min_font_size) / Partition_Num);
        }
        else
        {
            text.text = "";
            return;//テキストすべて表示可能なFontサイズ

        }

        text.text = "";
    }
    public void OnClick()
    {
        // Resetした後にClick
        if (strs.Count == 0) return;

        //文章が終わっていないときは文章を全て表示
        if (!is_one_sentence_end)
        {
            str_range = strs[str_page].Length;
            UpdateText(str_range);
        }
        else
        {



            //文章が終わっているときは次のページ
            if (use_back_log_text) backLog.AddTextToBackLog(strs[str_page]);//バックログに追加
            str_page++;
            if (str_page < strs.Count)
            {
                is_one_sentence_end = false;
                str_range = 0;
                time_sum = 0;
                UpdateFontSize(str_page, default_font_size);
                //山品追加分

                soundPlayer.ChooseSongs_SE(2);
            }
            else//次のページがなければ文章はそのまま、
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

        // 次のテキストがあるか確認し、ある場合は表示を更新する
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


        EditorGUILayout.LabelField(new GUIContent("文字送りの間隔"), EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("interval"));


        GUILayout.Space(5);
        EditorGUILayout.LabelField("フォントサイズ関連", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(property_auto_font_size, new GUIContent("自動調整の使用"));
        if (property_auto_font_size.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("min_font_size"), new GUIContent("最小文字サイズ"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Partition_Num"), new GUIContent("サイズ幅の分割数", "大きいほど処理は重い"));
        }
        else
        {
            GUILayout.Space(40);
        }


        GUILayout.Space(10);
        EditorGUILayout.LabelField(new GUIContent("文字送りする文字列"), EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("use_default_text"), new GUIContent("デフォルトテキストの使用","テキストコンポーネントのテキスト"));
        EditorGUILayout.PropertyField(property_inputType, new GUIContent("入力形式"));
        switch (property_inputType.enumValueIndex)
        {
            case (int)TextControl.TextInputType.DefaultText:
                EditorGUILayout.LabelField(new GUIContent("Textコンポーネントのテキストのみを使用"), EditorStyles.boldLabel);
                break;
            case (int)TextControl.TextInputType.DirectList:
                EditorGUILayout.LabelField(new GUIContent("リストに直接文字列を入力"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("strs"), new GUIContent("リスト"));
                break;
            case (int)TextControl.TextInputType.TextAsset:
                EditorGUILayout.LabelField(new GUIContent("テキストアセットの使用"), EditorStyles.boldLabel);
                EditorGUILayout.LabelField(new GUIContent("文字列は改行で区切られる"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("textAsset"));
                break;
            case (int)TextControl.TextInputType.EventData:
                EditorGUILayout.LabelField(new GUIContent("EventData(ScriptableObject)の使用"), EditorStyles.boldLabel);
                EditorGUILayout.LabelField(new GUIContent("テキスト本文が使われる"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("eventData"));
                break;
        }


        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        GUILayout.Space(5);
        EditorGUILayout.PropertyField(property_use_end_event, new GUIContent("エンドイベントの使用"));
        if (property_use_end_event.boolValue)
        {
            EditorGUILayout.LabelField(new GUIContent("ここに登録された関数がテキスト終了時に一回呼ばれます"), EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("EndEvent"));

        }


        EditorGUILayout.LabelField(new GUIContent("バックログ"), EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(property_use_back_log_text, new GUIContent("バックログを使用する"));
        if (property_use_back_log_text.boolValue)
        {
            var control = target as TextControl;
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("backLogObject"), new GUIContent("シーン上のバックログ"));
                if (check.changed)
                {
                    // 一旦反映させて、無効なバックログでないかチェック
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