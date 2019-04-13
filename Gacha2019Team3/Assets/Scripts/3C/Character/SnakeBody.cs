using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : SnakePart
{
    private void Start()
    {
        gameObject.AddComponent<SpriteRenderer>().sprite = GameData.Instance.m_SnakeBodySprite;
    }

    override public void Hit()
    {
        base.Hit();

        if (m_Body != null)
        {
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
}