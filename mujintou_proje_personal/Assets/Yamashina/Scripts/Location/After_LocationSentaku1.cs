using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class After_LocationSentaku1 : MonoBehaviour
{

    [SerializeField] GameObject Panel;
    [SerializeField] public Button button_Kawabe;
    [SerializeField] public Button button_Forest;
    [SerializeField] public Button button_Kaigan;
    [SerializeField] public Button button_Doukutu;
    [SerializeField] public Button button_Sangaku;
    [SerializeField] public Button button_Kakou;
    [SerializeField] public GameObject[] buttons;
    [SerializeField] public Sprite[] image;
    [SerializeField] public Sprite[] image_c;
    [SerializeField] public Fading fading;
    [SerializeField] SceneObject Doukutu;
    [SerializeField] SceneObject[] nextScene;

    [SerializeField] Button Syupatsu;
    [SerializeField] public int num;
    private int Choice_num;

    LockSystem lockSystem;

    void Start()
    {

        Syupatsu.interactable = false;
        fading= GameObject.FindWithTag("GameController").GetComponent<Fading>();

        lockSystem = GetComponent<LockSystem>();

        

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(Doukutu);

        }
    }


    private void OnEnable()
    {
       
    }


    public void Sentakuplaces(int num)
    {

        switch (num)
        {





            ////海岸選択
            case 0:

                Debug.Log(("シーン先は" + num));
                num = 0;
                Choice_num = num;
                buttons[num].GetComponent<Image>().sprite = image_c[num];
                ;
                for (int i = 0; i < buttons.Length; i++)
                {
                    Syupatsu.interactable = true;

                    if (i != num && lockSystem.isOpen[i])

                    {
                        buttons[i].GetComponent<Image>().sprite = image[i];
                    }

                    //Syupatsu.interactable = false;

                    //image_Change.restartImage(0);
                    //eventTrigger_Kaigan.enabled = false;
                    //eventTrigger_Doukutu.enabled = false;
                    //eventTrigger_Kawabe.enabled= false; 
                    //eventTrigger_Kakou.enabled= false;  
                    //eventTrigger_Sangaku.enabled= false;    
                    //eventTrigger_Forest.enabled= false; 

                    //button_Doukutu.enabled = false;
                    //button_Forest.enabled=false;
                    //button_Kaigan.enabled=false;    
                    //button_Kakou.enabled=false;
                    //button_Kawabe.enabled=false;
                    //button_Sangaku.enabled=false;



                }
                break;
            ////森選択
            case 1:

                Debug.Log(("シーン先は" + num));
                num = 1;
                Choice_num = num;
                //image_Change.restartImage(1);
                buttons[num].GetComponent<Image>().sprite = image_c[num];
                ;
                for (int i = 0; i < buttons.Length; i++)
                {
                    Syupatsu.interactable = true;

                    if (i != num && lockSystem.isOpen[i])
                    {
                        buttons[i].GetComponent<Image>().sprite = image[i];
                    }
                    //Syupatsu.interactable = false;

                    //eventTrigger_Kaigan.enabled = false;
                    //eventTrigger_Doukutu.enabled = false;
                    //eventTrigger_Kawabe.enabled = false;
                    //eventTrigger_Kakou.enabled = false;
                    //eventTrigger_Sangaku.enabled = false;
                    //eventTrigger_Forest.enabled = false;
                    //button_Doukutu.enabled = false;
                    //button_Forest.enabled = false;
                    //button_Kaigan.enabled = false;
                    //button_Kakou.enabled = false;
                    //button_Kawabe.enabled = false;
                    //button_Sangaku.enabled = false;

                }

                break;
            ////川辺選択
            case 2:

                Debug.Log(("シーン先は" + num));
                num = 2;
                Choice_num = num;
                //image_Change.restartImage(2);
                //eventTrigger_Kaigan.enabled = false;
                //eventTrigger_Doukutu.enabled = false;
                //eventTrigger_Kawabe.enabled = false;
                //eventTrigger_Kakou.enabled = false;
                //eventTrigger_Sangaku.enabled = false;
                //eventTrigger_Forest.enabled = false;
                //button_Doukutu.enabled = false;
                //button_Forest.enabled = false;
                //button_Kaigan.enabled = false;
                //button_Kakou.enabled = false;
                //button_Kawabe.enabled = false;
                //button_Sangaku.enabled = false;

                buttons[num].GetComponent<Image>().sprite = image_c[num];
                ;
                for (int i = 0; i < buttons.Length; i++)
                {
                    Syupatsu.interactable = true;

                    if (i != num && lockSystem.isOpen[i])
                    {
                        buttons[i].GetComponent<Image>().sprite = image[i];

                    }
                    //Syupatsu.interactable = /*false*/;

                }

                break;
            ////山岳選択
            case 3:

                Debug.Log(("シーン先は" + num));
                num = 3;
                Choice_num = num;
                //image_Change.restartImage(3);
                //eventTrigger_Kaigan.enabled = false;
                //eventTrigger_Doukutu.enabled = false;
                //eventTrigger_Kawabe.enabled = false;
                //eventTrigger_Kakou.enabled = false;
                //eventTrigger_Sangaku.enabled = false;
                //eventTrigger_Forest.enabled = false;
                //button_Doukutu.enabled = false;
                //button_Forest.enabled = false;
                //button_Kaigan.enabled = false;
                //button_Kakou.enabled = false;
                //button_Kawabe.enabled = false;
                //button_Sangaku.enabled = false;
                buttons[num].GetComponent<Image>().sprite = image_c[num];
                ;
                for (int i = 0; i < buttons.Length; i++)
                {
                    Syupatsu.interactable = true;

                    if (i != num && lockSystem.isOpen[i])
                    {
                        buttons[i].GetComponent<Image>().sprite = image[i];

                    }
                    //Syupatsu./*interactable*/ = false;

                }


                break;
            ////火口選択
            case 4:

                Debug.Log(("シーン先は" + num));
                num = 4;
                Choice_num = num;
                //image_Change.restartImage(image_c[num]);
                //eventTrigger_Kaigan.enabled = false;
                //eventTrigger_Doukutu.enabled = false;
                //eventTrigger_Kawabe.enabled = false;
                //eventTrigger_Kakou.enabled = false;
                //eventTrigger_Sangaku.enabled = false;
                //eventTrigger_Forest.enabled = false;
                //button_Doukutu.enabled = false;
                //button_Forest.enabled = false;
                //button_Kaigan.enabled = false;
                //button_Kakou.enabled = false;
                //button_Kawabe.enabled = false;
                //button_Sangaku.enabled = false;
                buttons[num].GetComponent<Image>().sprite = image_c[num];
                ;
                for (int i = 0; i < buttons.Length; i++)
                {
                    Syupatsu.interactable = true;

                    if (i != num && lockSystem.isOpen[i])
                    {
                        buttons[i].GetComponent<Image>().sprite = image[i];

                    }
                    //Syupatsu.interactable = false;

                }

                break;
            ////洞窟選択
            case 5:

                Debug.Log(("シーン先は" + num));
                num = 5;
                Choice_num = num;
                //image_Change.restartImage(5);
                //eventTrigger_Kaigan.enabled = false;
                //eventTrigger_Doukutu.enabled = false;
                //eventTrigger_Kawabe.enabled = false;
                //eventTrigger_Kakou.enabled = false;
                //eventTrigger_Sangaku.enabled = false;
                //eventTrigger_Forest.enabled = false;
                //button_Doukutu.enabled = false;
                //button_Forest.enabled = false;
                //button_Kaigan.enabled = false;
                //button_Kakou.enabled = false;
                //button_Kawabe.enabled = false;
                //button_Sangaku.enabled = false;
                buttons[num].GetComponent<Image>().sprite = image_c[num];
                ;
                for (int i = 0; i < buttons.Length; i++)
                {
                    Syupatsu.interactable = true;

                    if (i != num && lockSystem.isOpen[i])
                    {
                        buttons[i].GetComponent<Image>().sprite = image[i];

                    }
                    //Syupatsu.interactable = false;

                }


                break;

            case 6:
                num = 6;    
                for (int i = 0; i < buttons.Length; i++)
                {
                    Syupatsu.interactable = true;

                    if (i != num && lockSystem.isOpen[i])
                    {
                        buttons[i].GetComponent<Image>().sprite = image[i];

                    }
                    //Syupatsu.interactable = false;

                }

                break;
        }

    }
    public void LoadScene_each()
    {
        if (Choice_num == 0)
        {
            fading.Fade(Fading.type.FadeOut);
            fading.OnFadeEnd.AddListener(() =>SceneManager.LoadScene(nextScene[Choice_num]));

        }
        else if (Choice_num == 1)
        {

            fading.Fade(Fading.type.FadeOut);
            fading.OnFadeEnd.AddListener(() => SceneManager.LoadScene(nextScene[Choice_num]));
        }
        else if (Choice_num == 2)
        {

            fading.Fade(Fading.type.FadeOut);
            fading.OnFadeEnd.AddListener(() => SceneManager.LoadScene(nextScene[Choice_num]));
        }
        else if (Choice_num == 3)
        {

            fading.Fade(Fading.type.FadeOut);
            fading.OnFadeEnd.AddListener(() => SceneManager.LoadScene(nextScene[Choice_num]));
        }
        else if (Choice_num == 4)
        {

            fading.Fade(Fading.type.FadeOut);
            fading.OnFadeEnd.AddListener(() => SceneManager.LoadScene(nextScene[Choice_num]));
        }
        else if (Choice_num == 5)
        {

            fading.Fade(Fading.type.FadeOut);
            fading.OnFadeEnd.AddListener(() => SceneManager.LoadScene(nextScene[Choice_num]));

        }


    }
  


}
