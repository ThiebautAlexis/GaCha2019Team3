using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UseItem();
    }

    void UseItem()
    {
        if (Input.GetButton("Power1"))
        {
            ItemManager.Instance.AddLength();
        }

        if (Input.GetButton("Power2"))
        {
            ItemManager.Instance.Protect();
        }

        if (Input.GetButton("Power3"))
        {
            ItemManager.Instance.FireShot();
        }
    }
}
