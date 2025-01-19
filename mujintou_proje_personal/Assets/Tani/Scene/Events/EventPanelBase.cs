using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class EventPanelBase : MonoBehaviour
{
    //inspector field
    [SerializeField]
    Image event_view;
    [SerializeField]
    Text event_title;
    [SerializeField]
    Text event_text;
    [SerializeField]
    List<EventDatas> EventList;
    [SerializeField]
    Transform buttons_prefab_parent;
    [SerializeField]
    public EventDatas endEventData;


    //private field
    int probability_sum = 0;
    int current_event_scene_id = 0;


    TextControl event_text_control;
    UnityAction[] actions;
    private void Awake()
    {
        event_text_control = event_text.gameObject.GetComponent<TextControl>();



        actions = new UnityAction[] { SelectChoise0, SelectChoise1, SelectChoise2 };
    }

    void Start()
    {

        GameData.CanReturnTitle = false;
        PlayerInfo.Instance.SavePalyerData();
    }

    public void SetEvent(int index)
    {

    }
    //���X�g�̒��̃����_���ȃC�x���g�����s
    public void SetRandomEvent()
    {
        probability_sum = 0;
        int[] modifiedProbabilities = new int[EventList.Count];
        for (int i = 0; i < EventList.Count; i++)
        {
            int modi = (int)(EventList[i].probability * EventProbabilityMultiplier(EventList[i].scene_id));
            modifiedProbabilities[i] = modi;
            probability_sum += modi;
        }
        int random_int = Mathf.CeilToInt(Random.value * probability_sum);
        int range_min = 0;
        int range_max = 0;
        int index = 0;
        while (true)
        {
            range_min = range_max;
            range_max += modifiedProbabilities[index];
            if (random_int > range_min && random_int <= range_max)
            {
                break;
            }
            index++;
        }

        StartEvent(EventList[index]);
    }

    public enum EventCallBackType { Normal, Custom };
    public void StartEvent(EventDatas data)
    {
        if (!data)
        {
            Debug.LogError("Event data is null");
            return;
        }
        if (!CanEventExcute(data.scene_id))
        {
            SetRandomEvent();
            return;
        }



        event_text_control.EndEvent.RemoveAllListeners();
        event_text_control.ClickEventAfterTextsEnd.RemoveAllListeners();








        //UI��K�p
        current_event_scene_id = data.scene_id;
        event_view.sprite = data.event_view_sprite;
        // �^�C�g���ƃ��C���e�L�X�g�̃��[�J���C�Y
        data.event_title.GetLocalizedStringAsync().Completed += (asyncOperation) =>
        {
            event_title.text = asyncOperation.Result;
        };
        Debug.Log(data.main_text);
        Debug.Log(event_text.text);
        data.main_text.GetLocalizedStringAsync().Completed += (asyncOperation) =>
        {
            Debug.LogWarning("Result :" + asyncOperation.Result);
            event_text.text = asyncOperation.Result;
        };

        event_text_control.ResetTextData();
        string load_text_Event = data.main_text.GetLocalizedString();
        string[] split_text_Event = load_text_Event.Split('\n');
        foreach (var text in split_text_Event)
        {
            if (text == "") continue;
            event_text_control.AddTextData(text.Replace("**", "\n"));
        }

        // �C�x���g�I����̃{�^���\��
        event_text_control.EndEvent.AddListener(ShowButtons);

        if (buttons_prefab_parent.childCount > 0)
        {
            DestroyImmediate(buttons_prefab_parent.GetChild(0).gameObject);
        }
        var buttons = Instantiate(data.buttons_prefab);
        buttons.transform.SetParent(buttons_prefab_parent, false); // �e�I�u�W�F�N�g�ɒǉ�����ۂɃ��[���h�|�W�V���������Z�b�g
        //buttons.transform.localPosition = Vector3.zero; // ���[�J���|�W�V������(0,0,0)�ɐݒ�
        //buttons.transform.localScale = Vector3.one; // ���[�J���X�P�[����(1,1,1)�ɐݒ�
        //RectTransform rectTransform = buttons.GetComponent<RectTransform>();
        //rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        //rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        //rectTransform.pivot = new Vector2(0.5f, 0.5f);
        //rectTransform.anchoredPosition = Vector2.zero;
        //rectTransform.localScale = Vector3.one;


        //�{�^���̃e�L�X�g��K�p
        if (buttons.transform.childCount != data.results.Count)
        {
            Debug.LogError($"Mismatch between button count ({buttons.transform.childCount}) and results count ({data.results.Count})");
            return; // �G���[�����������ꍇ�A�����𒆒f
        }



        for (int i = 0; i < buttons.transform.childCount; i++)
        {
            var buttonTransform = buttons.transform.GetChild(i);

            // �{�^���̎q�I�u�W�F�N�g����1�ڂ� Text �R���|�[�l���g���擾
            var buttonTextComponent = buttonTransform.GetChild(0).GetComponent<Text>();


            var choiceResult = data.results[i];
            string choiceText = choiceResult.choise_text.GetLocalizedString(); // ���[�J���C�Y���ꂽ�e�L�X�g���擾

            // �{�^����1�ڂ̃e�L�X�g��ݒ�
            buttonTextComponent.text = choiceText;
            string currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;

                // �{�^���̎q�I�u�W�F�N�g����2�ڂ� Text �R���|�[�l���g���擾
                if (buttonTransform.childCount >= 2)
            {
                var actionValueTextComponent = buttonTransform.GetChild(1).GetComponent<Text>();

                if (currentLocale == "ja")
                {
                    // �{�^����2�ڂ̃e�L�X�g�� required_action_value ��ݒ�
                    actionValueTextComponent.text = $"�s���l: {choiceResult.required_action_value}";
                }
                else if (currentLocale == "en")
                {
                    actionValueTextComponent.text = $"ActionValue: {choiceResult.required_action_value}";

                }
            }

            // �{�^���̃A�N�V�����ݒ�
            var button = buttonTransform.GetComponent<Button>();
            if (data.callBackType == EventCallBackType.Normal)
            {
                button.onClick.AddListener(actions[i]);
            }

            button.interactable = PlayerInfo.Instance.ActionValue >= choiceResult.required_action_value;
        }
        //for (int i = 0; i < buttons.transform.childCount; i++)
        //{
        //    var EndEventchoice = endEventData.results[i];

        //    string choiceText_End = EndEventchoice.choise_text.GetLocalizedString(); // ���[�J���C�Y���ꂽ�e�L�X�g���擾
        //    Debug.Log(choiceText_End);
        //    endEventData.buttons_prefab.transform.GetChild(0).GetComponent<Text>().text = choiceText_End;
        //}

        //�e�L�X�g���I���܂Ń{�^�����\����
        buttons_prefab_parent.gameObject.SetActive(false);


    }

    float EventProbabilityMultiplier(int scene_id)
    {
        PlayerInfo info = PlayerInfo.Instance;
        switch (scene_id)
        {
            case 3:
                if (PlayerInfo.Instance.Day.day >= 10)
                {
                    return 1.5f;
                }
                return 1;
            case 1000:
                return info.weather == PlayerInfo.Weather.Sunny ? 1 : 0;
            case 1100:
                return info.weather == PlayerInfo.Weather.Rainy ? 1 : 0;
            case 1200:
                return info.weather == PlayerInfo.Weather.Cloudy ? 1 : 0;
            case 1005:
                if (info.Day.day >= 10 && info.Thirst <= 20)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case 1011:
                if (info.Day.day >= 20 && info.Thirst <= 50)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case 1010:
                return info.weather == PlayerInfo.Weather.Rainy ? 1 : 0;
            case 1004:
                return info.weather == PlayerInfo.Weather.Sunny ? 1 : 0;
            case 3005:
                return info.weather == PlayerInfo.Weather.Rainy ? 1 : 0;
            case 3010:
                return info.weather == PlayerInfo.Weather.Rainy ? 1 : 0;
            case 4011:
                return info.weather == PlayerInfo.Weather.Rainy ? 1 : 0;
            case 4013:
                return info.weather == PlayerInfo.Weather.Rainy ? 1 : 0;
            case 4012:
                return info.weather == PlayerInfo.Weather.Sunny ? 1 : 0;
            case 6010:
                return info.weather == PlayerInfo.Weather.Rainy ? 1 : 0;
            case 9901:
                if (PlayerInfo.Instance.ActionValue == 5)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case 9902:
                if (PlayerInfo.Instance.ActionValue == 4)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case 9903:
                if (PlayerInfo.Instance.ActionValue == 3)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case 9904:
                if (PlayerInfo.Instance.ActionValue <= 2)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }




            default:
                return 1;
        }
    }

    bool CanEventExcute(int scene_id)
    {
        switch (scene_id)
        {
            case 3:
                return PlayerInfo.Instance.Health >= 50;
            default:
                return true;
        }
    }





    public void ShowButtons()
    {

        buttons_prefab_parent.gameObject.SetActive(true);
        event_text_control.EndEvent.RemoveListener(ShowButtons);

    }



    public void ChoiseSelected(int index)
    {
        PlayerInfo.Instance.SetInventryLock(true);
        Destroy(buttons_prefab_parent.gameObject.transform.GetChild(0).gameObject);
        buttons_prefab_parent.gameObject.SetActive(false);
        event_text_control.EndEvent.RemoveAllListeners();
        ChoiseResult result;

        result = EventList.Find(n => n.scene_id == current_event_scene_id).results[index];




        event_text_control.ResetTextData();
        string load_text_Event;
        string[] split_text_Event;
        load_text_Event = result.result_text.GetLocalizedString();
        Debug.LogWarning("LoadTextEvent :" + load_text_Event);
        split_text_Event = load_text_Event.Split(char.Parse("\n"));
        foreach (var text in split_text_Event)
        {
            if (text == "") continue;
            event_text_control.AddTextData(text.Replace("**", "\n"));
        }


        PlayerInfo info = PlayerInfo.Instance;
        //�X�e�[�^�X�̑�����\��������

        var prev_health = info.Health;
        var prev_hunger = info.Hunger;
        var prev_thirst = info.Thirst;
        var prev_action = info.ActionValue;

        int health_change = Mathf.RoundToInt(Random.Range(result.health_change_min, result.health_change_max));
        int hunger_change = Mathf.RoundToInt(Random.Range(result.hunger_change_min, result.hunger_change_max));
        int thirst_change = Mathf.RoundToInt(Random.Range(result.thirst_change_min, result.thirst_change_max));


        info.Health += health_change;
        info.Hunger += hunger_change;
        info.Thirst += thirst_change;
        info.ActionValue -= result.required_action_value;

        string status_change =
            $"�̗́@ : {prev_health} �� {info.Health}\n" +
            $"���� �@: {prev_thirst} �� {info.Thirst}\n" +
            $"�H�� �@: {prev_hunger} �� {info.Hunger}\n" +
            $"�s���l : {prev_action} �� {info.ActionValue}";
        event_text_control.AddTextData(status_change);




        if (!info.IsPlayerConditionEqualTo(PlayerInfo.Condition.Poisoned))
        {

            if (!info.IsPlayerConditionEqualTo(PlayerInfo.Condition.Hungry))
            {
                if (!info.IsPlayerConditionEqualTo(PlayerInfo.Condition.Thirsty))
                {
                    goto NoSpecificCondition;
                }
            }
        }

        string conditionChangeText = "";

        int prev_poison_health = info.Health;
        int after_poison_health = (int)(info.IsPlayerConditionEqualTo(PlayerInfo.Condition.Poisoned) ? prev_poison_health * 0.8f : prev_poison_health);

        if (info.IsPlayerConditionEqualTo(PlayerInfo.Condition.Poisoned))
        {
            conditionChangeText += $"��: �̗�{info.Health} �� {(int)(info.Health * 0.8f)}\n";
            info.Health = (int)(info.Health * 0.8f);
        }

        if (info.IsPlayerConditionEqualTo(PlayerInfo.Condition.Hungry))
        {

            conditionChangeText += $"�H�� : �̗�{info.Health} �� {Mathf.Clamp(info.Health - (((100 - info.Hunger) / 4) - 19), 0, 100)}\n";
            info.Health = Mathf.Clamp(info.Health - (((100 - info.Hunger) / 4) - 19), 0, 100);
        }

        if (info.IsPlayerConditionEqualTo(PlayerInfo.Condition.Thirsty))
        {
            conditionChangeText += $"���� : �H��{info.Hunger} �� {Mathf.Clamp(info.Hunger - (((100 - info.Thirst) / 4) - 19), 0, 100)}\n";
            info.Hunger = Mathf.Clamp(info.Hunger - (((100 - info.Thirst) / 4) - 19), 0, 100);
        }

        event_text_control.AddTextData(conditionChangeText);



    NoSpecificCondition:

        //�l�����ʂ�\��������
        if (result.Gain_Items.Count != 0)
        {


            foreach (var n in result.Gain_Items)
            {
            string result_text_Item="";
                //���̃A�C�e�����擾�ł��邩
                bool bGet = Random.value <= n.probability;
                if (bGet)
                {
                    //�����l���ł��邩
                    int num = n.range_min + Mathf.CeilToInt(Random.value * (n.range_max - n.range_min));
                    if (PlayerInfo.Instance.Inventry.GetNullSlot())
                    {
                        Debug.Log(PlayerInfo.Instance.Inventry.GetItemName(n.id));
                        string currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;
                        if (currentLocale == "en")
                        {
                            result_text_Item += "The " + PlayerInfo.Instance.Inventry.GetItemName(n.id) + $" is**obtained by the �g{num}�h! \n";
                        }
                        else if (currentLocale == "ja") {
                            result_text_Item += PlayerInfo.Instance.Inventry.GetItemName(n.id) + $"��{num}�l��!\n";

                        }
                        // result_text_Item = result.result_text.GetLocalizedString();
                        string[] split_text_Event_Item;

                        Debug.LogWarning("LoadTextEvent :" + result_text_Item);
                        split_text_Event_Item = result_text_Item.Split(char.Parse("\n"));
                        int a = 0;
                        foreach (var text in split_text_Event_Item)
                        {
                            if (text == "") continue;
                            Debug.Log($"{result_text_Item}/{a}");a++;

                            event_text_control.AddTextData(text.Replace("**", "\n"));
                        }
                       
                        event_text_control.EndEvent.AddListener(() => info.Inventry.GetItem(n.id, num, true));
                    }
                    else
                    {
                        string currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;
                        if (currentLocale == "en")
                        {
                            result_text_Item += "I couldn't hold" + PlayerInfo.Instance.Inventry.GetItemName(n.id) + $"{num} pieces!\n";
                        }
                        else if (currentLocale == "ja")
                        {
                            result_text_Item += PlayerInfo.Instance.Inventry.GetItemName(n.id) + $"{num}�͎����؂�Ȃ�����!\n";

                        }
                        string[] split_text_Event_Item;
                        Debug.Log(result_text_Item);
                        Debug.LogWarning("LoadTextEvent :" + result_text_Item);
                        split_text_Event_Item = result_text_Item.Split(char.Parse("\n"));
                        foreach (var text in split_text_Event_Item)
                        {
                            if (text == "") continue;
                            Debug.Log(result_text_Item);

                            event_text_control.AddTextData(text.Replace("**", "\n"));
                        }
                    } 
    

                }
            }
        }
        PlayerInfo.Instance.SavePalyerData();


        event_text_control.ClickEventAfterTextsEnd.AddListener(ONEndEvent);


    }

    void ONEndEvent()
    {
        event_text_control.ClickEventAfterTextsEnd.RemoveListener(ONEndEvent);
        PlayerInfo.Instance.SetInventryLock(false);
        PlayerInfo.Instance.CheckPlayerDeath();
        StartEvent(endEventData);
    }


    void SelectChoise0() { ChoiseSelected(0); }
    void SelectChoise1() { ChoiseSelected(1); }
    void SelectChoise2() { ChoiseSelected(2); }




}








