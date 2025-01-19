using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class AspectKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Camera targetCamera; //�ΏۂƂ���J����
    [SerializeField]
    private Vector2 aspectVec; //�ړI�𑜓x4

    void Update()
    {
        var screenAspect = Screen.width / (float)Screen.height; //��ʂ̃A�X�y�N�g��1920/1080 1.77777
        var targetAspect = aspectVec.x / aspectVec.y; //�ړI�̃A�X�y�N�g��  1280/720
        var magRate = targetAspect / screenAspect; //�ړI�A�X�y�N�g��ɂ��邽�߂̔{��
        var viewportRect = new Rect(0, 0, 1, 1); //Viewport�����l��Rect���쐬
        if (magRate < 1)
        {
            viewportRect.width = magRate; //�g�p���鉡����ύX
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;//������
        }
        else
        {
            viewportRect.height = 1 / magRate; //�g�p����c����ύX
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;//�����]��
        }
        targetCamera.rect = viewportRect; //�J������Viewport�ɓK�p
    }


        
}

