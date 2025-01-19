using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Wakimizu : MonoBehaviour
{
    [SerializeField] public NewItem enptyBottle;
    [SerializeField] public NewItem waterBottle;
    //[SerializeField] Texture2D texture;
  //  [SerializeField] Image_Method image_Method;
    [SerializeField]GameObject  image;
    [SerializeField] public Button button;
  //  [SerializeField] public GameObject wakimizuImage;
    public bool wakimizu_f;
    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(false); 
        wakimizu_f = true;
        Texture2D texture = Resources.Load("") as Texture2D;

    }

   

    public void DrinkWater()
    {
        Debug.Log("���ޑO�̐�����" + PlayerInfo.Instance.Thirst);
        if (wakimizu_f)
        {
            PlayerInfo.Instance.Water--;

            image.SetActive (true);
            PlayerInfo.Instance.Thirst += 20;
            wakimizu_f = false;
        }
        else
        {
            Debug.Log("�����N����Ȃ������B");
        }

        Debug.Log("���񂾌�̐�����" + PlayerInfo.Instance.Thirst);

    }

    public void DrawWater()
    {
        if (wakimizu_f)
        {
            if (enptyBottle.CurrentStackCount >= 1)
            {
                waterBottle.CurrentStackCount++;
                PlayerInfo.Instance.Water--;

                Debug.Log("���̃{�g����1���₵�܂���");

                enptyBottle.CurrentStackCount--;
                Debug.Log("��̃{�g����1���炵�܂���");
                // ���\�[�X����Atexture2���擾

                // texture2���g��Sprite������āA���f������
                //button.image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                image.SetActive(true);
            }
            else
            {
                Debug.Log("��̃{�g��������܂���");
            }

        }
        else
        {
            Debug.Log("�����N����Ȃ������B");
        }


    }
}
