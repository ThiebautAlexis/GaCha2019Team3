using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : SnakePart
{
    [Header("Basic Variables")]
    public GameController m_Controller = null;

    public enum Direction
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        NONE        
    }

    [Header("Gameplay Variables")]
    public Item m_Item = null;
    public bool m_IsShield = false;
    public float m_ShieldTimeLimit = 0f;
    public int m_Size = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<SpriteRenderer>().sprite = GameData.Instance.m_SnakeHeadSprite;
        m_Controller = new GameController();

        m_Controller.UseFirstAbility += UseFirstAbility;
        m_Controller.UseSecondAbility += UseSecondAbility;
        m_Controller.UseThirdAbility += UseThirdAbility;
    }

    // Update is called once per frame
    void Update()
    {
        m_Controller.Update();
        m_Size = CountBodies();
    }

    override public void Hit()
    {
        base.Hit();

        Debug.LogError("DEAD !!!");
        Debug.LogWarning("Time Scale Stopped");
        Time.timeScale = 0;
    }

    public void Move()
    {
        Vector2Int previousPos = m_TilePosition;
        Vector2Int newPos = m_TilePosition;

        switch (m_Controller.m_Direction)
        {
            case Direction.UP:
                newPos = m_TilePosition - new Vector2Int(0, 1); 
                break;
            case Direction.RIGHT:
                newPos = m_TilePosition + new Vector2Int(1, 0);
                break;
            case Direction.DOWN:
                newPos = m_TilePosition + new Vector2Int(0, 1);
                break;
            case Direction.LEFT:
                newPos = m_TilePosition - new Vector2Int(1, 0);
                break;
            default:
                break;
        }

        newPos.x = Mathf.Clamp(newPos.x, 0, GameData.Instance.m_TileManager.m_MapSize.x - 1);
        newPos.y = Mathf.Clamp(newPos.y, 0, GameData.Instance.m_TileManager.m_MapSize.y - 1);

        SetTilePosition(newPos);

        if (m_Body != null)
        {
            m_Body.Move(previousPos);
        }
    }

    public int CountBodies()
    {
        Debug.Log("Count: " + m_Size);

        if (m_Body != null)
        {
            return m_Body.Count();
        }

        return 0;
    }

    private void UseFirstAbility()
    {
        Debug.Log("Use First Ability !");
    }

    private void UseSecondAbility()
    {
        Debug.Log("Use Second Ability !");
    }

    private void UseThirdAbility()
    {
        Debug.Log("Use Third Ability !");
    }
    
}
