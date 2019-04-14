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

        if (showtime == 5 && flag)
        {
            GameData.Instance.expand(1);
            flag = false;
        }
    }
}
