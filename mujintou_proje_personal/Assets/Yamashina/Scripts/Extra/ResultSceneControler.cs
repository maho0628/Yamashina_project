using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Text.RegularExpressions;

public class ResultSceneControler : MonoBehaviour
{



    [SerializeField]
    SceneObject title;
    [SerializeField] Text Txt_deador;
    [SerializeField] Text Txt_day;
    [SerializeField] Text Special;
    
    //[SerializeField]
    //RawImage RawImage;
    //[SerializeField] GameObject action;
    //[SerializeField]
    //GameObject ActionValueImagePrefab;
    [SerializeField]
    Image ItemImage;
    [SerializeField] public  GameObject Text_screen;
    public string gamepath;
    public string timeStamp;
    string screenShotPath;
    bool isDisplayed;
    [SerializeField] anotherSoundPlayer SEAudio;
    [SerializeField] Fade fade;
    private void Awake()
    {
        //��������
        Debug.Log(PlayerInfo.Instance.Day.day);
        Txt_day.text = PlayerInfo.Instance.Day.day.ToString() + "��";

        //�������ǂ���
        if (PlayerInfo.Instance.Health <= 0)
        {
            Debug.Log(PlayerInfo.Instance.Health);
            Txt_deador.text = "���s";

        }
        if (PlayerInfo.Instance.Health > 0)
        {
            Txt_deador.text = "����";
        }

        //�ŏ��Ɏ�ɓ��ꂽ�A�C�e���̖��O
        Debug.Log(PlayerInfo.Instance.FirstItemId);
        int ID = PlayerInfo.Instance.FirstItemId;
        string name = PlayerInfo.Instance.Inventry.GetItemName((Items.Item_ID)ID);
        Special.text = name;

        //�ŏ��Ɏ�ɓ��ꂽ�A�C�e���̃C���[�W
        ItemImage.sprite = SlotManager.GetItemData((Items.Item_ID)PlayerInfo.Instance.FirstItemId).icon;
        SEAudio = GameObject.FindAnyObjectByType<anotherSoundPlayer>().GetComponent<anotherSoundPlayer>();
    }

    private void Start()
    {
        //�B�e�������ǂ���
        isDisplayed = false;
        fade.feadout_f = false;

        //�ǂ̂��炢�̕b���Ŏ����Ń^�C�g����ʂɖ߂邩
        //Invoke(nameof(ReToTitle), 50f);
    }
    public static class ScreenshotCaptor
    {

        /// <summary>
        /// �X�N���[���V���b�g���B��
        /// </summary>
        public static IEnumerator Capture(string imageName = "image.png", Action callback = null)
        {
            //���ۂ̓��t�ɕϊ�
            DateTime date = DateTime.Now;
            imageName = date.ToString("yyyy-MM-dd-HH-mm-ss-fff");
             string path = "";

            // �v���W�F�N�g�t�@�C���̃X�N���[���V���b�g�t�H���_�̒����ɍ쐬
            path = "screenshot/" + imageName + ".png";
            string imagePath = Application.streamingAssetsPath + "/"+path;
            //iOS�AAndroid���@�̎��̓p�X��Application.persistentDataPath��ǉ�
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
    imagePath = Path.Combine(Application.persistentDataPath, imageName);
#endif

            //�O�ɎB�����X�N�V�����폜
            File.Delete(imagePath);

            //�X�N���[���V���b�g���B��
            UnityEngine.ScreenCapture.CaptureScreenshot(imagePath);
            //�X�N���[���V���b�g���ۑ������܂őҋ@(�ő�2�b)
            float latency = 0, latencyLimit = 2;
            while (latency < latencyLimit)
            {
                //�t�@�C�������݂��Ă���΃��[�v�I��
                if (File.Exists(imagePath))
                {
                    break;
                }
                latency += Time.deltaTime;
                yield return null;
            }
            //�ҋ@���Ԃ�����ɒB���Ă�����x���\��(�����炭�X�N�V�����ۑ��o���Ă��Ȃ���)
            if (latency >= latencyLimit)
            {
                Debug.LogWarning("�ҋ@���Ԃ�����ɒB���܂����I����ɃX�N���[���V���b�g���ۑ��ł��Ă��܂���I");
            }

            //�R�[���o�b�N���o�^����Ă���Ύ��s
            if (callback != null)
            {
                callback();
            }
        }
    }
    public void ReToTitle()
    {

        SEAudio.ChooseSongs_SE(0);

        Invoke(nameof(ResetTitle), 0.2f);
    }

    //���ۂɃC���X�y�N�^�[��ŎB�e�{�^���ɐݒ肵�Ă���֐�
    public void CaptureButtton()
    {
        SEAudio.ChooseSongs_SE(0);

        StartCoroutine(ScreenshotCaptor.Capture
            (imageName: "Screenshot.png", callback: Callback));
        GameObject.FindGameObjectWithTag("ClickEffect").SetActive(false);   


    }
    public void ResetTitle()
    {
        //var loaded = SceneManager.LoadSceneAsync(title);
        //loaded.allowSceneActivation = false;

        //�v���C���[�̔j��������Ɉړ�//
        if (PlayerInfo.InstanceNullable)
        {
            PlayerInfo.Instance.DestroySelf();
        }
        DataManager.ErasePlayerSaveData();

        //�v���C���[�̔j��������Ɉړ�//
        //loaded.allowSceneActivation = true;
        fade.feadout_f = true;
      
    }
    //�B�e�������Ɏ��s�����i�B�e���܂����̃e�L�X�g�\���j
    private void Callback()
    {
        Debug.Log("�B�e����");
        Text_screen.SetActive(true);
    }
}



// Start is called before the first frame update


