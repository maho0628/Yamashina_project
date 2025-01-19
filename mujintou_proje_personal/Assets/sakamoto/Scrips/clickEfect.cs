using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class clickEfect_Overlay : MonoBehaviour
{
    //クリックエフェクトのプレハブを取得
    [Header("クリックエフェクトのプレハブを入れて")]
    [SerializeField] GameObject efect;
    [Header("クリックエフェクトのプレハブを入れて")]
    [SerializeField] Animator animator;
    [SerializeField] GameObject parent;
    //クリックエフェクト
    GameObject ClickEfect;
    GameObject ClickEfectParent;
    //[Header("canvasのRendermodeがoverlayならチェックを入れる。\n Cameraなら不要")]
    //[SerializeField] bool overlay;
    [SerializeField] UnityEvent ClickEndEvent;
    //[SerializeField] Camera targetCamera;


    //スクリーン座標とワールド座標の宣言
    Vector3 mousePos, worldPos;
    // Start is called before the first frame update
    void Start()
    {
        //anime = efect.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {              
        //マウスの位置を取得
        mousePos = Input.mousePosition;         
        //マウス座標をワールド座標に変換
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y + 5, 10.0f));
        //Debug.Log(worldPos);
 
        if (Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1))
        {
            prehabDestroy();

            ClickEfectParent = Instantiate(parent);
            ClickEfect = Instantiate(efect);

            //if (!overlay)
            //{
            //    ClickEfectParent.gameObject.transform.GetChild(0).GetComponent<Canvas>().worldCamera = targetCamera;

            //}

            ClickEfect.gameObject.transform.SetParent(ClickEfectParent.transform.GetChild(0));

            animator = ClickEfect.GetComponent<Animator>();
            efect.SetActive(true);
            //if (overlay)
            //{
                ClickEfect.transform.position = mousePos;
            //}
            //else
            //{
            //    ClickEfect.transform.position = worldPos;
            //}
            animator.Play("clickAnime",0);
            ClickEndEvent.Invoke();

            Invoke("prehabDestroy", 0.3f);

            //efect.transform.parent = parent.transform;

            //Debug.Log(efect.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position);
            //efect.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position = worldPos;
            //Debug.Log(efect.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position);

            //efect.transform.position = worldPos;
            //anime.SetBool("clickAnime", true);
            //Debug.Log(efect.transform.position);
        }

    }

    void prehabDestroy()
    {
        Destroy(ClickEfect);
        Destroy(ClickEfectParent);
    }
    public void test()
    {
        Debug.Log("テスト成功");
    }
}
