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

    [Header("Other Prefab")]
    public GameObject m_BackgroundPrefab = null;
    public GameObject m_GroundPrefab = null;
    public GameObject m_WallPrefab = null;

    [Header("Item")]
    public GameObject m_PickUpPrefab = null;

    [Header("Misc Variables")]
    public Transform m_EntitiesContainerTransform = null;
    public List<SnakeHead> m_Players = new List<SnakeHead>();
    
    [Header("Fx Prefab")]
    public GameObject m_CollectFxPrefab = null;//0
    public GameObject m_BombFxPrefab = null; //1
    public GameObject m_LaserFxPrefab = null; //2
    public GameObject m_DmgFxPrefab = null; //3
    public GameObject m_ShieldFxPrefab = null; //4
    public GameObject m_GrowFxPrefab = null; //5

    private void Awake()
    {
        m_TileManager = new TileManager(new Vector2Int(m_MapSizeX, m_MapSizeY));

        GenerateWall();
        GenerateGroundByTile();        
    }

    private void GenerateGroundByTile()
    {
        if (m_TileManager != null)
        {
            for (int i = 0; i < m_TileManager.m_MapSize.x / 2; i++)
            {
                for (int j = 0; j < m_TileManager.m_MapSize.y / 2; j++)
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
            for (int i = 0; i < m_TileManager.m_MapSize.x / 2; i++)
            {
                GameObject upWall = Instantiate(m_WallPrefab, new Vector3(1 * i, 0, 1), Quaternion.identity);
                GameObject downWall = Instantiate(m_WallPrefab, new Vector3(1 * i, 0, -m_TileManager.m_MapSize.x / 2), Quaternion.identity);
            }

            for (int i = 0; i < m_TileManager.m_MapSize.y / 2; i++)
            {
                GameObject leftWall = Instantiate(m_WallPrefab, new Vector3(-1, 0, -1 * i), Quaternion.identity);
                GameObject rightWall = Instantiate(m_WallPrefab, new Vector3(m_TileManager.m_MapSize.y / 2, 0, -1 * i), Quaternion.identity);
            }          
        }
    }

    public void SpawnFx (GameObject gameOb, int n)
    {
        Debug.Log(n);
        if (n ==0)
        {
            GameObject FxToDestroy = Instantiate(GameData.Instance.m_CollectFxPrefab, gameOb.transform.position, Quaternion.identity);
            Invoke("DestroyFx", FxToDestroy.GetComponent<ParticleSystem>().main.duration);
            Debug.Log("SpawnedFx");

        }else if (n == 1)
        {
            GameObject FxToDestroy = Instantiate(GameData.Instance.m_BombFxPrefab, gameOb.transform.position, Quaternion.identity);
            Invoke("DestroyFx", FxToDestroy.GetComponent<ParticleSystem>().main.duration);
            Debug.Log("SpawnedFx");
        }else if (n == 2)
        {
            GameObject FxToDestroy = Instantiate(GameData.Instance.m_LaserFxPrefab, gameOb.transform.position, Quaternion.identity);
            Invoke("DestroyFx", FxToDestroy.GetComponent<ParticleSystem>().main.duration);
            Debug.Log("SpawnedFx");
        }else if (n == 3)
        {
            GameObject FxToDestroy = Instantiate(GameData.Instance.m_DmgFxPrefab, gameOb.transform.position, Quaternion.identity);
            Invoke("DestroyFx", FxToDestroy.GetComponent<ParticleSystem>().main.duration);
            Debug.Log("SpawnedFx");
        }else if (n == 4)
        {
            GameObject FxToDestroy = Instantiate(GameData.Instance.m_ShieldFxPrefab, gameOb.transform.position, gameOb.transform.rotation);
            FxToDestroy.transform.SetParent(gameOb.transform);
            Invoke("DestroyFx", FxToDestroy.GetComponent<ParticleSystem>().main.duration);
            Debug.Log("SpawnedFx");
        }else if (n == 5)
        {
            GameObject FxToDestroy = Instantiate(GameData.Instance.m_GrowFxPrefab, gameOb.transform.position, gameOb.transform.rotation);
            FxToDestroy.transform.SetParent(gameOb.transform);
            Invoke("DestroyFx", FxToDestroy.GetComponent<ParticleSystem>().main.duration);
            Debug.Log("SpawnedFx");
        }
    }

    private void DestroyFx(GameObject FxToDestroy)
    {
        Destroy(FxToDestroy);
        Debug.Log("Destroyed");
    }
}
