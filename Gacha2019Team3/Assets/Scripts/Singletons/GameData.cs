using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : Singleton<GameData>
{
    [Header("Hand filled datas")]
    public int m_MapSizeX = 20;
    public int m_MapSizeY = 20;
    public int m_PlayerCount = 1;

    [Header("Snake Prefabs")]
    public GameObject m_SnakeHeadPrefab = null;
    public GameObject m_SnakeBodyPrefab = null;
    public GameObject m_SnakeQueuePrefab = null;

    [Header("Basic Variables")]
    public TileManager m_TileManager = null;

    [Header("Misc Variables")]
    public GameObject m_DebugBackground;
    public Sprite m_SnakeHeadSprite;
    public Sprite m_SnakeBodySprite;

    public Sprite m_BackgroundSprite = null;

    public List<SnakeHead> m_Players = new List<SnakeHead>();

    private void Awake()
    {
        m_TileManager = new TileManager(new Vector2Int(m_MapSizeX, m_MapSizeY));

        m_DebugBackground.transform.localScale = Vector3.one * (m_MapSizeX * 0.5f);
       
    }

}
