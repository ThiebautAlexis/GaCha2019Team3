using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndData : Singleton<EndData>
{
    public int score;
    public int time;
    public bool win = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateScore(int newscore)
    {
        score = newscore;
    }

    public void UpdateTime(int newtime)
    {
        time = newtime;
    }
}
