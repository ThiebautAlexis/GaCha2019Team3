using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomTile : MonoBehaviour
{
    [Header("Basic Variables")]
    public List<GameObject> m_Entities = null;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTileMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeTileMap()
    {
        m_Entities = new List<GameObject>();
    }
}
