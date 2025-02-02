using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Onoe
{
    public class MouseScore_A_Onoe : MonoBehaviour
    {
        public float rotationSpeed = 5f;    //�}�E�X���I�u�W�F�N�g�����񂷂邽�߂�臒l
        public Score_GameA scoreGame_a;
        Vector2 mousePos = Vector2.zero;
        bool Drag = false;

        private void SetDrag()
        {
            mousePos.x = Mathf.Clamp01(Input.GetAxis("Mouse X"));
            mousePos.y = Mathf.Clamp01(Input.GetAxis("Mouse Y"));
            Drag = true;
        }

        void ScoreUp()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            int scoreIncrease = Mathf.RoundToInt(Mathf.Abs(mouseX) + Mathf.Abs(mouseY) * 10); // �K�؂Ȍv�Z���@�ɕύX
            scoreGame_a.IncreaseScore(scoreIncrease);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetDrag();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Drag = false;
            }

            if (Drag)
            {
                    ScoreUp();
            }
        }
        
    }
}