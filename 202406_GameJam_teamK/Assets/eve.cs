using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eve : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     multiAudio multi= GameObject.FindAnyObjectByType<multiAudio>().GetComponent<multiAudio>();
        multi.ChooseSongs_BGM(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
