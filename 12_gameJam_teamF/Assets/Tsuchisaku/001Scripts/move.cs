using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public bool isRight;

    public Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (isRight)
        {
            transform.position += movement;
        }
        else
        {
            transform.position -= movement;
        }
    }
      
}
