﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public Transform target;
    public Vector3 offset;
    public float speed = 20;

    public Camera Camera => Camera.main;
    // Start is called before the first frame update
    void Start()
    {
        // target = transform của Player
    }

    // Update is called once per frame
    void Update()
    {
        // Thay đổi vị trí theo Player
        //transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
}
