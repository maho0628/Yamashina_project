using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Option_bridge : MonoBehaviour
{
    //[SerializeField] public AudioMixer audioMixer;
    [SerializeField] public Slider bgmSlider;//BGMスライダー
    [SerializeField] public Slider seSlider;//SEスライダー
    [SerializeField] multiAudio multiAudio;
    [SerializeField] public RectTransform fillRectTransform_BGM;
    [SerializeField] public RectTransform fillRectTransform_SE;
    [SerializeField] public float fixedWidth = 100f;
    [SerializeField] public float fixedHeight = 50f;

    void Start()
    {
        Audiovolume.instance.BGM = GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume;
        Audiovolume.instance.SE = GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume;

        Audiovolume.instance.audioSourceBGM = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();
        Audiovolume.instance.audioSourceSE = GameObject.FindWithTag("SE").GetComponent<AudioSource>();

        //bgmSlider.fillRect = fillRectTransform_BGM;
        //seSlider.fillRect = fillRectTransform_SE;
        fillRectTransform_BGM = GameObject.FindGameObjectWithTag("BGM_Rect").GetComponent<RectTransform>();
        fillRectTransform_SE = GameObject.FindGameObjectWithTag("SE_Rect").GetComponent<RectTransform>();
        //SetFixedSize();

        BgmLoadSlider();
        SeLoadSlider();
        //bgmSlider.fillRect = multiAudio.fillRectTransform_BGM;
        //seSlider.fillRect = multiAudio.fillRectTransform_SE;
        ////fillRectTransform_BGM = GameObject.FindGameObjectWithTag("BGM_Rect").GetComponent<RectTransform>();
        //fillRectTransform_SE = GameObject.FindGameObjectWithTag("SE_Rect").GetComponent<RectTransform>();

        //SceneManager.sceneLoaded += SaveVolume;


        //{
        //    //// 幅と高さを固定値に設定する
        //    SetFixedSize();
        //}
    }
    private void OnEnable()
    {
        
        if (gameObject.activeSelf)
        {
            multiAudio = GetComponent<multiAudio>();

            bgmSlider = GameObject.FindGameObjectWithTag("BGMSlider").GetComponent<Slider>();
            seSlider = GameObject.FindGameObjectWithTag("SESlider").GetComponent<Slider>();
            //    GameObject.FindGameObjectWithTag("SESlider").GetComponent<Slider>().value = multiAudio.seSlider.value;

            //    GameObject.FindGameObjectWithTag("BGMSlider").GetComponent<Slider>().value = multiAudio.bgmSlider.value;
        }
        else {
            
        }


      

    }
    // 幅と高さを固定値に設定するメソッド
    void SetFixedSize()
    {
        //幅と高さを設定する
        fillRectTransform_BGM.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fixedWidth);
        fillRectTransform_BGM.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fixedHeight);
        fillRectTransform_SE.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fixedWidth);
        fillRectTransform_SE.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fixedHeight);
    }

    // Start is called before the first frame update
    public void BgmVolume(float value)
    {
        float a = bgmSlider.value;

        Audiovolume.instance.SetBgmVolume(a);
        //audioMixer.SetFloat("BGM", multiAudio.ConvertVolumeToDb(bgmSlider.value));

        Audiovolume.instance.BGM = GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume;

        BgmSave();
        print(a);
    }

    public void SeVolume(float value)
    {
       
        float b = seSlider.value;

        Audiovolume.instance.SetSeVolume(b);
        //audioMixer.SetFloat("SE", multiAudio.ConvertVolumeToDb(seSlider.value));
        //セーブ
        SeSave();

        Audiovolume.instance.SE = GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume;

        //audioSourceBGM = GameObject.Find("BGM_ob").GetComponent<AudioSource>();
        //audioSourceSE= GameObject.Find("SE_ob").GetComponent<AudioSource>();


        print(b);
    }
    public void BgmSave()
    {
        PlayerPrefs.SetFloat("bgmSliderValue", bgmSlider.value);
        Debug.Log("Check OnDisable");

    }
    public void SeSave()
    {
        PlayerPrefs.SetFloat("seSliderValue", seSlider.value);
        Debug.Log("Check OnDisable");

    }
   //public void SaveVolume(Scene scene,LoadSceneMode mode)
   // {


   // }


    // 幅と高さを固定値に設定するメソッド
    


    public void BgmLoadSlider()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmSliderValue", 1.0f);

        //bgmSlider.GetComponent<Slider>().onValueChanged.AddListener(BgmVolume);
        float a = bgmSlider.value;
        Audiovolume.instance.SetBgmVolume(a);
        print(a);
    }

    public void SeLoadSlider()
    {
        //seSlider.GetComponent<Slider>().onValueChanged.AddListener(SeVolume);

        seSlider.value = PlayerPrefs.GetFloat("seSliderValue", 1.0f);
        float b = seSlider.value;
        Audiovolume.instance.SetSeVolume(b);
        print(b);
    }

}