﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager
{
    [Header("Basic Variables")]
    public Vector2Int m_MapSize = Vector2Int.zero;
    public CustomTile[,] m_MapTile = null;

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

    public CustomTile GetTile(Vector2Int _TilePosition)
    {
        return m_MapTile[_TilePosition.x, _TilePosition.y];
    }

    public List<CustomTile> GetEmptyTiles()
    {
        List<CustomTile> emptyTiles = new List<CustomTile>();
         
        for (int i = 0; i < m_MapSize.x; i++)
        {
            for (int j = 0; j < m_MapSize.y; j++)
            {
                if (m_MapTile[i,j].m_Entities.Count == 0)
                {
                    emptyTiles.Add(m_MapTile[i, j]);
                }
            }
        }

        return emptyTiles;
    }

    public Vector2Int GetPosition(CustomTile _Tile)
    {
        for (int i = 0; i < m_MapSize.x; i++)
        {
            for (int j = 0; j < m_MapSize.y; j++)
            {
                if (m_MapTile[i, j] == _Tile)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1,-1);
    }

    public Vector3 TilePositionToWorldPosition(Vector2Int _TilePosition)
    {
        return new Vector3(_TilePosition.x * 0.5f, 0, -_TilePosition.y * 0.5f);
    }
}
