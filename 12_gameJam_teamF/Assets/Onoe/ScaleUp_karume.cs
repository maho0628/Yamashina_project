using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onoe
{
    public class ScaleUp_karume_S : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("ScaleUp");
        }

        //スケールアップ
        IEnumerator ScaleUp()
        {
            for(float i = 1; i < 1.5; i += 0.1f)
            {
                transform.localScale = new Vector3(i, i, i);
                yield return new WaitForSeconds(i);
            }
        }
    }
}
