using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string scenename;

    public void ChangeScene()
    {
        

        SceneManager.LoadScene(scenename);
    }

    
}
