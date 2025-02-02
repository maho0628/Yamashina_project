using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onoe
{
    public class Handle : MonoBehaviour
    {
        Vector2 Pos;//最初にクリックしたときの位置
        Quaternion rotation;//最初にクリックしたときのオブジェクトの角度

        Vector2 vecA;//オブジェクトの中心からPosへのベクトル
        Vector2 vecB;//オブジェクトの中心からマウス位置へのベクトル

        float angle;//vecAとvecBがなす角度
        Vector3 AxB;//vecAとvecBの外積

        //PointerDownで呼び出す
        //クリックでパラメーターの初期値を求める
        bool Drag;  //ドラッグ中のフラグ
        public void SetPos()
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//マウス位置をワールド座標で取得
                rotation = this.transform.rotation;//オブジェクトの現在の角度を取得
                Drag = true;
            }
        }

        //ハンドルをドラッグしている間に呼び出す
        public void Rotate()
        {
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                Drag = false;
                return;
            }

            vecA = Pos - (Vector2)this.transform.position;//ある地点からのベクトルを求める
            vecB = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            //z座標が悪さをしないためVecter2にしている

            angle = Vector2.Angle(vecA, vecB);//vecAとvecBが成す角度を求める
            AxB = Vector3.Cross(vecA, vecB);//vecAとvecBの外積を求める

            //外積のzの正負で回転方向を決める
            if (AxB.z > 0)
            {
                this.transform.localRotation = rotation * Quaternion.Euler(0, 0, angle);//初期値との掛け算で相対的に回転させる
            }
            else
            {
                this.transform.localRotation = rotation * Quaternion.Euler(0, 0, -angle);//初期値との掛け算で相対的に回転させる
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
