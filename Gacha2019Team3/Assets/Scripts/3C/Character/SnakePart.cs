using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SnakePart : MonoBehaviour
{
    [Header("Basic Variables")]
    public SnakeBody m_Body = null;

    [Header("Gameplay Variables")]
    public Vector2Int m_TilePosition = Vector2Int.zero;

    public Vector2Int m_LastTilePosition = Vector2Int.zero;

    public Vector2Int GetTilePosition()
    {
        return m_TilePosition;
    }

    public void InitTilePosition(Vector2Int _TilePosition)
    {
        m_TilePosition = _TilePosition;
        transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(_TilePosition);
        GameData.Instance.m_TileManager.GetRestrictedMap()[_TilePosition.x, _TilePosition.y].m_Entities.Add(gameObject);
    }

    public void SetTilePosition(Vector2Int _TilePosition)
    {
        m_LastTilePosition = m_TilePosition;
        CustomTile[,] test = GameData.Instance.m_TileManager.GetRestrictedMap();
        GameData.Instance.m_TileManager.GetRestrictedMap()[m_LastTilePosition.x, m_LastTilePosition.y].m_Entities.Remove(gameObject);
        m_TilePosition = _TilePosition;

        transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(_TilePosition);
        GameData.Instance.m_TileManager.GetRestrictedMap()[m_TilePosition.x, m_TilePosition.y].m_Entities.Add(gameObject);
    }

    virtual public void Hit()
    {
        Debug.Log("Hit !");
    }

    public void RemoveParts()
    {
        if (m_Body != null)
        {
            m_Body.RemoveParts();
        }

        // Maybe remove if from tile ?
        //TileManager

        Destroy(this);
    }

    [ContextMenu("AddBody")]
    public void AddBody()
    {
        if (m_Body == null)
        {
            GameObject newBody = Instantiate(GameData.Instance.m_SnakeBodyPrefab);
            newBody.transform.Rotate(new Vector3(-90, 0, 0));
            newBody.transform.parent = GameData.Instance.m_EntitiesContainerTransform;
            
            newBody.transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(m_LastTilePosition);

            m_Body = newBody.GetComponent<SnakeBody>();
        }
        else
        {
            m_Body.AddBody();
        }
    }
}
