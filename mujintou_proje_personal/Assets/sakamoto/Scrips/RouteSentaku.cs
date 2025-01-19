using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;


public class RouteSentaku : Event_Text
{
    [SerializeField] Button Route1;
    [SerializeField] Button Route2;
    [SerializeField] GameObject Route3;
    [SerializeField] GameObject Route1Image;
    [SerializeField] GameObject Route2Image;
    [SerializeField] GameObject BG_cover;
    [SerializeField] GameObject BG2;
    [SerializeField] GameObject Loadimage;
    [SerializeField] GameObject ItemPrefab;
    [SerializeField][Tooltip("とび先のシーン")] SceneObject scene;
    //入手したアイテムの種類
    int[] item_ID;
    //入手したアイテムの数
    int[] get_Num;
    //洞窟でどれだけ奥に進んだか
    int deep = 0;
    [SerializeField] float deep_Light;


    Color color;
    Color normalColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
    Color highlightColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    //[SerializeField, Header("inventoryマネージャー取得してね")] InventoryManagerVer cpInventoryManager;
    //[SerializeField] GetItemManager GetItemManager;

    public Event_BG_Disply event_BG_Disply;
    public Event_Text event_Text;
    public Event_Manage event_manage;

    int next_num_tnp;//次のイベントナンバー

    // Start is called before the first frame update

    //ChatGPT作
    void Start()
    {
        PlayerInfo.Instance.SavePalyerData();   
        //配列の初期化
        item_ID = new int[2];
        get_Num = new int[2];

        Route1.image.color = Route2.image.color = normalColor;
        Route1Image.SetActive(false);
        Route2Image.SetActive(false);

        //if (PlayerInfo.Instance.Inventry.GetItemAmount((Items.Item_ID)event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Zyouken) >= event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Zyouken_num || event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Zyouken_num == 0)
        //{
            // Route1にマウスオーバー時のイベントを追加
            EventTrigger trigger1 = Route1.gameObject.AddComponent<EventTrigger>();
            trigger1.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
            trigger1.triggers[0].callback.AddListener((data) => { OnRouteEnter(Route1); });
            trigger1.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
            trigger1.triggers[1].callback.AddListener((data) => { OnRouteExit(Route1); });
        //}
        //else
        //{
        //    Route1.interactable = false;
        //}

        //if (PlayerInfo.Instance.Inventry.GetItemAmount((Items.Item_ID)event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Zyouken) >= event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Zyouken_num || event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Zyouken_num == 0)
        //{
            // Route2にマウスオーバー時のイベントを追加
            EventTrigger trigger2 = Route2.gameObject.AddComponent<EventTrigger>();
            trigger2.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
            trigger2.triggers[0].callback.AddListener((data) => { OnRouteEnter(Route2); });
            trigger2.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
            trigger2.triggers[1].callback.AddListener((data) => { OnRouteExit(Route2); });
        //}
        //else
        //{
        //    Route2.interactable = false;
        //}

        Route3.SetActive(false);
    }

    void OnRouteEnter(Button button)
    {
        button.image.color = highlightColor;
        if(button == Route1)
        {
            Route1Image.SetActive(true);
        }
        if(button == Route2) 
        { 
            Route2Image.SetActive(true);
        }
    }

    void OnRouteExit(Button button)
    {
        button.image.color = normalColor;
        if (button == Route1)
        {
            Route1Image.SetActive(false);
        }
        if (button == Route2)
        {
            Route2Image.SetActive(false);
        }
    }
    
