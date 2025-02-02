using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onoe
{
    public class Handle : MonoBehaviour
    {
        Vector2 Pos;//�ŏ��ɃN���b�N�����Ƃ��̈ʒu
        Quaternion rotation;//�ŏ��ɃN���b�N�����Ƃ��̃I�u�W�F�N�g�̊p�x

        Vector2 vecA;//�I�u�W�F�N�g�̒��S����Pos�ւ̃x�N�g��
        Vector2 vecB;//�I�u�W�F�N�g�̒��S����}�E�X�ʒu�ւ̃x�N�g��

        float angle;//vecA��vecB���Ȃ��p�x
        Vector3 AxB;//vecA��vecB�̊O��

        //PointerDown�ŌĂяo��
        //�N���b�N�Ńp�����[�^�[�̏����l�����߂�
        bool Drag;  //�h���b�O���̃t���O
        public void SetPos()
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//�}�E�X�ʒu�����[���h���W�Ŏ擾
                rotation = this.transform.rotation;//�I�u�W�F�N�g�̌��݂̊p�x���擾
                Drag = true;
            }
        }

        //�n���h�����h���b�O���Ă���ԂɌĂяo��
        public void Rotate()
        {
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                Drag = false;
                return;
            }

            vecA = Pos - (Vector2)this.transform.position;//����n�_����̃x�N�g�������߂�
            vecB = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            //z���W�����������Ȃ�����Vecter2�ɂ��Ă���

            angle = Vector2.Angle(vecA, vecB);//vecA��vecB�������p�x�����߂�
            AxB = Vector3.Cross(vecA, vecB);//vecA��vecB�̊O�ς����߂�

            //�O�ς�z�̐����ŉ�]���������߂�
            if (AxB.z > 0)
            {
                this.transform.localRotation = rotation * Quaternion.Euler(0, 0, angle);//�����l�Ƃ̊|���Z�ő��ΓI�ɉ�]������
            }
            else
            {
                this.transform.localRotation = rotation * Quaternion.Euler(0, 0, -angle);//�����l�Ƃ̊|���Z�ő��ΓI�ɉ�]������
            }
        }

        private void Update()
        {
            if (Drag) 
            {
                Rotate();
            }
            else
            {
                SetPos();
            }
        }
    }
}
