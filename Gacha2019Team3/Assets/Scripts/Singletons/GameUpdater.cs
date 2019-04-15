using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameUpdater : Singleton<GameUpdater>
{
    [Header("Basic Variables")]
    public float m_TickEventPlayer = 0.25f;
    public float m_TickEvent = 0.25f;
    public float m_TickEventReset = 6f;

    private float m_TickTimerPlayer = 0f;
    private float m_TickTimer = 0f;
    private bool m_FreezeEventTime = false;

    void Awake()
    {
        InitializedPlayer();
    }

    void Update()
    {
        PlayerTick();
        EventTick();
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

                newHead.InitTilePosition(new Vector2Int(GameData.Instance.m_MapSizeX / (numberPlayer + 1) * (i + 1), GameData.Instance.m_MapSizeY / (numberPlayer + 1) * (i + 1)));
                newHead.m_PlayerNum = PlayerNum.Player1 + i;

                GameData.Instance.m_Players.Add(newHead);
            }
            else
            {
                Debug.LogError("Missing Data reference in GameData.");
            }
        }
    }

    private void PlayerTick()
    {
        m_TickTimerPlayer += Time.deltaTime;

        if (m_TickTimerPlayer < m_TickEventPlayer)
        {
            return;
        }

        GameData.Instance.m_Players.ForEach(p => p.Move());

        m_TickTimerPlayer = 0f;
    }

    private void EventTick()
    {
        if (m_FreezeEventTime)
        {
            Debug.Log("Time Freeze !");
            m_TickTimer += Time.deltaTime;

            if (m_TickTimer >= m_TickEventReset)
            {
                PlayEventTime();
            }
        }
    }

    public void StopEventTime()
    {
        m_TickEvent = m_TickEventReset; //Mathf.Infinity;
        m_FreezeEventTime = true;

        Debug.Log("Stop time called...");
    }

    private void PlayEventTime()
    {
        m_FreezeEventTime = false;
        m_TickEvent = m_TickEventPlayer;
        m_TickTimer = 0f;
        
        Debug.Log("Play time called...");
    }

    public bool IsStoped()
    {
        return m_FreezeEventTime;
    }
}
