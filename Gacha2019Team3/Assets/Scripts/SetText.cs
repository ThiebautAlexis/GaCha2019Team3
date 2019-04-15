using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    public GameObject wintext;
    public GameObject scoretext;

    void Start()
    {
        if (EndData.Instance.win == true)
        {
            wintext.GetComponent<Text>().text = "YOU WON !";
            scoretext.GetComponent<Text>().text = "Your score is : " + EndData.Instance.score;
        }
        else
        {
            wintext.GetComponent<Text>().text = "YOU DIED !";
            scoretext.GetComponent<Text>().text = "Your lasted " + EndData.Instance.time + "second !";
        }
    }
}

