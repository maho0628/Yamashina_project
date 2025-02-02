using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitCircle : MonoBehaviour
{
    public float lifeTime;
    private float timer;


    public GameObject ObjectHitEffect;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }

    }

   
}
