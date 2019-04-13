using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : Singleton<GameData>
{
    [Header("Hand filled datas")]
    public int m_TileMapSize = 20;

    public GameObject m_DebugBackground;
    public Sprite m_SnakeHeadSprite;
    public Sprite m_SnakeBodySprite;

    [Header("Basic Variables")]
    public TileManager m_TileManager = null;
    public List<SnakeHead> m_Players = new List<SnakeHead>();

    public Sprite m_BackgroundSprite = null;

    private void Awake()
    {
        m_TileManager = new TileManager(new Vector2Int(m_TileMapSize, m_TileMapSize));
        m_DebugBackground.transform.localScale = Vector3.one * (m_TileMapSize * 0.5f);
    }
}