    public void ChoiseRoute1()
    {
        //ボタンの機能提出
        Route1.interactable = false;
        Route2.interactable = false;
        //インベントリをロック
        PlayerInfo.Instance.SetInventryLock(true);

        textControl.ResetTextData();
        //StartCoroutine(appearBGcover());
        addMainSentence(event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Result1);
        changCondirion(event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_health, event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_hunger, event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_warter);
        if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward2 == 0)
        {
            getItems(event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward1, event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward1_num);
        }
        else
        {
            getItems(event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward1, event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward1_num,
                     event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward2, event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Reward2_num);
        }
        next_num_tnp = event_manage.eventDatas[event_manage.now_event_num].Sentakusi1_Next_Ivent_ID;
        ID_change_Event_num();
        BG2.SetActive(true);
        //textControl.ClickEventAfterTextsEnd.AddListener(Nextevent);
        deep++;
    }
    public void ChoiseRoute2()
    {
        //ボタンの機能提出
        Route1.interactable = false;
        Route2.interactable = false;
        //インベントリをロック
        PlayerInfo.Instance.SetInventryLock(true);

        textControl.ResetTextData();
        //StartCoroutine(appearBGcover());
        addMainSentence(event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Result1);
        changCondirion(event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_health, event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_hunger, event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_warter);
        if (event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward2 == 0)
        {
            getItems(event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward1, event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward1_num);
        }
        else
        {
            getItems(event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward1, event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward1_num,
                     event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward2, event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Reward2_num);
        }
        next_num_tnp = event_manage.eventDatas[event_manage.now_event_num].Sentakusi2_Next_Ivent_ID;
        ID_change_Event_num();
        BG2.SetActive(true);
        //textControl.ClickEventAfterTextsEnd.AddListener(Nextevent);
        deep++;
    }
    public void ChoiseRoute3()
    {
        //ボタンの機能提出
        Route1.interactable = false;
        Route2.interactable = false;
        //インベントリをロック
        PlayerInfo.Instance.SetInventryLock(true);

        textControl.ResetTextData();
        //StartCoroutine(appearBGcover());
        changCondirion(-5, 0, 0);

        addMainSentence(event_manage.eventDatas[event_manage.now_event_num].Cancel_Result);
        next_num_tnp = event_manage.eventDatas[event_manage.now_event_num].Cancel_Next_Ivent_ID;
        ID_change_Event_num();
        textControl.ClickEventAfterTextsEnd.AddListener(Nextevent);
        deep--;
    }

    //IEnumerator appearBGcover()
    //{
    //    while (true)
    //    {
    //        Color BG_cover_color;
    //         BG_cover_color = BG_cover.GetComponent<Image>().color;
    //        BG_cover_color.a += 0.05f;
    //        BG_cover.GetComponent<Image>().color = BG_cover_color;
    //        if (BG_cover.GetComponent<Image>().color.a < 0.7f)
    //        {
    //            break;
    //        }
    //        yield return null;
    //    }
    //}

    //　本文、体力変化、アイテムゲットの順番

    //本文書き換え
    void addMainSentence(string scentence)
    {
        textControl.AddTextData(scentence);
    }

    //コンディション書き換え
    void changCondirion(int helth, int hunger, int thirst)
    {
        int beforeHelth;
        int beforeHunger;
        int beforeThirst;
        //変化前の値をストック
        beforeHelth = PlayerInfo.Instance.Health;
        beforeHunger = PlayerInfo.Instance.Hunger;
        beforeThirst = PlayerInfo.Instance.Thirst;

        //体力変化
        PlayerInfo.Instance.Health += helth;
        PlayerInfo.Instance.Hunger += hunger;
        PlayerInfo.Instance.Thirst += thirst;

        textControl.AddTextData($"体力 　{beforeHelth} => {PlayerInfo.Instance.Health}\n" +
                                $"食料 {beforeHunger} => {PlayerInfo.Instance.Hunger}\n" +
                                $"水分 　{beforeThirst} => {PlayerInfo.Instance.Thirst}");
    }

    //アイテムゲット
    void getItems(int Item_ID, int get_num)
    {
        if(Item_ID != 0)
        {
            string Item_name;      
            Item_name = PlayerInfo.Instance.Inventry.GetItemName((Items.Item_ID)Item_ID);
            if (PlayerInfo.Instance.Inventry.GetItem((Items.Item_ID)Item_ID, get_num))
            {
                textControl.AddTextData($"{Item_name}を{get_num}つ獲得！");
            }
            else
            {
                textControl.AddTextData($"{Item_name}を{get_num}個は持ちきれなかった。");
            }
            item_ID[0] = Item_ID;
            get_Num[0] = get_num;
            textControl.ClickEventAfterTextsEnd.AddListener(DisplayGetItem);
        }
        else
        {
            textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
            textControl.ClickEventAfterTextsEnd.AddListener(Nextevent);

        }
    }

    void getItems(int Item_ID, int Get_num, int itemID, int getNum)
    {
        string[] Item_name = new string[2];

        Item_name[0] = PlayerInfo.Instance.Inventry.GetItemName((Items.Item_ID)Item_ID);
        if (PlayerInfo.Instance.Inventry.GetItem((Items.Item_ID)Item_ID, Get_num))
        {
            textControl.AddTextData($"{Item_name[0]}を{Get_num}つ獲得！");
        }
        else
        {
            textControl.AddTextData($"{Item_name[0]}を{Get_num}個は持ちきれなかった。");
        }


        Item_name[1] = PlayerInfo.Instance.Inventry.GetItemName((Items.Item_ID)itemID);
        if (PlayerInfo.Instance.Inventry.GetItem((Items.Item_ID)itemID, getNum))
        {
            textControl.AddTextData($"{Item_name[1]}を{getNum}個獲得！");
        }
        else
        {
            textControl.AddTextData($"{Item_name[1]}を{getNum}個は持ちきれなかった。");
        }

        //textControl.AddTextData($"{Item_name[1]}を{getNum}つ手に入れました。");
        item_ID[0] = Item_ID;
        get_Num[0] = Get_num;
        item_ID[1] = itemID;
        get_Num[1] = getNum;
        textControl.ClickEventAfterTextsEnd.AddListener(DisplayGetItem2);

    }


    void DisplayGetItem()
    {
        StartCoroutine(displayGetItem());
    }
    IEnumerator displayGetItem()
    {       
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        for (int i = 0; i < get_Num[0]; i++)
        {
            yield return new WaitForSeconds(0.2f);
            //生成位置
            Vector3 pos = new Vector3(550, -250 + 125f * (i + 1), 0.0f);
            //プレハブを指定位置に生成
            //Instantiate(ItemPrefab, pos, Quaternion.identity);
            GameObject obj = Instantiate(ItemPrefab, pos, Quaternion.identity);
            obj.transform.SetParent(Route1.image.canvas.gameObject.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = pos;
            obj.GetComponent<Image>().sprite = SlotManager.GetItemData((Items.Item_ID)item_ID[0]).icon;
            obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = PlayerInfo.Instance.Inventry.GetItemAmount((Items.Item_ID)item_ID[0]).ToString();
        }

        textControl.ClickEventAfterTextsEnd.AddListener(Nextevent);

    }
    void DisplayGetItem2()
    {
        StartCoroutine(displayGetItem2());
    }
    IEnumerator displayGetItem2()
    {
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        for (int i = 0; i < get_Num[0] + get_Num[1]; i++)
        {
            yield return new WaitForSeconds(0.2f);
            //生成位置
            Vector3 pos = new Vector3(550, -250 + 125f * (i + 1), 0.0f);
            //プレハブを指定位置に生成
            //Instantiate(ItemPrefab, pos, Quaternion.identity);
            GameObject obj = Instantiate(ItemPrefab, pos, Quaternion.identity);
            obj.transform.SetParent(Route1.image.canvas.gameObject.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = pos;
            if (i < get_Num[0])
            {
                obj.GetComponent<Image>().sprite = SlotManager.GetItemData((Items.Item_ID)item_ID[0]).icon;
                obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = PlayerInfo.Instance.Inventry.GetItemAmount((Items.Item_ID)item_ID[0]).ToString();
            }
            else
            {
                obj.GetComponent<Image>().sprite = SlotManager.GetItemData((Items.Item_ID)item_ID[1]).icon;
                obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = PlayerInfo.Instance.Inventry.GetItemAmount((Items.Item_ID)item_ID[1]).ToString();
            }
        }
        textControl.ClickEventAfterTextsEnd.AddListener(Nextevent);
        
    }

    //次のイベントに飛ばす
    void Nextevent()
    {
        PlayerInfo.Instance.CheckPlayerDeath();
        nextEvent(next_num_tnp);
    }

    void nextEvent(int event_num)
    {
        event_manage.now_event_num = event_num;
        if (event_manage.eventDatas[event_manage.now_event_num].Event_ID== 5013)
        {
            //洞窟の一番奥
            textControl.ResetTextData();
            textControl.AddTextData(event_manage.eventDatas[event_manage.now_event_num].Main_Text);
            Title_Disply(event_manage.eventDatas[event_manage.now_event_num].Event_Title);
            textControl.ClickEventAfterTextsEnd.AddListener(toKyotenn);
        }
        else
        {
            //Loadimage.SetActive(true);         
            //textControl.ResetTextData();
            textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
            Invoke("eventStart", 0.3f);

            //if (deep > 0)
            //{
            //    Route3.SetActive(true);
            //}
            //else
            //{
            //    Route3.SetActive(false);
            //}

            Color BG_cover_color;
            BG_cover_color = BG_cover.GetComponent<Image>().color;
            BG_cover_color.a = deep_Light * deep;
            BG_cover.GetComponent<Image>().color = BG_cover_color;
        }
    }

    void eventStart()
    {
        PlayerInfo.Instance.SavePalyerData();
        BG2.SetActive(false);
        Loadimage.SetActive(false);
        //textControl.AddTextData(event_manage.eventDatas[event_manage.now_event_num].Main_Text);
        SetEventText();
        //ボタンの機能再開
        Route1.interactable = true;
        Route2.interactable = true;
        //インベントリをアンロック
        PlayerInfo.Instance.SetInventryLock(false);
    }

    //イベントIDとリストのナンバーの紐づけ
    void ID_change_Event_num()
    {
        for (int i = 0; i < event_manage.eventDatas.Length; i++)
        {
            if (next_num_tnp == event_manage.eventDatas[i].Event_ID)
            {
                Debug.Log(event_manage.eventDatas[i].Event_ID);
                next_num_tnp = i;
                Debug.Log(event_manage.eventDatas[event_manage.now_event_num].Event_ID);
                break;
            }
        }
    }

   

    //拠点に戻る

    void toKyotenn()
    {
        Debug.Log("DoActionしたよ");
        PlayerInfo.Instance.DoAction();
        Loadimage.SetActive(true);
        Invoke("goToKyotenn", 3.0f);
    }

    void goToKyotenn()
    {
        //インベントリをアンロック
        PlayerInfo.Instance.SetInventryLock(false);
        SceneManager.LoadScene(scene);
    }
}
