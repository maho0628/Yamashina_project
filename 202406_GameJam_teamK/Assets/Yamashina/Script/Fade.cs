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
    [SerializeField] SceneObject Scenename;
    [SerializeField] float a_Down;
    // Start is called before the first frame update
    void Start()
    {
        feadout_f = false;
        //feadPanal.SetActive(true);
        feadPanal.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        color = feadPanal.GetComponent<Image>().color;
        //Debug.Log(feadPanal.GetComponent<Image>().color);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(feadout_f);

        if (!feadout_f)
        {
           FeadIn();
        }
        if(feadout_f)
        {
            FeadOut(Scenename);
        }
        //if(Input.GetKeyDown(KeyCode.A)) 
        //{
        //    feadout_f = true;
        //    //Debug.Log(feadout_f);
        //}
        //Debug.Log(feadout_f);

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
            SceneManager.LoadScene(Scenename);
        }
    }
}
