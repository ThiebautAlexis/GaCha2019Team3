using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SnakePart : MonoBehaviour
{
    [Header("Basic Variables")]
    public SnakeBody m_Body = null;

    [Header("Gameplay Variables")]
    public bool m_CanWalkOnItself = false;
    public Vector2Int m_TilePosition = Vector2Int.zero;
    public Vector2Int m_LastTilePosition = Vector2Int.zero;

    [Header("Shield Variables")]
    public bool m_IsShield = false;
    public float m_ShieldActiveTime = 0f;
    public int m_ShieldTimeLimit = 32;

    public Vector2Int GetTilePosition()
    {
        return m_TilePosition;
    }

    public void InitTilePosition(Vector2Int _TilePosition)
    {
        m_TilePosition = _TilePosition;
        transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(_TilePosition);
        GameData.Instance.m_TileManager.m_MapTile[_TilePosition.x, _TilePosition.y].m_Entities.Add(gameObject);
    }

    public void SetTilePosition(Vector2Int _TilePosition)
    {
        m_LastTilePosition = m_TilePosition;

        GameData.Instance.m_TileManager.m_MapTile[m_LastTilePosition.x, m_LastTilePosition.y].m_Entities.Remove(gameObject);
        m_TilePosition = _TilePosition;

        transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(_TilePosition);
        GameData.Instance.m_TileManager.m_MapTile[m_TilePosition.x, m_TilePosition.y].m_Entities.Add(gameObject);
    }

    public void Hit()
    {
        Debug.Log("Hit !");

        if (!m_IsShield)
        {
            HitEffect();
        }
    }

    virtual public void HitEffect()
    {

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

    public void AddBody()
    {
        if (m_Body == null)
        {
            GameObject newBody = Instantiate(GameData.Instance.m_SnakeBodyPrefab);

            newBody.transform.parent = GameData.Instance.m_EntitiesContainerTransform;           
            newBody.transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(m_TilePosition);

            m_Body = newBody.GetComponent<SnakeBody>();
            m_Body.m_TilePosition = m_TilePosition;

            /// NOT ON THE TILE
        }
        else
        {
            m_Body.m_CanWalkOnItself = m_CanWalkOnItself;
            m_Body.AddBody();
        }
    }

    protected void ActivateShield()
    {
        m_IsShield = true;
    }

    protected void DeactivateShield()
    {
        m_IsShield = false;
    }

    protected void ShieldUpdateTimeAndDeactivate()
    {
        if (m_IsShield)
        {
            m_ShieldActiveTime += Time.deltaTime;

            if (m_ShieldActiveTime > (float)(m_ShieldTimeLimit * GameUpdater.Instance.m_TickEvent))
            {
                DeactivateShield();

                m_ShieldActiveTime = 0;
            }
        }
    }

    private void Update()
    {
        ShieldUpdateTimeAndDeactivate();
    }
}
