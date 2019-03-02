using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MapUnit : MonoBehaviour
{
    public Triangle triangle;
    public Color color = Color.white;
    public bool isInPosion=false;
    // Start is called before the first frame update
    public void Set(Triangle t) {
        triangle = t;
        Vector3[] temp= { triangle.points[0], triangle.points[1], triangle.points[2], triangle.points[0] };
        GetComponent<LineRenderer>().SetPositions(temp);
    }
    void Start()
    {
       
    }

    void SetPosion(object source, System.Timers.ElapsedEventArgs e)
    {
    }

    // Update is called once per frame
    void Update()
    {
        System.Random rd = new System.Random();
        Debug.Log(rd.NextDouble());
        GetComponent<SpriteRenderer>().color = Color.red;
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
