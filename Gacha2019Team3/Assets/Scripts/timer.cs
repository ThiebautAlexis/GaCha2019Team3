using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    public float time;
    //int maxtime;
    int showtime;
    public string scenename;
    bool flag = true;
    int phase = 0;

    private void Start()
    {
        //maxtime = (int)time;
    }

    void Update()
    {
        time += Time.deltaTime;

        /*if (time <= 0)
        {
            EndData.Instance.win = true;
            SceneManager.LoadScene(scenename);
        }
        else
        {*/
            showtime = (int)time;
            EndData.Instance.UpdateTime(/*maxtime - */showtime);
            this.gameObject.GetComponent<Text>().text = showtime.ToString();
        //}

        if (showtime % 10 == 0 && flag && showtime < 31)
        {
            phase++;
            GameData.Instance.expand(phase);
            flag = false;
        }
        else if (showtime % 10 == 1)
        {
            flag = true;
        }
    }
}
