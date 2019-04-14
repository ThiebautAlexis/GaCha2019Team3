using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameUpdater : Singleton<GameUpdater>
{
    [Header("Basic Variables")]
    public float m_TickEvent = 0.25f;

    private float m_TickTimer = 0f;

    void Start()
    {
        InitializedPlayer();
    }

    void Update()
    {

        Debug.Log("Free Tile: " + GameData.Instance.m_TileManager.GetEmptyTiles().Count);

        m_TickTimer += Time.deltaTime;

        if (m_TickTimer < m_TickEvent)
        {
            return;
        }

        GameData.Instance.m_Players.ForEach(p => p.Move());

        m_TickTimer = 0f;

    }

    private void InitializedPlayer()
    {
        for (int i = 0; i < GameData.Instance.m_PlayerCount; i++)
        {
            GameObject newSnake = Instantiate(GameData.Instance.m_SnakeHeadPrefab);
            if (newSnake != null)
            {
                SnakeHead newHead = newSnake.GetComponent<SnakeHead>();
                newSnake.transform.parent = GameData.Instance.m_EntitiesContainerTransform;

                int numberPlayer = GameData.Instance.m_PlayerCount + 1;

                newHead.InitTilePosition(new Vector2Int(GameData.Instance.m_TileManager.GetRestrictedMapSize().x / (numberPlayer + 1) * (i + 1), GameData.Instance.m_TileManager.GetRestrictedMapSize().y / (numberPlayer + 1) * (i + 1)));

                GameData.Instance.m_Players.Add(newHead);
            }
            else
            {
                Debug.LogError("Missing Data reference in GameData.");
            }
        }
    }
}
