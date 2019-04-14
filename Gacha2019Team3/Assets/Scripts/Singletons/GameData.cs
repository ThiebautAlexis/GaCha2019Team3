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
    public GameObject m_SnakeProjectilePrefab = null;

    [Header("Background & Ground")]
    public GameObject m_BackgroundPrefab;
    public GameObject m_GroundPrefab;

    [Header("Item")]
    public GameObject m_PickUpPrefab = null;

    [Header("Misc Variables")]
    public Transform m_EntitiesContainerTransform = null;
    public List<SnakeHead> m_Players = new List<SnakeHead>();

    private void Awake()
    {
        m_TileManager = new TileManager(new Vector2Int(m_MapSizeX, m_MapSizeY));

        /// A REVOIR
        /// 
        GenerateGroundByTile();

        //m_Ground.transform.localScale = Vector3.one * (m_MapSizeX * 0.5f);   
    }

    private void GenerateGroundByTile()
    {
        if (m_TileManager != null)
        {
            for (int i = 0; i < m_TileManager.m_MapSize.x; i++)
            {
                for (int j = 0; j < m_TileManager.m_MapSize.y; j++)
                {
                    GameObject ground = Instantiate(m_GroundPrefab, new Vector3(i, -1, -j), Quaternion.identity);
                    ground.transform.localScale = new Vector3(0.1f, 1, 0.1f);
                }
            }
        }
    }

}
