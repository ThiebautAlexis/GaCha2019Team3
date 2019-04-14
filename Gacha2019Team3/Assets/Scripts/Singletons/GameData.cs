using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : Singleton<GameData>
{
    [Header("Basic Variables")]
    [SerializeField] int m_MapSizeX = 40;
    [SerializeField] int m_MapSizeY = 40;
    public int m_PlayerCount = 1;
    public TileManager m_TileManager = null;

    public Vector2Int m_MapRestrictionPlot1 = Vector2Int.zero;
    public Vector2Int m_MapRestrictionPlot2 = Vector2Int.zero;
    Vector2Int m_BaseRestricted;

    public List<GameObject> walls = new List<GameObject>();

    public GameCamera m_Camera = null;

    [Header("Snake Prefabs")]
    public GameObject m_SnakeHeadPrefab = null;
    public GameObject m_SnakeBodyPrefab = null;
    public GameObject m_SnakeProjectilePrefab = null;

    [Header("Other Prefab")]
    public GameObject m_BackgroundPrefab = null;
    public GameObject m_GroundPrefab = null;
    public GameObject m_WallPrefab = null;

    [Header("Item")]
    public GameObject m_PickUpPrefab = null;

    [Header("Misc Variables")]
    public Transform m_EntitiesContainerTransform = null;
    public List<SnakeHead> m_Players = new List<SnakeHead>();

    private void Awake()
    {
        m_TileManager = new TileManager(new Vector2Int(m_MapSizeX, m_MapSizeY));

        
        GenerateGroundByTile();        
    }

    private void Start()
    {
        m_TileManager.m_RestrictedMap1 = m_MapRestrictionPlot1;
        m_TileManager.m_RestrictedMap2 = m_MapRestrictionPlot2;
        m_BaseRestricted = m_MapRestrictionPlot2;
        GenerateWall();
    }

    private void GenerateGroundByTile()
    {
        if (m_TileManager != null)
        {
            for (int i = 0; i < m_TileManager.GetFullMapSize().x / 2; i++)
            {
                for (int j = 0; j < m_TileManager.GetFullMapSize().y / 2; j++)
                {
                    GameObject ground = Instantiate(m_GroundPrefab, new Vector3(i * 1, -0.5f, -j * 1), Quaternion.identity);
                    ground.transform.localScale = new Vector3(0.1f, 1, 0.1f);
                }
            }
        }
    }

    private void GenerateWall()
    {
        if (m_WallPrefab != null)
        {
            for (int i = 0; i < m_TileManager.GetRestrictedMapSize().x / 2; i++)
            {
                GameObject upWall = Instantiate(m_WallPrefab, new Vector3(1 * i, 0, 1), Quaternion.identity);
                walls.Add(upWall);
                GameObject downWall = Instantiate(m_WallPrefab, new Vector3(1 * i, 0, -m_TileManager.GetRestrictedMapSize().x/2), Quaternion.identity);
                walls.Add(downWall);
            }
            
            for (int i = 0; i < m_TileManager.GetRestrictedMapSize().y / 2; i++)
            {
                GameObject leftWall = Instantiate(m_WallPrefab, new Vector3(-1, 0, -1 * i), Quaternion.identity);
                walls.Add(leftWall);
                GameObject rightWall = Instantiate(m_WallPrefab, new Vector3(m_TileManager.GetRestrictedMapSize().y/2, 0, -1 * i), Quaternion.identity);
                walls.Add(rightWall);
            }          
        }
    }

    public void expand(int phase)
    {
        
        foreach (GameObject wall in walls)
        {

            
            Destroy(wall);
        }
        walls.Clear();
        m_TileManager.m_RestrictedMap2 = new Vector2Int(m_MapRestrictionPlot2.x+5*phase,m_MapRestrictionPlot2.y+5*phase);
        GenerateWall();
    }
}
