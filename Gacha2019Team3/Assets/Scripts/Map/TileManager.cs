using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Header("Basic Variables")]
    public Vector2Int m_MapSize = Vector2Int.zero;
    public CustomTile[,] m_MapTile = null;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTileMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CustomTile GetTile(Vector2Int _TilePosition)
    {
        return m_MapTile[_TilePosition.x, _TilePosition.y];
    }

    private void InitializeTileMap()
    {
        m_MapTile = new CustomTile[m_MapSize.x, m_MapSize.y];

        for (int i = 0; i < m_MapSize.x; i++)
        {
            for (int j = 0; j < m_MapSize.y; j++)
            {
                m_MapTile[i, j] = new CustomTile();
            }
        }
    }
}
