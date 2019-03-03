using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{

    int hour;
    int minute;
    int second;
    int millisecond;
    public Text test_chenggong;
    // 已经花费的时间
    float timeSpend = 0.0f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeSpend += Time.deltaTime;
        //GlobalSetting.timeSpent = timeSpend;
        hour = (int)timeSpend / 3600;
        minute = ((int)timeSpend - hour * 3600) / 60;
        second = (int)timeSpend - hour * 3600 - minute * 60;
        millisecond = (int)((timeSpend - (int)timeSpend) * 1000);
        GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", hour, minute, second, millisecond);
    }
    void OnTriggerStay(Collider other)
    {
    }
}