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
        GameObject newSnake = new GameObject("SnakeHead");
        SnakeHead newHead = newSnake.AddComponent<SnakeHead>();
        newSnake.transform.parent = m_EntitiesContainerTransform;

        newHead.InitTilePosition(Vector2Int.zero);

        GameData.Instance.m_Players.Add(newHead);
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
}
