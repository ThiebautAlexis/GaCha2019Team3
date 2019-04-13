using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float time;
    int showtime;

    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            this.gameObject.GetComponent<Text>().text = "WIN";
        }
        else
        {
            showtime = (int)time;
            this.gameObject.GetComponent<Text>().text = showtime.ToString();
        }
    }
}
