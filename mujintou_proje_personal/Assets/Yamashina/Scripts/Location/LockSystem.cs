using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] After_LocationSentaku1 after_;
    [SerializeField]
    Sprite[] image_Lock;
    public int First_unLocknum;
    public int Second_unLocknum;
    public int Third_unLocknum;
    public int Final_unLocknum;
    [HideInInspector]
    public bool[] isOpen = new bool[6];

    private void Start()
    {
        Debug.Log("�s���l��" + PlayerInfo.Instance.ActionValue);

      

        System.Array.Fill(isOpen, false);
        isOpen[0] = true;
        isOpen[1] = true;
    }

    //private void /*Update*/()
    //{
        
    //}
    private void Awake()
    {
        PlayerInfo.Instance.OnMaxActionValueChange.AddListener(() => Lock());
        after_.buttons[2].GetComponent<Image>().sprite = image_Lock[0];
        after_.buttons[3].GetComponent<Image>().sprite = image_Lock[0];
        after_.buttons[4].GetComponent<Image>().sprite = image_Lock[1];
        after_.buttons[5].GetComponent<Image>().sprite = image_Lock[2];
        after_.button_Kawabe.interactable = false;
        after_.button_Doukutu.interactable = false;
        after_.button_Sangaku.interactable = false;
        after_.button_Kakou.interactable = false;

        Lock();
    }
    private void OnEnable()
    {
        Lock();
    }

    void Lock()
        
    {
        Debug.Log($"���݂̍s���l: {PlayerInfo.Instance.MaxActionValue}");
        //���
        if (PlayerInfo.Instance.MaxActionValue >= First_unLocknum)
        {
            Debug.Log(PlayerInfo.Instance.ActionValue >= First_unLocknum);
            after_.button_Kawabe.interactable = true;
            after_.buttons[2].GetComponent<Image>().sprite = after_.image[2];
            isOpen[2] = true;

        }
        //else if (PlayerInfo.Instance.MaxActionValue < First_unLocknum)
        //{
        //    after_.button_Kawabe.interactable = false;
        //    after_.buttons[2].GetComponent<Image>().sprite = image_Lock[0];
        //}
        //�R�x
        if (PlayerInfo.Instance.MaxActionValue >= Second_unLocknum)
        {

            after_.button_Sangaku.interactable = true;
            after_.buttons[3].GetComponent<Image>().sprite = after_.image[3];
            Debug.Log("�s���l��" + PlayerInfo.Instance.ActionValue);
            isOpen[3] = true;

        }
        //else if (PlayerInfo.Instance.MaxActionValue < Second_unLocknum)
        //{
        //    after_.button_Sangaku.interactable = false;
        //    after_.buttons[3].GetComponent<Image>().sprite = image_Lock[0];
        //}
        //�Ό�
        if (PlayerInfo.Instance.MaxActionValue >= Third_unLocknum)
        {
            after_.button_Kakou.interactable = true;

            after_.buttons[4].GetComponent<Image>().sprite = after_.image[4];
            Debug.Log("�s���l��" + PlayerInfo.Instance.ActionValue);

            isOpen[4] = true;
        }
        //else if (PlayerInfo.Instance.MaxActionValue < Third_unLocknum)
        //{
        //    after_.button_Kakou.interactable = true;

        //    after_.buttons[4].GetComponent<Image>().sprite = image_Lock[1];
        //}
        //���A
        if (PlayerInfo.Instance.MaxActionValue >= Final_unLocknum)
        {
            after_.button_Doukutu.interactable = true;

            after_.buttons[5].GetComponent<Image>().sprite = after_.image[5];
            Debug.Log("�s���l��" + PlayerInfo.Instance.ActionValue);
            isOpen[5] = true;
        }
        //else if (PlayerInfo.Instance.MaxActionValue < Final_unLocknum)
        //{

        //    after_.button_Doukutu.interactable = true;

        //    after_.buttons[5].GetComponent<Image>().sprite = image_Lock[2];

        //}

    }
}
