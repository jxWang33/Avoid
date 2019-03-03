using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MapUnit : MonoBehaviour
{
    public GameObject playerBall;
    public Triangle triangle;
    public static Color color = Color.white;
    public bool isInPosion;
    private bool shouldRoll;
    private static float interTime = 1f;
    private float countInterTime;
    private static float PoisonTime = 4f;
    private float countPoisonTime;
    private double randomNum;
    private static double probability=0.75d;
    // Start is called before the first frame update
    public void Set(Triangle t)
    {
        triangle = t;
        Vector3[] temp = { triangle.points[0], triangle.points[1], triangle.points[2], triangle.points[0] };
        GetComponent<LineRenderer>().SetPositions(temp);
    }
    void Start()
    {
        isInPosion = false;
        shouldRoll = true;
        countInterTime = 0;
        countPoisonTime=0;
        randomNum = -1;
        playerBall = GameObject.Find("Circle");
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = color;
        countInterTime += Time.deltaTime;
        System.Random rd = new System.Random(GetRandomSeed());
        if (countInterTime > interTime)
        {
            //Debug.Log(rd.NextDouble());
            //动画 
            if(randomNum>probability)
            {
                GetComponent<SpriteRenderer>().color = Color.yellow;
                if (countInterTime > interTime + PoisonTime)
                {
                    //毒气喷出
                    GetComponent<SpriteRenderer>().color = Color.red;
                    //isInPosion = true;

                    //调Ball
                    //Debug.Log(playerBall.name);
                    if (IsInside(playerBall.transform.position.x, playerBall.transform.position.y))
                    {
                        playerBall.GetComponent<BallState>().SetPoison();
                    }
                    countInterTime = 0;
                    randomNum = rd.NextDouble();
                }
            }
            else
            {
                countInterTime = 0;
                randomNum = rd.NextDouble();
            }
        }

    }
    static int GetRandomSeed()
    {
        byte[] bytes = new byte[4];
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        rng.GetBytes(bytes);
        return BitConverter.ToInt32(bytes, 0);
    }
    bool IsInside(float x, float y)
    {   //三角形边上的点不算在三角形内
        float x1 = triangle.points[0].x;
        float x2 = triangle.points[1].x;
        float x3 = triangle.points[2].x;
        float y1 = triangle.points[0].y;
        float y2 = triangle.points[1].y;
        float y3 = triangle.points[2].y;

        double A = Area(x1, y1, x2, y2, x3, y3);
        double A1 = Area(x, y, x2, y2, x3, y3);
        double A2 = Area(x1, y1, x, y, x3, y3);
        double A3 = Area(x1, y1, x2, y2, x, y);

        return (A == A1 + A2 + A3);
    }
    double Area(float x1, float y1, float x2, float y2, float x3, float y3)
    {
        return System.Math.Abs((x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2.0);
    }
}
