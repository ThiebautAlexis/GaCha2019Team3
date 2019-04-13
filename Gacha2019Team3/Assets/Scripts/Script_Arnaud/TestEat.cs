using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Item.instance.BeEaten();
        }
    }
}
