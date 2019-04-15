using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager
{
    [Header("Basic Variables")]
    Vector2Int m_MapSize = Vector2Int.zero;
    CustomTile[,] m_MapTile = null;
    public float m_CellSize = 1.0f;
    public Vector2Int m_RestrictedMap1 = Vector2Int.zero;
    public Vector2Int m_RestrictedMap2 = Vector2Int.zero;

    public TileManager(Vector2Int _MapSize)
    {
        m_MapSize = _MapSize;
        m_MapTile = new CustomTile[m_MapSize.x, m_MapSize.y];

        for (int i = 0; i < m_MapSize.x; i++)
        {
            for (int j = 0; j < m_MapSize.y; j++)
            {
                m_MapTile[i, j] = new CustomTile();
            }
        }
    }

    public CustomTile[,] GetRestrictedMap()
    {
        CustomTile[,] copy = new CustomTile[GetRestrictedMapSize().x, GetRestrictedMapSize().y];
        for (int i = m_RestrictedMap1.x; i <= m_RestrictedMap2.x; i++)
        {
            for (int j = m_RestrictedMap1.y; j <= m_RestrictedMap2.y; j++)
            {
                copy[i, j] = m_MapTile[i, j];
            }
        }

        return copy;
    }

    public Vector2Int GetFullMapSize()
    {
        return m_MapSize;
    }

    public Vector2Int GetRestrictedMapSize()
    {
        return (m_RestrictedMap2 - m_RestrictedMap1) + Vector2Int.one;
    }

    public CustomTile GetTile(Vector2Int _TilePosition)
    {
        if ((_TilePosition.x >= 0 && _TilePosition.x <= m_MapSize.x - 1) && (_TilePosition.y >= 0 && _TilePosition.y <= m_MapSize.y - 1))
        {
            return m_MapTile[_TilePosition.x, _TilePosition.y];
        }

        return null;
    }

    public List<CustomTile> GetEmptyTiles()
    {
        List<CustomTile> emptyTiles = new List<CustomTile>();

        for (int i = m_RestrictedMap1.x; i <= m_RestrictedMap2.x; i++)
        {
            for (int j = m_RestrictedMap1.y; j <= m_RestrictedMap2.y; j++)
            {
                if (m_MapTile[i, j].m_Entities.Count == 0)
                {
                    emptyTiles.Add(m_MapTile[i, j]);
                }
            }
        }

        return emptyTiles;
    }

    public Vector2Int GetPosition(CustomTile _Tile)
    {
        for (int i = m_RestrictedMap1.x; i <= m_RestrictedMap2.x; i++)
        {
            for (int j = m_RestrictedMap1.y; j <= m_RestrictedMap2.y; j++)
            {
                if (m_MapTile[i, j] == _Tile)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }

    public Vector3 TilePositionToWorldPosition(Vector2Int _TilePosition)
    {
        return new Vector3(_TilePosition.x * m_CellSize, 0, -_TilePosition.y * m_CellSize);
    }
}
