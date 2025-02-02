using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toshiki;

public class CS_MoveMato : MonoBehaviour, IHit
{
    enum Direction
    {
        Right,
        Left
    }

    [SerializeField, Header("ˆÚ“®‘¬“x")]
    private float speed = 0.5f;
    public float Speed
    {
        set
        {
            speed = value;
        }
    }

    // •ûŒü
    private Direction dir = Direction.Right;

    void Start()
    {
        if(transform.position.x > 0)
        {
            dir = Direction.Left;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(dir == Direction.Right)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            if(transform.position.x > 9)
            {
                Destroy(gameObject);
            }
        }
        if (dir == Direction.Left)
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            if (transform.position.x < -9)
            {
                Destroy(gameObject);
            }

        }

    }

    void IHit.Hit()
    {

    }
}
