using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keizoku_Botton : MonoBehaviour
{
    [SerializeField] GameObject roadImage;
    [SerializeField] Sentakushi_Method sentakushi_Method;
    [SerializeField] Fade fade;

    // Start is called before the first frame update
    public void onContinue()
    {
        roadImage.SetActive(true);
        Invoke("BackIvent", 2);
    }
    public void onBackHome()
    {
        fade.scene_name_num = 2;
        //PlayerInfo.Instance.DoAction();
        fade.feadout_f = true;        

    }

    private void BackIvent()
    {
        sentakushi_Method.GoToEnding();
    }
}
