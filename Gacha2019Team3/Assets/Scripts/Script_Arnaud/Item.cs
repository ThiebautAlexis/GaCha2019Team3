using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public static Item instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeEaten()
    {
        if (!ItemManager.Instance.hasItemInStorage)
        {
            ItemManager.Instance.hasItemInStorage = true;
            
        }
        ItemManager.Instance.isItemOnMap = false;
        Destroy(this.gameObject);

    }
}
