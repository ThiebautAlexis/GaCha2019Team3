using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : SnakePart
{
    public bool m_CanBeDestroyed = false;
    public GameObject m_FXDamagePrefab = null;

    private void Update()
    {
        if (m_CanBeDestroyed)
        {
            GameData.Instance.m_Camera.m_ShakeBehavior.LaunchCameraShake(1f, 0.1f, 5f);

            GameObject fx = Instantiate(m_FXDamagePrefab, transform.position, Quaternion.identity);       
            Destroy(fx, m_FXDamagePrefab.GetComponent<ParticleSystem>().main.duration);

            GameData.Instance.m_TileManager.GetTile(m_TilePosition).m_Entities.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    override public void HitEffect()
    {
        base.HitEffect();

        m_CanBeDestroyed = true;

        if (m_Body != null)
        {
            CanBeDestroyed();
            m_Body = null;
        }
    }

    public void Move(Vector2Int _NewPostion)
    {
        Vector2Int previousPos = m_TilePosition;

        SetTilePosition(_NewPostion);

        if (m_Body != null)
        {
            m_Body.Move(previousPos);
        }
    }

    public int Count()
    {
        if (m_Body != null)
        {
            return m_Body.Count() + 1;
        }

        return 1;
    }

    public void CanBeDestroyed()
    {
        m_CanBeDestroyed = true;
        if (m_Body != null)
        {
            m_Body.CanBeDestroyed();
        }
    }
}