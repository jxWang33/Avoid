using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public struct Triangle {
    public Vector2[] points;
    public Triangle(Vector2[] g) {
        points= new Vector2[3];
        points = g;
    }
};
public class Map : MonoBehaviour
{
    public float w;
    public float h;
    public static int cutCount = 3;
    public float avgArea=0;
    public float debug;
    public GameObject mapUnit;
    private Sprite texture;
    private System.Random rd = new System.Random();

    public List<Triangle> mapUnits = new List<Triangle>();
    // Start is called before the first frame update
    void Start()
    {
        texture = Resources.Load("Triangle", typeof(Sprite)) as Sprite;
        Vector2 temp = new Vector2();
        temp =GetRandomPoint();
        mapUnits.Add(new Triangle(new Vector2[3] {new Vector2(0,0), new Vector2(w, 0), temp }));
        mapUnits.Add(new Triangle(new Vector2[3] {new Vector2(0,0), new Vector2(0, h), temp }));
        mapUnits.Add(new Triangle(new Vector2[3] {new Vector2(w,h), new Vector2(w, 0), temp }));
        mapUnits.Add(new Triangle(new Vector2[3] {new Vector2(w,h), new Vector2(0, h), temp }));

        for(int i = 0; i < cutCount; i++){
            Vector2 rp = GetRandomPoint();
            avgArea = w * h / (4 + i * 2);
            if (!CutTriangle(rp)) {
                i = i - 1;//切割失败
            };
        }
        InitMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
    bool CutTriangle(Vector2 p){
        //取三角形
        Triangle t = new Triangle();
        bool flag = false;//是否取出三角形
        for (int i = 0; i < mapUnits.Count; i++)
        {
            if (IsInside(mapUnits[i], p.x, p.y))
            {
                if (Area(mapUnits[i]) < avgArea)
                    continue;
                t = mapUnits[i];
                mapUnits.Remove(mapUnits[i]);
                flag = true;
                break;
            }
        }

        if (flag == false)
            return false;
        //切割
        mapUnits.Add(new Triangle(new Vector2[3] { t.points[0], t.points[1], p }));
        mapUnits.Add(new Triangle(new Vector2[3] { t.points[0], t.points[2], p }));
        mapUnits.Add(new Triangle(new Vector2[3] { t.points[1], t.points[2], p }));
        return true;
    }
    void InitMap() {
        for (int i = 0; i < mapUnits.Count; i++){
            GameObject temp = GameObject.Instantiate(mapUnit,transform);
            Sprite tempS = GameObject.Instantiate(texture);
            temp.GetComponent<SpriteRenderer>().sprite = tempS;
            temp.GetComponent<SpriteRenderer>().sprite.OverrideGeometry(mapUnits[i].points,
                temp.GetComponent<SpriteRenderer>().sprite.triangles);

            temp.GetComponent<MapUnit>().Set(mapUnits[i]);
        }
    }
    Vector2 GetRandomPoint() {
        Vector2 fin = new Vector2(0,0);
        fin.x = (float)rd.NextDouble()*w;
        fin.y = (float)rd.NextDouble()*h;

        return fin;
    }
    double Area(float x1, float y1, float x2, float y2, float x3, float y3)
    {
        return Math.Abs((x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2.0);
    }
    double Area(Triangle t)
    {
        float x1 = t.points[0].x;
        float x2 = t.points[1].x;
        float x3 = t.points[2].x;
        float y1 = t.points[0].y;
        float y2 = t.points[1].y;
        float y3 = t.points[2].y;
        return Math.Abs((x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2.0);
    }
    bool IsInside(Triangle t,float x, float y)
    {   //三角形边上的点不算在三角形内
        float x1 = t.points[0].x;
        float x2 = t.points[1].x;
        float x3 = t.points[2].x;
        float y1 = t.points[0].y;
        float y2 = t.points[1].y;
        float y3 = t.points[2].y;

        double A = Area(x1, y1, x2, y2, x3, y3);
        double A1 = Area(x, y, x2, y2, x3, y3);
        double A2 = Area(x1, y1, x, y, x3, y3);
        double A3 = Area(x1, y1, x2, y2, x, y);
        if (A == 0 || A1 == 0 || A2 == 0 || A3 == 0)
            return false;

        return (A == A1 + A2 + A3);
    }
}
