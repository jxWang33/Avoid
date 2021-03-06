﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public float currentV;
    [SerializeField]
    private float maxVelocity;
    private static float k = 2.0f;
    private static float u = 0.1f;
    [SerializeField]
    private Vector2 horF;
    [SerializeField]
    private Vector2 verF;

    enum Dir { Minus,Zero,Positive};
    [SerializeField]
    private Dir onUp;
    [SerializeField]
    private Dir onLeft;

    // Start is called before the first frame update
    void Start()
    {
        maxVelocity = 5.0f;
        horF = new Vector2(k, 0);
        verF = new Vector2(0, k);
        onUp = Dir.Zero;
        onLeft = Dir.Zero;
        maxVelocity = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Response();
    }
    void Response()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) { onUp = Dir.Minus; }
        if (Input.GetKeyDown(KeyCode.UpArrow))  { onUp = Dir.Positive; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { onLeft = Dir.Minus; }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { onLeft = Dir.Positive; }

        if (Input.GetKeyUp(KeyCode.DownArrow)) { onUp = Dir.Zero; if (Input.GetKey(KeyCode.UpArrow)) onUp = Dir.Positive; }
        if (Input.GetKeyUp(KeyCode.UpArrow)) { onUp = Dir.Zero; if (Input.GetKey(KeyCode.DownArrow)) onUp = Dir.Minus; }
        if (Input.GetKeyUp(KeyCode.RightArrow)) { onLeft = Dir.Zero; if (Input.GetKey(KeyCode.LeftArrow)) onLeft = Dir.Positive; }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) { onLeft = Dir.Zero; if (Input.GetKey(KeyCode.RightArrow)) onLeft = Dir.Minus; }

        if (onUp == Dir.Minus) {
            if (Vector2.Dot(-verF, GetComponent<Rigidbody2D>().velocity) < 0 || GetComponent<Rigidbody2D>().velocity.magnitude < maxVelocity) 
                GetComponent<Rigidbody2D>().AddForce(-verF);
        }
        if (onUp == Dir.Positive){
            if (Vector2.Dot(verF, GetComponent<Rigidbody2D>().velocity) < 0 || GetComponent<Rigidbody2D>().velocity.magnitude < maxVelocity)
                GetComponent<Rigidbody2D>().AddForce(verF);
        }
        if (onLeft == Dir.Minus){
            if (Vector2.Dot(-horF, GetComponent<Rigidbody2D>().velocity) < 0 || GetComponent<Rigidbody2D>().velocity.magnitude < maxVelocity)
                GetComponent<Rigidbody2D>().AddForce(horF);
        }
        if (onLeft == Dir.Positive){
            if (Vector2.Dot(horF, GetComponent<Rigidbody2D>().velocity) < 0 || GetComponent<Rigidbody2D>().velocity.magnitude < maxVelocity)
                GetComponent<Rigidbody2D>().AddForce(-horF);
        }
        currentV = GetComponent<Rigidbody2D>().velocity.magnitude;
        GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity.normalized * -u);
    }
    public void Clear() {
        maxVelocity = 5.0f;
        horF = new Vector2(k, 0);
        verF = new Vector2(0, k);
        onUp = Dir.Zero;
        onLeft = Dir.Zero;
        maxVelocity = 5.0f;
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }
}
