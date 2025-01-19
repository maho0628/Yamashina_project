
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange_kyoten : Image_Method
{
    int image_num;
    public KyotenToOtherspace kyotenToOtherspace;
    public Image TakibiImage;
    [SerializeField] public Sprite Takibi_off;
    [SerializeField] public Sprite Takibi_on;

    void Start()
    {
        
        image_num = 0;
        if (PlayerInfo.Instance.Fire <= 0)
        {
            TakibiImage.sprite = Takibi_off;
        }
        else
        {
            TakibiImage.sprite = Takibi_on;
        }
        // PutImage(image);
    }
    public void ImageChange()
    {
        Debug.Log("ƒ{ƒ^ƒ“‰Ÿ‚µ‚½");
     //   Debug.Log(kyotenToOtherspace.takibi);
        if (kyotenToOtherspace.takibi_f == false)
        {
            Debug.Log("ƒ{ƒ^ƒ“‰Ÿ‚µ‚½‚Q");
            int rand_num;
            rand_num = Random.Range(0, 3) ;
            if (rand_num == 3) rand_num = 2;
            if (rand_num == 0)
            {
                if (image_num == 0 && PlayerInfo.Instance.Fire==0)
                {
                    TakibiImage.sprite = Takibi_on;
                    //PutImage(Image2);
                    //Remove_Image(image);
                    image_num = 1;
                    PlayerInfo.Instance.Fire = 100;
                    kyotenToOtherspace.takibi_f = true;
                    Debug.Log("¬Œ÷");


                }
            }
            else {
             //if (image_num == 1 && kyotenToOtherspace.takibi > 0)
             //   {
                    TakibiImage.sprite = Takibi_off;
                    //PutImage(image);
                    //Remove_Image(Image2);
                    image_num = 0;
                 //   Debug.Log(PlayerInfo.Instance.Fire);
                    Debug.Log("Ž¸”s");

                //}
                //Debug.Log(kyotenToOtherspace.takibi);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            PlayerInfo.Instance.Fire = 0;
        // Debug.Log(PlayerInfo.Instance.Fire);
        }
    }

}


