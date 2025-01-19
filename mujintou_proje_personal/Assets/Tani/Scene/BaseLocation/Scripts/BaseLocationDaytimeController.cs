using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseLocationDaytimeController : MonoBehaviour
{
    [SerializeField]
    List<PanelBase> panels;
    [SerializeField]
    SceneObject next_scene;
    [SerializeField]
    int pattern = 0;
    [SerializeField]
    GameObject BL_Tu_1_SceneControllerPrefab = null;
    [SerializeField]
    GameObject BL_Tu_2_SceneControllerPrefab = null;
    [SerializeField]
    GameObject BL_Tu_3_SceneControllerPrefab = null;

    float LongClickTanp = 0;

    [SerializeField] GameObject LongClickPr;
    //クリックエフェクト
    GameObject LongClickEf;
    //クリックエフェクトの親オブジェクト
    GameObject LongClickPa;
    //[SerializeField] GameObject Image_close;
    [SerializeField] GameObject parent;
    [SerializeField] LoadGauge loadGauge;
    [SerializeField] GameObject[] panels_GO;
    bool closePanels;
    bool restCheck;
    bool prehabF;
     // Start is called before the first frame update
    IEnumerator Start()
    {
        //Image_close.SetActive(false);   
        closePanels = false;
        prehabF = true;
        restCheck = false; 
        PlayerInfo.Instance.SetInventryLock(false);
        switch (pattern)
        {

            case 0:
                GameData.CanReturnTitle = true;
                if (PlayerInfo.Instance.Day.isDayTime)
                {
                    var areaNameShow = (AreaNameText)GameObject.FindAnyObjectByType(typeof(AreaNameText));
                    IEnumerator coroutine = areaNameShow.ShowAreaText();
                    StartCoroutine(coroutine);
                }
                else
                {
                    var fade = ((Fading)GameObject.FindAnyObjectByType(typeof(Fading)));
                    fade.Fade(Fading.type.FadeIn);
                }



                for (int i = 0; i < panels.Count; i++)
                {
                    ActivatePanelAdditive(i);
                }
                yield return null;
                DeactivateAllPanels();
                PlayerInfo.Instance.Inventry.SetVisible(false);
                break;
            case 1:
                {
                    //tutorial 一回目
                    var fade = ((Fading)GameObject.FindAnyObjectByType(typeof(Fading)));
                    fade.Fade(Fading.type.FadeIn);
                    Instantiate(BL_Tu_1_SceneControllerPrefab);
                    break;
                }
            case 2:
                {
                    PlayerInfo.Instance.DoAction();
                    var fade = ((Fading)GameObject.FindAnyObjectByType(typeof(Fading)));
                    fade.Fade(Fading.type.FadeIn);
                    Instantiate(BL_Tu_2_SceneControllerPrefab);
                    break;
                }
            case 3:
                {
                    var fade = ((Fading)GameObject.FindAnyObjectByType(typeof(Fading)));
                    fade.Fade(Fading.type.FadeIn);
                    Instantiate(BL_Tu_3_SceneControllerPrefab);
                    break;
                }

        }
       
    }

    
    void Update()
    {
        closePanels = false;
        for (int i = 0; i < panels_GO.Length; i++)
        {
            bool[] check = new bool[panels_GO.Length];
            if (!panels_GO[i].activeSelf) check[i] = false;
            else check[i] = true;
            if (check[i]) closePanels = true;
        }
        if(PlayerInfo.Instance.Inventry.GetVisibility()) closePanels = true;

        if (PlayerInfo.InstanceNullable == null) return;
        PlayerInfo.Instance.CheckPlayerDeath();
        if (Input.GetMouseButton(1) )
        {
            LongClickTanp +=  Time.deltaTime;
            if (!Input.GetMouseButton(0))
            {
                if (0.5f < LongClickTanp && LongClickTanp < 0.6f)
                {
                    if (prehabF)
                    {
                        if (!restCheck)
                        {
                            efectStart();
                            Debug.Log("プレハブ生成");
                            prehabF = false;
                        }
                    }
                }
                if (LongClickTanp > loadGauge.countTime + 0.5f)
                {
                    Debug.Log("プレハブ破壊");
                    Destroy(LongClickEf);
                    Destroy(LongClickPa);
                    closePanels = true;
                    DeactivateAllPanels();
                    PlayerInfo.Instance.Inventry.SetVisible(false);
                    LongClickTanp = 0;
                }

            }
            else
            {
                Destroy(LongClickEf);
                Destroy(LongClickPa);
                closePanels = true;
            }
            //DeactivateAllPanels();
            //PlayerInfo.Instance.Inventry.SetVisible(false);
        }
        if(Input.GetMouseButtonUp(1))
        {
            Destroy(LongClickEf);
            Destroy(LongClickPa);
            prehabF = true;
            LongClickTanp = 0;
            Debug.Log("tanp初期化");
        }
        if (Input.GetMouseButtonDown(1))
        {
           
        }
        void efectStart()
        {
            if (closePanels)
            {
                LongClickPa = Instantiate(parent);
                LongClickEf = Instantiate(LongClickPr);
                LongClickEf.gameObject.transform.SetParent( LongClickPa.transform.GetChild(0));
                LongClickEf.transform.position = Input.mousePosition;
            }
        }
        //Image_close.SetActive(closePanels);
    }
    private void Awake()
    {
        

    }
     public  void Pushing()
    {
        if (closePanels)
        {
            DeactivateAllPanels();
            PlayerInfo.Instance.Inventry.SetVisible(false);
            closePanels= false; 
        }
    }
    

    public void ActivatePanelSingle(int index)
    {
        if (index >= panels.Count) return;
        DeactivateAllPanels();
        panels[index].SetEnabled(true);
    }

    public void ActivatePanelAdditive(int index)
    {
        if (index >= panels.Count) return;
        panels[index].SetEnabled(true);
    }

    public void DeactivatePanel(int index)
    {
        if (index >= panels.Count) return;
        panels[index].SetEnabled(false);
    }

    public void DeactivateAllPanels()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetEnabled(false);
        }
    }

    public void SwitchActive(int index)
    {
        if (index >= panels.Count) return;
        panels[index].SwitchEnabaled();
    }

    //public void NextScene()
    //{
    //    var fade = ((Fading)GameObject.FindAnyObjectByType(typeof(Fading)));
    //    fade.Fade(Fading.type.FadeOut);
    //    fade.OnFadeEnd.AddListener(() => { SceneManager.LoadScene(scene); });
        
    //}

    public void ChangeBaseLocation()
    {

        SceneManager.LoadScene(next_scene);
    }

    public void restCheckTrue()
    {
        restCheck = true;
        Invoke("restCheckFalse", 5.0f);
    }
    void restCheckFalse()
    {
        restCheck = false;
    }
}
   
