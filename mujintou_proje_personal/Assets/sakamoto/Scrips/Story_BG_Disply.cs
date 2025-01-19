using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disply_Story_BG : Image_Method
{
    [SerializeField] StoryData storyData;
    // Start is called before the first frame update
    void Start()
    {
        //”wŒi‚ð•`ŽÊ
        PutImage(storyData.BG);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
