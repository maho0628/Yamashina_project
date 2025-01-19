using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class clickEfect_Overlay : MonoBehaviour
{
    //�N���b�N�G�t�F�N�g�̃v���n�u���擾
    [Header("�N���b�N�G�t�F�N�g�̃v���n�u������")]
    [SerializeField] GameObject efect;
    [Header("�N���b�N�G�t�F�N�g�̃v���n�u������")]
    [SerializeField] Animator animator;
    [SerializeField] GameObject parent;
    //�N���b�N�G�t�F�N�g
    GameObject ClickEfect;
    GameObject ClickEfectParent;
    //[Header("canvas��Rendermode��overlay�Ȃ�`�F�b�N������B\n Camera�Ȃ�s�v")]
    //[SerializeField] bool overlay;
    [SerializeField] UnityEvent ClickEndEvent;
    //[SerializeField] Camera targetCamera;


    //�X�N���[�����W�ƃ��[���h���W�̐錾
    Vector3 mousePos, worldPos;
    // Start is called before the first frame update
    void Start()
    {
        //anime = efect.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {              
        //�}�E�X�̈ʒu���擾
        mousePos = Input.mousePosition;         
        //�}�E�X���W�����[���h���W�ɕϊ�
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
        Debug.Log("�e�X�g����");
    }
}
