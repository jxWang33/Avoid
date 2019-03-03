using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : MonoBehaviour
{
    public static GameObject map;
    public int hp;
    public RaycastHit debug;
    public bool isPoison;

    public static Color color = Color.green;
    public static Color damagedColor = Color.red;
    private static float damageInterTime = 1.000f;
    private float damageTimeCount;
    private static int maxDamageCount = 5;
    private int remainDamageCount;
    private static int damagePerCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("map");
        GetComponent<SpriteRenderer>().color = color;
        hp = 100;
        isPoison = false;
        remainDamageCount = maxDamageCount;
        damageTimeCount = damageInterTime+0.1f;
        transform.position = new Vector2(2.5f,2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            Clear();
        }
        if (isPoison) {
            if (damageTimeCount > damageInterTime)
            {
                hp -= damagePerCount;
                remainDamageCount--;
                damageTimeCount = 0;
            }
            if (remainDamageCount < 1)
            {
                OutPosion();
            }
            damageTimeCount += Time.deltaTime;
        }
    }
    private void Clear()
    {
        hp = 100;
        isPoison = false;
        remainDamageCount = maxDamageCount;
        damageTimeCount = damageInterTime + 0.1f;
        transform.position = new Vector2(2.5f, 2.5f);
        GetComponent<SpriteRenderer>().color = color;
        //Destroy(GetComponent<Rigidbody2D>());
        //gameObject.AddComponent<Rigidbody2D>();
        GetComponent<BallMove>().Clear();
    }
    public void SetPoison()
    {
        isPoison = true;
        remainDamageCount = maxDamageCount;
        GetComponent<SpriteRenderer>().color = damagedColor;
    }
    public void OutPosion() {
        isPoison = false;
        remainDamageCount = maxDamageCount;
        damageTimeCount = damageInterTime + 0.1f;
        GetComponent<SpriteRenderer>().color = color;
    }
    bool IsDead()
    {
        if (IsZeroHp()||IsOutMap())
        {
            return true;
        }
        return false;
    }
    bool IsZeroHp()
    {
        if (hp <= 0)
        {
            return true;
        }
        return false;
    }
    bool IsOutMap()
    {
        if (transform.position.x < 0 ||transform.position.x>map.GetComponent<Map>().w||
            transform.position.y<0||transform.position.y> map.GetComponent<Map>().h)
        {
            return true;
        }
        //治疗块
        return false;

    }
}
