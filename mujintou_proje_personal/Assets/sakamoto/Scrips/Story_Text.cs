using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//using System.Diagnostics;

public class Story_Text : Text_Method
{
    [SerializeField] StoryData storyData;
    [SerializeField] string sceneName; 
    [SerializeField] Fade fade;
    int line_num;

    public bool Botton_f;
    // Start is called before the first frame update
    void Start()
    {
        Botton_f = true;
        //Invoke();
        line_num = 1;
        Text_Disply(storyData.FarstLine);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Botton_f)
            {

                Botton_f = false;
                if (!fade.feadout_f)
                {
                    Invoke("Next_Text", 1.0f);

                    if (!fade.feadout_f)
                    {
                        Botton_f = true;
                    }
                }
                else
                {
                    Invoke("Next_Text", 0);
                }

            }
        }
    }
    void Next_Text()
    {
        line_num++;
        if (line_num == 2)
        {
            if (storyData.ScondLine == "NULL")
            {
                fade.feadout_f = true;
            }
            else
            {
            Text_Disply(storyData.ScondLine);

            }

        }
        if (line_num == 3) 
            
        {

            if (storyData.ThirdLine == "NULL")
            {
                fade.feadout_f = true;
            }
            else
            {
            Text_Disply(storyData.ThirdLine);

            }

        }
        if (line_num == 4)
        {
            if (storyData.FourthLine == "NULL")
            {
                fade.feadout_f = true;
            }
            Text_Disply(storyData.FourthLine);

        }
        if (line_num == 5)
        {
            if (storyData.FifthLine == "NULL")
            {
                fade.feadout_f = true;
            }
            Text_Disply(storyData.FifthLine);

        }
        if (line_num == 6)
        {
            if (storyData.SixthLine == "NULL")
            {
                fade.feadout_f = true;
            }
            Text_Disply(storyData.SixthLine);

        }
        if (line_num == 7)
        {
            if (storyData.SeventhLine == "NULL")
            {
                fade.feadout_f = true;
            }
            Text_Disply(storyData.SeventhLine);

        }
        if (line_num == 8)
        {
            if (storyData.EightLine == "NULL")
            {
                fade.feadout_f = true;
            }
            Text_Disply(storyData.EightLine);

        }
        if (line_num == 9)
        {
            if (storyData.NinethLine == "NULL")
            {
                fade.feadout_f = true;
            }
            Text_Disply(storyData.NinethLine);

        }
        if (line_num == 10)
        {
            Text_Disply(storyData.TenthLine);
            Debug.Log("ƒ^ƒCƒgƒ‹‚É–ß‚é");
        }


    }



    private void OnDestroy()
    {
        // DestroyŽž‚É“o˜^‚µ‚½Invoke‚ð‚·‚×‚ÄƒLƒƒƒ“ƒZƒ‹
        CancelInvoke();
    }
    public void Invoke()
    {
        Invoke(nameof(LoadingScene), 2.5f);

    }
    public void LoadingScene()//Scene‚Ì‘JˆÚ
    {
        SceneManager.LoadScene(sceneName);

    }
}
