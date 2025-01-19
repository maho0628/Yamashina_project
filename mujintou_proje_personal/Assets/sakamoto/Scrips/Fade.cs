using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Fade : MonoBehaviour
{
    [SerializeField] GameObject feadPanal;
    Color color;
    public bool feadout_f;
    public int scene_name_num;
    [SerializeField] SceneObject[] Scenename_list;
    [SerializeField] float a_Down;
    // Start is called before the first frame update
    void Start()
    {
        scene_name_num = 0;
        feadout_f = false;
        feadPanal.SetActive(true);
        feadPanal.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        color = feadPanal.GetComponent<Image>().color;
        //Debug.Log(feadPanal.GetComponent<Image>().color);
    }

    // Update is called once per frame
    void Update()
    {
        if (!feadout_f)
        {
           FeadIn();
        }
        if(feadout_f)
        {
            FeadOut(Scenename_list[scene_name_num]);
        }
        //if(Input.GetKeyDown(KeyCode.A)) 
        //{
        //    feadout_f = true;
        //    //Debug.Log(feadout_f);
        //}
        
    }
    void FeadIn()
    {
        if(!(color.a <= 0))
        {
            //Debug.Log(feadPanal.GetComponent<Image>().color);
            color.a -= (a_Down * Time.deltaTime);
            feadPanal.GetComponent<Image>().color = color;
            //Debug.Log(feadPanal.GetComponent<Image>().color);
            if(color.a <= 0)
            {
                feadPanal.SetActive(false);
            }

            
        }
    }
    void FeadOut(string scene_name)
    {
        feadPanal.SetActive(true);
        if((color.a < 1))
        {
            color.a += (a_Down * Time.deltaTime); 
            feadPanal.GetComponent<Image>().color = color;
        }
        if(color.a >= 1)
        {
            SceneManager.LoadScene(scene_name);
        }
    }
}
