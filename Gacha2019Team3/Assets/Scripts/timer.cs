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
        showtime = (int)time;
        EndData.Instance.UpdateTime(/*maxtime - */showtime);
        this.gameObject.GetComponent<Text>().text = string.Format("{0:#0}:{1:00}", Mathf.Floor(showtime / 60), Mathf.Floor(showtime) % 60);


        if (showtime % 20 == 0 && flag && showtime < 31)
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
