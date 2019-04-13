using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SnakePart : MonoBehaviour
{
    [Header("Basic Variables")]
    public SnakeBody m_Body = null;

    [Header("Gameplay Variables")]
    protected Vector2Int m_TilePosition = Vector2Int.zero;

    public Vector2Int GetTilePosition()
    {
        return m_TilePosition;
    }

    public void InitTilePosition(Vector2Int _TilePosition)
    {
        m_TilePosition = _TilePosition;
        transform.position = new Vector3(m_TilePosition.x, m_TilePosition.y, 0) * 0.5f;
        GameData.Instance.m_TileManager.m_MapTile[_TilePosition.x, _TilePosition.y].m_Entities.Add(gameObject);
    }

    public void SetTilePosition(Vector2Int _TilePosition)
    {
        GameData.Instance.m_TileManager.m_MapTile[_TilePosition.x, _TilePosition.y].m_Entities.Remove(gameObject);
        m_TilePosition = _TilePosition;
        transform.position = new Vector3(m_TilePosition.x, -m_TilePosition.y, 0) * 0.5f;
        GameData.Instance.m_TileManager.m_MapTile[_TilePosition.x, _TilePosition.y].m_Entities.Add(gameObject);
    }

    virtual public void Hit()
    {
        Debug.Log("Hit !");
    }
}
