using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public ShakeCamera m_ShakeBehavior = null;

    // Start is called before the first frame update
    void Start()
    {
        m_ShakeBehavior = GetComponent<ShakeCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
