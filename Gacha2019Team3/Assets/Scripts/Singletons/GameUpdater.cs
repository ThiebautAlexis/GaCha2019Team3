using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameUpdater : Singleton<GameUpdater>
{
    [Header("Basic Variables")]
    public float m_TickEvent = 0.25f;

    private float m_TickTimer = 0f;

    [Header("Misc")]
    [SerializeField]
    Transform m_EntitiesContainerTransform = null;

    void Start()
    {
        InitializedPlayer();
    }

    void Update()
    {
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
            //GameObject newSnake = new GameObject("SnakeHead_" + i);

            GameObject newSnake = Instantiate(GameData.Instance.m_SnakeHeadPrefab);
            newSnake.transform.Rotate(new Vector3(-90, 0, 0));
            SnakeHead newHead = newSnake.AddComponent<SnakeHead>();
            newSnake.transform.parent = m_EntitiesContainerTransform;

            newHead.InitTilePosition(new Vector2Int(GameData.Instance.m_MapSizeX / 2, GameData.Instance.m_MapSizeY / 2));


            GameData.Instance.m_Players.Add(newHead);
        }
    }
}
