using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : SnakePart
{
    public bool m_CanBeDestroyed = false;
    public GameObject m_FXDamage = null;

    private void Start()
    {

    }

    private void Update()
    {
        if (m_CanBeDestroyed)
        {
            Instantiate(m_FXDamage, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    override public void HitEffect()
    {
        base.HitEffect();

        CanBeDestroyed();

        if (m_Body != null)
        {
            m_Body.CanBeDestroyed();
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
    }
}