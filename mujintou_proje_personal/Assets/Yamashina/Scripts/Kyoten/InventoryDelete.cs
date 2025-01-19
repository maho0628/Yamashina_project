using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDelete : MonoBehaviour
{
    float LongClickTanp = 0;
    bool restCheck;
    bool prehabF;
    bool closePanels;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject LongClickPr;

    [SerializeField] LoadGauge loadGauge;
     //クリックエフェクト
    GameObject LongClickEf;
    //クリックエフェクトの親オブジェクト
    GameObject LongClickPa;
    // Start is called before the first frame update
    void Start()
    {
        closePanels = false;
        prehabF = true;
        restCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        closePanels = false;

        if (PlayerInfo.Instance.Inventry.GetVisibility()) closePanels = true;

        if (Input.GetMouseButton(1))
        {
            LongClickTanp += Time.deltaTime;
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
                    //DeactivateAllPanels();
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
        if (Input.GetMouseButtonUp(1))
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
                LongClickEf.gameObject.transform.SetParent(LongClickPa.transform.GetChild(0));
                LongClickEf.transform.position = Input.mousePosition;
            }
        }


    }
}
