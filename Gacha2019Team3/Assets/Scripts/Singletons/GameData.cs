using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : Singleton<GameData>
{
    [Header("Basic Variables")]
    public int m_MapSizeX = 20;
    public int m_MapSizeY = 20;
    public int m_PlayerCount = 1;
    public TileManager m_TileManager = null;

    [Header("Snake Prefabs")]
    public GameObject m_SnakeHeadPrefab = null;
    public GameObject m_SnakeBodyPrefab = null;
    public GameObject m_SnakeQueuePrefab = null;
    public GameObject m_SnakeProjectilePrefab = null;

    [Header("Misc Variables")]
    public GameObject m_Background;
    public GameObject m_Ground;

    public List<SnakeHead> m_Players = new List<SnakeHead>();

    public Transform m_EntitiesContainerTransform = null;

    private void Awake()
    {
        m_TileManager = new TileManager(new Vector2Int(m_MapSizeX, m_MapSizeY));

        /// A REVOIR
        m_Ground.transform.localScale = Vector3.one * (m_MapSizeX * 0.5f);   
    }

}
