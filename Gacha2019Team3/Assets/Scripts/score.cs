using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class score : MonoBehaviour
{

    int scoring;
    
    public int bodies = 1;
    bool done = false;

    void Update()
    {
        if (done == false)
        {
            done = true;
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        bodies = GameData.Instance.m_Players[0].m_Size;
        scoring = (scoring + bodies + 1);
        EndData.Instance.UpdateScore(scoring);
        this.gameObject.GetComponent<Text>().text = scoring.ToString();
        done = false;
        yield return null;
    }
}
