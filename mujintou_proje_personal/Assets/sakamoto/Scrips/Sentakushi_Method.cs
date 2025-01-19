
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Sentakushi_Method : Event_Text
{
    [SerializeField] Text _sentakusiTextObject1;
    [SerializeField] Text _sentakusiTextObject2;
    [SerializeField] Text _sentakusiTextObject3;

    //[SerializeField] TextMeshProUGUI _eventTextObject;
    [SerializeField] GameObject _sentakusi1;
    [SerializeField] GameObject _sentakusi2;
    [SerializeField] GameObject _sentakusi3;

    [SerializeField] GameObject shadow1;
    [SerializeField] GameObject shadow2;
    [SerializeField] GameObject shadow3;

    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject backHomeButton;

    [SerializeField] GameObject roadImage;

    [SerializeField] Fade fade;
    [SerializeField] effect effect;
    [SerializeField] string scene_Name;
    [SerializeField, Header("inventoryマネージャー取得してね")] InventoryManagerVer cpInventoryManager;
    [SerializeField] GetItemManager GetItemManager;
    [SerializeField, Header("一回の探索で何回までイベントを回せるか")] int MaxCanEvent;

    //[SerializeField] TextControl textControl;

    //public int DoEvent;//何回イベントをしたか
    public Event_Ilast_Disply event_Ilast_Disply;
    public Event_BG_Disply event_BG_Disply;
    public Event_Text event_Text;
    public Event_Manage event_manage;
    public multiAudio multiAudio;
    int[] Result_tam;
    int Result_Num;
    bool SceneContinue;
    bool GoToLoadScene;
    int next_num_tnp;//ひとつ前のイベントナンバー

    int Event_num;
    // Start is called before the first frame update

    private void Start()
    {
        //cpInventoryManager = GameObject.Find("InventorySlotManager").GetComponent<InventoryManagerVer>();
        //GetItemManager = GameObject.Find("AllItemSpawnSystem").GetComponent<GetItemManager>();
        Event_num = 0;
        //DoEvent = 0;
        Debug.Log(event_manage.start_event_num);
        Set_Sentakusi_Words(event_manage.eventDatas[event_manage.start_event_num].Sentakusi1, 
                            event_manage.eventDatas[event_manage.start_event_num].Sentakusi2, 
                            event_manage.eventDatas[event_manage.start_event_num].Cancel);
        _sentakusiSetActive(event_manage.start_event_num);
        SceneContinue = false;
        GoToLoadScene = false;
        allShadow(false);

    }
    private void Update()
    {
        if (GoToLoadScene)
        {              

            if (Input.GetMouseButton(0))
            {
                if (!(PlayerInfo.Instance.Health == 0))
                {
                    textControl.ResetTextData();
                    textControl.AddTextData("探索を続けますか？");
                    _sentakusi1.SetActive(false);
                    _sentakusi2.SetActive(false);
                    _sentakusi3.SetActive(false);

                    continueButton.SetActive(true);
                    backHomeButton.SetActive(true);


                    if (Event_num >= MaxCanEvent || next_num_tnp / 1000 >= 10)
                    {
                        shadow1.SetActive(true);
                    }
                }
                else
                {
                    GoToEnding();
                }
                //Event_num++;
                //event_Text.SetEventText();
                //event_Ilast_Disply.SetEventIlast();
                //event_BG_Disply.SetEventBG();
                //Set_Sentakusi_Words(event_manage.eventDatas[event_manage.now_event_num].Sentakusi1,
                //                    event_manage.eventDatas[event_manage.now_event_num].Sentakusi2,
                //                    event_manage.eventDatas[event_manage.now_event_num].Cancel);

                //_sentakusiSetActive(event_Manage.now_event_num);
                //_sentakusi1.SetActive(true);
                //_sentakusi2.SetActive(true);
                //_sentakusi3.SetActive(true);
                //GoToLoadScene = false;
                //if(Event_num == 5)
                //{
                //    SceneManager.LoadScene(scene_Name);
                //}    

                //PlayerInfo.Instance.Health
            }
        }
    }
    public void Push_Sentakusi1()
    {
        if (GoToLoadScene == false)
        {
            textControl.ResetTextData();
            textControl.AddTextData
                /*Text_Disply*/(event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Result1);
            getItem(0);
            //_sentakusi1.SetActive(false);
            _sentakusi2.SetActive(false);
            _sentakusi3.SetActive(false);
            allShadow(false);

            GoToLoadScene = true;

            if(event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_health < 0)
            {
                Debug.Log("通った");
                effect.damage_f = true;
            }
            Debug.Log("変換前の体力は" + PlayerInfo.Instance.Health + "食料は" + PlayerInfo.Instance.Hunger + "水分は" + PlayerInfo.Instance.Thirst + "です");
            PlayerInfo.Instance.Health += event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_health;
            PlayerInfo.Instance.Hunger += event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_hunger;
            PlayerInfo.Instance.Thirst += event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_warter;
            Debug.Log("変換後の体力は" + PlayerInfo.Instance.Health + "食料は" + PlayerInfo.Instance.Hunger + "水分は" + PlayerInfo.Instance.Thirst + "です");

            if (PlayerInfo.Instance.Day.day <= event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_day || event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_day == 0)
            {
                next_num_tnp = event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Next_Ivent_ID;
            }
            else 
            {
                next_num_tnp = event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Next_Ivent_ID_2;
            }

            for (int i = 0; i < event_manage.eventDatas.Length; i++)
            {
                if (PlayerInfo.Instance.Day.day <= event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_day || event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_day == 0)
                {
                    if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Next_Ivent_ID == event_manage.eventDatas[i].Event_ID)
                    {
                        Debug.Log(event_manage.eventDatas[i].Event_ID);
                        event_manage.now_event_num = i;
                        Debug.Log(event_manage.eventDatas[event_manage.now_event_num].Event_ID);
                        break;
                    }
                }
                else 
                {
                    if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Next_Ivent_ID_2 == event_manage.eventDatas[i].Event_ID)
                    {
                        Debug.Log(event_manage.eventDatas[i].Event_ID);
                        event_manage.now_event_num = i;
                        Debug.Log(event_manage.eventDatas[event_manage.now_event_num].Event_ID);
                        break;
                    }
                }
            }

        }
    }

    public void Push_Sentakusi2()
    {
        if (GoToLoadScene == false)
        {
            textControl.ResetTextData();
            textControl.AddTextData(event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Result1);
            getItem(1);
            _sentakusi1.SetActive(false);
            _sentakusi3.SetActive(false);
            allShadow(false);

            GoToLoadScene = true;

            Debug.Log("変換前の体力は" + PlayerInfo.Instance.Health + "食料は" + PlayerInfo.Instance.Hunger + "水分は" + PlayerInfo.Instance.Thirst + "です");
            PlayerInfo.Instance.Health += event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_health;
            PlayerInfo.Instance.Hunger += event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_hunger;
            PlayerInfo.Instance.Thirst += event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_warter;
            Debug.Log("変換後の体力は" + PlayerInfo.Instance.Health + "食料は" + PlayerInfo.Instance.Hunger + "水分は" + PlayerInfo.Instance.Thirst + "です");

            if (PlayerInfo.Instance.Day.day <= event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_day || event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_day == 0)
            {
                next_num_tnp = event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Next_Ivent_ID;
            }
            else
            {
                next_num_tnp = event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Next_Ivent_ID_2;
            }

            for (int i = 0; i < event_manage.eventDatas.Length; i++)
            {
                if (PlayerInfo.Instance.Day.day <= event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_day || event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_day == 0)
                {
                    if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Next_Ivent_ID == event_manage.eventDatas[i].Event_ID)
                    {
                        Debug.Log(event_manage.eventDatas[i].Event_ID);
                        event_manage.now_event_num = i;
                        Debug.Log(event_manage.eventDatas[event_manage.now_event_num].Event_ID);
                        break;
                    }
                }
                else
                {
                    if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Next_Ivent_ID_2 == event_manage.eventDatas[i].Event_ID)
                    {
                        Debug.Log(event_manage.eventDatas[i].Event_ID);
                        event_manage.now_event_num = i;
                        Debug.Log(event_manage.eventDatas[event_manage.now_event_num].Event_ID);
                        break;
                    }
                }
            }

        }
    }

    public void Push_Sentakusi3()
    {
        if (GoToLoadScene == false)
        {
            textControl.ResetTextData();
            textControl.AddTextData(event_manage.eventDatas[event_manage.now_event_num].Cancel_Result);
            _sentakusi1.SetActive(false);
            _sentakusi2.SetActive(false);
            allShadow(false);

            GoToLoadScene = true;

            for (int i = 0; i < event_manage.eventDatas.Length; i++)
            {
                if (event_manage.eventDatas[event_manage.now_event_num].Cancel_Next_Ivent_ID == event_manage.eventDatas[i].Event_ID)
                {
                    Debug.Log(event_manage.eventDatas[i].Event_ID);
                    event_manage.now_event_num = i;
                    Debug.Log(event_manage.eventDatas[event_manage.now_event_num].Event_ID);
                    break;
                }
            }
        }
    }

    public void Set_Sentakusi_Words(string Words1, string Words2, string Words3)
    {
        _sentakusiTextObject1.text = Words1;
        _sentakusiTextObject2.text = Words2;
        _sentakusiTextObject3.text = Words3;
    }

    void _sentakusiSetActive(int event_num)
    {
        if (event_manage.eventDatas[event_num].Sentakusi1_Zyouken != 0)
        {
            for (int i = 0; i < event_manage.newItem.Length; i++)
            {
                if (event_manage.eventDatas[event_num].Sentakusi1_Zyouken == event_manage.newItem[i].ScriptalItem.itemID)
                {
                    Debug.Log("イベントIDは" + event_manage.newItem[i].ScriptalItem.itemID);
                    Debug.Log("条件の個数は" + event_manage.eventDatas[event_num].Sentakusi1_Zyouken_num);
                    Debug.Log("持っている個数は" + event_manage.newItem[i].CurrentStackCount);

                    if (event_manage.eventDatas[event_num].Sentakusi1_Zyouken_num <= event_manage.newItem[i].CurrentStackCount)
                    {

                        shadow1.SetActive(false);
                        break;
                    }
                    else
                    {
                        shadow1.SetActive(true);
                        break;
                    }
                }
            }
        }
        else
        {
            shadow1.SetActive(false);
        }
        if (event_manage.eventDatas[event_num].Sentakusi2_Zyouken != 0)
        {
            for (int i = 0; i < event_manage.newItem.Length; i++)
            {
                if (event_manage.eventDatas[event_num].Sentakusi2_Zyouken == event_manage.newItem[i].ScriptalItem.itemID)
                {
                    if (event_manage.eventDatas[event_num].Sentakusi2_Zyouken_num <= event_manage.newItem[i].CurrentStackCount)
                    {
                        shadow2.SetActive(false);
                    }
                    else
                    {
                        shadow2.SetActive(true);
                    }
                }
            }
        }
        else
        {
            shadow2.SetActive(false);
        }
        shadow3.SetActive(false);
    }

    void getItem(int Sentakusi)
    {
        if (Sentakusi == 0)
        {
            if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward1 != 0)
            {
                for (int i = 0; i < event_manage.newItem.Length; i++)
                {
                    if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward1 == event_manage.newItem[i].ScriptalItem.itemID)
                    {
                        Debug.Log(event_manage.newItem[i].ScriptalItem.name + "をゲットしました");
                        for (int j = 0; j < event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward1_num; j++)
                        {
                            Debug.Log("get");
                            event_manage.newItem[i].CurrentStackCount++;
                           
                            //if(Inventory.Instance.


                        }
                    }
                }
            }
            if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward2 != 0)
            {
                for (int i = 0; i < event_manage.newItem.Length; i++)
                {
                    if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward2 == event_manage.newItem[i].ScriptalItem.itemID)
                    {
                        Debug.Log(event_manage.newItem[i].ScriptalItem.name + "をゲットしました");
                        for (int j = 0; j < event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward2_num; j++)
                        {
                            Debug.Log("get");
                            event_manage.newItem[i].CurrentStackCount++;
                            if (!cpInventoryManager.GetIsItemFull())
                            {
                                GetItemManager.GetNewItem(event_manage.newItem[i].ScriptalItem.itemID);
                            }
                        }
                    }
                }
            }
            if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward3 != 0)
            {
                for (int i = 0; i < event_manage.newItem.Length; i++)
                {
                    if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward3 == event_manage.newItem[i].ScriptalItem.itemID)
                    {
                        Debug.Log(event_manage.newItem[i].ScriptalItem.name + "をゲットしました");
                        for (int j = 0; j < event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward3_num; j++)
                        {
                            Debug.Log("get");
                            //event_manage.newItem[i].CurrentStackCount++;
                            event_manage.newItem[i].CurrentStackCount++;
                            if (!cpInventoryManager.GetIsItemFull())
                            {
                                GetItemManager.GetNewItem(event_manage.newItem[i].ScriptalItem.itemID);
                            }
                        }
                    }
                }
            }
            
        }
        if (Sentakusi == 1)
        {
            if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward1 != 0)
            {
                for (int i = 0; i < event_manage.newItem.Length; i++)
                {
                    if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward1 == event_manage.newItem[i].ScriptalItem.itemID)
                    {
                        Debug.Log(event_manage.newItem[i].ScriptalItem.name + "をゲットしました");
                        for (int j = 0; j < event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward1_num; j++)
                        {
                            Debug.Log("get");
                            event_manage.newItem[i].CurrentStackCount++;
                            if (!cpInventoryManager.GetIsItemFull())
                            {
                                GetItemManager.GetNewItem(event_manage.newItem[i].ScriptalItem.itemID);
                            }
                        }
                    }
                }
            }
            if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward2 != 0)
            {
                for (int i = 0; i < event_manage.newItem.Length; i++)
                {
                    if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward2 == event_manage.newItem[i].ScriptalItem.itemID)
                    {
                        Debug.Log(event_manage.newItem[i].ScriptalItem.name + "をゲットしました");
                        for (int j = 0; j < event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward2_num; j++)
                        {
                            Debug.Log("get");
                            event_manage.newItem[i].CurrentStackCount++;
                            if (!cpInventoryManager.GetIsItemFull())
                            {
                                GetItemManager.GetNewItem(event_manage.newItem[i].ScriptalItem.itemID);
                            }
                        }
                    }
                }
            }
            if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward3 != 0)
            {
                for (int i = 0; i < event_manage.newItem.Length; i++)
                {
                    if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward3 == event_manage.newItem[i].ScriptalItem.itemID)
                    {
                        Debug.Log(event_manage.newItem[i].ScriptalItem.name + "をゲットしました");
                        for (int j = 0; j < event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward3_num; j++)
                        {
                            Debug.Log("get");
                            event_manage.newItem[i].CurrentStackCount++;
                            if (!cpInventoryManager.GetIsItemFull())
                            {
                                GetItemManager.GetNewItem(event_manage.newItem[i].ScriptalItem.itemID);
                            }
                        }
                    }
                }
            }
        }

    }

    public int Day_Branch(int day)
    {
        if(PlayerInfo.Instance.Day.day == day)
        {
            return event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Next_Ivent_ID;
        }
        else
        {
            return 0;
        }
    }

    public void GoToEnding()
    {
        Debug.Log(next_num_tnp);
        Debug.Log(next_num_tnp / 1000); 
        //バッドエンド
        if(PlayerInfo.Instance.Health == 0)
        {
            fade.scene_name_num = 1;
           fade.feadout_f = true;
            Debug.Log("バッドエンド");
            //SceneManager.LoadScene("BadEnd");
        }

        //トゥルーエンド
        else if (next_num_tnp / 1000 == 0)
        {
            fade.feadout_f = true;
            Debug.Log("トゥルーエンド");
            //SceneManager.LoadScene("TrueEnd");
        }

        else
        {
            //if (Event_num == 5)
            //{
            //    fade.scene_name_num = 2;
            //    fade.feadout_f = true;
            //}
            //else
            {
                Event_num++;
                event_Text.SetEventText();
                event_Ilast_Disply.SetEventIlast();
                event_BG_Disply.SetEventBG();
                Set_Sentakusi_Words(event_manage.eventDatas[event_manage.now_event_num].Sentakusi1,
                                    event_manage.eventDatas[event_manage.now_event_num].Sentakusi2,
                                    event_manage.eventDatas[event_manage.now_event_num].Cancel);

                _sentakusiSetActive(event_Manage.now_event_num);

                roadImage.SetActive(false);

                _sentakusi1.SetActive(true);
                _sentakusi2.SetActive(true);
                _sentakusi3.SetActive(true);

                continueButton.SetActive(false);
                backHomeButton.SetActive(false);
                GoToLoadScene = false;

            }

            //PlayerInfo.Instance.Health

        }
    }
    protected
       void allShadow(bool check)
       {
           shadow1.SetActive(check);
           shadow2.SetActive(check);
           shadow3.SetActive(check);
       }
}

//全てのシャドウのセットアクティブをオン・オフする
