using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : MonoBehaviour
{

    public GameObject circle;
    public static GameObject map;
    public int hp;
    public RaycastHit debug;
    public bool isPoison;

    private static float interTime = 1.000f;
    private float timeCount;
    private static int maxCount = 5;
    private int remainCount;
    private static int damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("map");
        hp = 100;

        isPoison = false;
        remainCount = maxCount;
        timeCount = interTime+0.1f;

        transform.position = new Vector3(2.5f,2.5f,-10f);
        this.gameObject.name = "circle";
        GetComponent<BallMove>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            Destroy(this.gameObject);
            GameObject g = (GameObject)Instantiate(circle);
            
            g.GetComponent<BallState>().enabled = true;
        }
        if (isPoison) {
            if (timeCount > interTime)
            {
                hp -= damage;
                remainCount--;
                timeCount = 0;
            }
            if (remainCount < 1)
            {
                isPoison = false;
                remainCount = maxCount;
                timeCount = interTime + 0.1f;
            }
            timeCount += Time.deltaTime;
        }
    }
    public void SetPoison()
    {
        isPoison = true;
        remainCount = maxCount;
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
