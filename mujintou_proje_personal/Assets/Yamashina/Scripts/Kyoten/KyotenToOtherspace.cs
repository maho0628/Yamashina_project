using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KyotenToOtherspace : CreditPanel1
{
    [SerializeField] GameObject TakibiPanel;
    [SerializeField] GameObject WakimizuPanel;
    [SerializeField] GameObject cookingPanel;
    [SerializeField] GameObject sleeppingPanel;
    [SerializeField] GameObject prefab;
    [SerializeField]  GameObject InventoryPanel;
    [SerializeField] After_LocationSentaku1 after_LocationSentaku;
    [SerializeField] multiAudio multiAudio;
    [SerializeField] GameObject takibi_tabu;
    [SerializeField] GameObject takibi_button;
    [SerializeField] GameObject TansakuPanel;
    [SerializeField] GameObject Cooking_messagePanel;
    [SerializeField] GameObject Takibi_3PattonPanel;
    [SerializeField] GameObject craftPanel;

    [SerializeField] GameObject shadow1;
    [SerializeField] GameObject shadow2;
    [SerializeField] GameObject shadow3;
    [SerializeField] GameObject shadow4;
    //public GameObject wakimizu_tabu;
    public bool takibi_f;

    //行先メニューでいけない場所を隠すイメージのフラグ
    public bool shadow1_f = true;   //左下
    public bool shadow2_f = true;   //右上
    public bool shadow3_f = true;   //右中央
    public bool shadow4_f = true;   //右下
                                    //private float TakibiTabu_timer = 0.0f;
                                    //public float takibiTabu_wait = 0.5f;
                                    //  [SerializeField] public Text takibi_number; // 新しいUIパネルへの参照
                                    //[SerializeField]Text yesbutton;
                                    //[SerializeField] Text nobutton;
                                    //[SerializeField] Text CookingButton;
                                    //[SerializeField] GameObject newbutton;
                                    //bool takibicheck = false;



    // Start is called before the first frame update
    void Start()
    {

        //inventory.SetActive(false);
        //TakibiTabu_timer = 0.0f;
        if (PlayerInfo.Instance.Fire <= 0)
        {
            takibi_f = false;
        }
        else
        {
            takibi_f = true;
        }
        // prehubdelete.deletePrehub();
        mainPanel.SetActive(true);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        InventoryPanel.SetActive(false);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        cookingPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        Cooking_messagePanel.SetActive(false);
        //InventoryMove();
        Takibi_3PattonPanel.SetActive(false);
        craftPanel.SetActive(false);
        //newbutton.SetActive(false); 

        //inventory.SetActive(true);
        //InventorySlot.SetActive(true);


        //wakimizu_tabu.SetActive(false);
    }

    //private void Update()
    //{

    //    quitPanel();

    //}
    // Update is called once per frame
    public override void MainView()
    {
        mainPanel.SetActive(true);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        cookingPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        Cooking_messagePanel.SetActive(false);
        craftPanel.SetActive(false);
        //wakimizu_tabu.SetActive (false);        
        //InventoryMove()/*;*/
        //newbutton.SetActive(false);
    }

    public override void SubView()
    {
        mainPanel.SetActive(false);
        subPanel.SetActive(true);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        cookingPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        Cooking_messagePanel.SetActive(false);
        GameObject.Find("SozaibakoSlotManager").transform.position = Vector3.zero;
        craftPanel.SetActive(false);


        //wakimizu_tabu.SetActive(false);
        //GameObject.Find("Inventory").transform.position = Vector3.zero;
        //newbutton.SetActive(false); 

    }
    public override void CreditView()
    {
        mainPanel.SetActive(false);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        cookingPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        Cooking_messagePanel.SetActive(false);
        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        craftPanel.SetActive (false);   
        //wakimizu_tabu.SetActive(false);
        //InventoryMove();
        //newbutton.SetActive(false); 


    }


    public void WakimizuView()
    {
        mainPanel.SetActive(false);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        cookingPanel.SetActive(false);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(true);
        sleeppingPanel.SetActive(false);
        //InventoryMove();
        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        Cooking_messagePanel.SetActive(false);
        craftPanel.SetActive(false);
        //newbutton.SetActive(false);
        //wakimizu_tabu.SetActive(false);

    }
    public void TakibiView()
    {

        //Debug.Log("takibi" + takibi);
        // if (!takibicheck)
        //{
        //if (Boolnumber())
        //{
        //takibi_f = false;
        mainPanel.SetActive(false);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        cookingPanel.SetActive(false);
        TakibiPanel.SetActive(true);
        WakimizuPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        craftPanel.SetActive(false);

        //newbutton.SetActive(false);



        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        Cooking_messagePanel.SetActive(false);
        //    wakimizu_tabu.SetActive(false);
        //}
        //  }
        //else if (!takibicheck)
        //{
        //    MainView();

        //}

    }
    public void CookingView()
    {
        mainPanel.SetActive(false);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        cookingPanel.SetActive(true);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        craftPanel.SetActive(false);
        //GameObject.Find("Inventory").transform.position = Vector3.zero;


        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        Cooking_messagePanel.SetActive(false);
        //newbutton.SetActive(false);

        //wakimizu_tabu.SetActive(false);



    }
    public void sleepingView()
    {
        //Instantiate(prefab);

        mainPanel.SetActive(false);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        cookingPanel.SetActive(false);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        sleeppingPanel.SetActive(true);
        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        Cooking_messagePanel.SetActive(false);
        craftPanel.SetActive(false);

        //newbutton.SetActive(false);

        //wakimizu_tabu.SetActive(false);




    }
    public void takibi_tabuPanel()
    {
        mainPanel.SetActive(true);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        cookingPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        takibi_tabu.SetActive(true);
        TansakuPanel.SetActive(false);
        //newbutton.SetActive(false);
        craftPanel.SetActive(false);


        Cooking_messagePanel.SetActive(false);
        /* if (takibi_tabu == true)
         {
             TakibiTabu_timer += Time.deltaTime;
             Debug.Log("takibitabu_timer"+TakibiTabu_timer);
             if (TakibiTabu_timer >= takibiTabu_wait)
                 {

                 takibi_tabu.SetActive(false);
             }
         }
         else if(!takibi_tabu)
             {
             TakibiTabu_timer = 0.0f;
         }
        */


        //wakimizu_tabu.SetActive(false);
        // takibi_number.text = "焚き火:" + takibi;

    }

    public void tansakuPanel()
    {
        mainPanel.SetActive(false);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        cookingPanel.SetActive(false);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        //newbutton.SetActive(false);
        craftPanel.SetActive(false);

        //どこに行けるかの確認
        shadowCheck();
        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(true);
        Cooking_messagePanel.SetActive(false);

        //GameObject.Find("InventorySlotManager").transform.position = new Vector3(375.4f, -1039f, 0.0f);
        //GameObject.Find("SozaibakoSlotManager").transform.position = new Vector3(-4.4f, -7.94f, 0.0f);

    }


    public void takibiCheck()
    {

        if (takibi_f == false)
        {
            TakibiView();
        }
        else if (takibi_f == true)
        {
            takibi_button.SetActive(false);
        }
    }

    public void cooking_tabu()
    {

        mainPanel.SetActive(true);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        cookingPanel.SetActive(false);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        Cooking_messagePanel.SetActive(true);
        craftPanel.SetActive(false);

        //newbutton.SetActive(false);


    }


    //{

    public void Cookingflag()
    {
        if (takibi_f == false)
        {
            cooking_tabu();
        }
        else if (takibi_f == true)
        {
            CookingView();
        }

    }
    //    if (takibi <=0)
    //    {
    //        takibicheck = true;
    //        takibi = 100;
    //        return true;
    //    }
    //    else 
    //    {
    //        takibicheck = false;
    //        return false;
    //    }
    //}


    void allShadow()
    {
        shadow1.SetActive(true);
        shadow2.SetActive(true);
        shadow3.SetActive(true);
        shadow4.SetActive(true);
    }
    void shadowCheck()
    {
        if (!shadow1_f)
        {
            shadow1.SetActive(false);
        }
        if (!shadow2_f)
        {
            shadow2.SetActive(false);
        }
        if (!shadow3_f)
        {
            shadow3.SetActive(false);
        }
        if (!shadow4_f)
        {
            shadow4.SetActive(false);
        }

    }
    public  void SozaibakoView()
    {
        mainPanel.SetActive(false);
        subPanel.SetActive(true);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        cookingPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        Cooking_messagePanel.SetActive(false);
        craftPanel.SetActive(false);


        GameObject.Find("SozaibakoSlotManager").transform.position = Vector3.zero;
        GameObject.Find("InventorySlotManager").transform.position = new Vector3(375.4f, -1039f, 0.0f);

    }




    public void deleteShadow(int field)
    {
        if (field == 1)
        {
            shadow1_f = false;
        }
        if (field == 2)
        {
            shadow2_f = false;
        }
        if (field == 3)
        {
            shadow3_f = false;
        }
        if (field == 4)
        {
            shadow4_f = false;
        }
    }
    // public void InventoryMove()
    // {

    //     GameObject.Find("InventoryManager").transform.position = new Vector3(375.4f, -1039f, 0.0f);
    // }
    //public void TakibiButtonTextChange()
    // {
    //     //newbutton.SetActive(true);
    //     //yesbutton.text = "ライター";
    //     //nobutton.text = "弓式";
    //     //CookingButton.text = "きりもみ";
    public  void InventoryView()
    {
        InventoryPanel.SetActive(true);
        //GameObject.Find("Inventory").transform.position = Vector3.zero;
        GameObject.Find("InventorySlotManager").transform.position = Vector3.zero;
        GameObject.Find("SozaibakoSlotManager").transform.position = new Vector3(-4.4f, -7.94f, 0.0f);

        //inventory = GameObject.Find("Inventory");

        //newbutton.SetActive(false); 
        //wakimizu_tabu.SetActive(false);
    }
    public void CraftPanelView()
    {
        mainPanel.SetActive(false);
        subPanel.SetActive(false);
        Audiovolume.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        TakibiPanel.SetActive(false);
        WakimizuPanel.SetActive(false);
        cookingPanel.SetActive(false);
        sleeppingPanel.SetActive(false);
        takibi_tabu.SetActive(false);
        TansakuPanel.SetActive(false);
        Cooking_messagePanel.SetActive(false);
        craftPanel.SetActive(true);
    }


    // }

}




