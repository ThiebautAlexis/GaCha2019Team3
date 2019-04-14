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
    public float m_ShieldActiveTime = 4.0f;
    public float m_ShieldTimeLimit = 0f;
    public int m_Size = 0;
    public float m_Resetability = 0.5f;

    private float m_TimerAbility = 0f;
    private bool m_CanUseAbility = false;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.AddComponent<SpriteRenderer>().sprite = GameData.Instance.m_SnakeHeadSprite;
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
        ShieldUpdateTimeAndDeactivate();
        ResetAbility();
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
            case Direction.NONE:
                return;
            default:
                break;
        }

        newPos.x = Mathf.Clamp(newPos.x, 0, GameData.Instance.m_TileManager.m_MapSize.x - 1);
        newPos.y = Mathf.Clamp(newPos.y, 0, GameData.Instance.m_TileManager.m_MapSize.y - 1);

        if (newPos == m_TilePosition)
        {
            return;
        }

        SetTilePosition(newPos);

        if (m_Body != null)
        {
            m_Body.Move(previousPos);
        }

        //if (CanMove(newPos))
        //{
        //    SetTilePosition(newPos);

        //    if (m_Body != null)
        //    {
        //        m_Body.Move(previousPos);
        //    }
        //}

    }

    public bool CanMove(Vector2Int _WantedTilePosition)
    {
        CustomTile wantedTile = GameData.Instance.m_TileManager.GetTile(_WantedTilePosition);
        List<GameObject> entities = wantedTile.m_Entities;

        if (entities.Count > 0)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].GetType() == typeof(Item))
                {
                    //Item item = entities[i] is Item;
                    return true;
                }
                else
                {
                    if (ItemManager.Instance.CheckItem(_WantedTilePosition))
                    {
                        m_Item = new Item();
                        ItemManager.Instance.DestroyItem(_WantedTilePosition);
                    }
                }
            }
        }

        return true;
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

    void ActivateShield()
    {
        m_IsShield = true;

    }

    void DeactivateShield()
    {
        m_IsShield = false;

    }

    void ShieldUpdateTimeAndDeactivate()
    {
        if (m_IsShield)
        {
            m_ShieldTimeLimit += Time.deltaTime;

            if (m_ShieldTimeLimit > m_ShieldActiveTime)
            {
                DeactivateShield();

                m_ShieldTimeLimit = 0;
            }
        }
    }

    public void ShootProjectile()
    {
        Debug.Log("Shoot !!!");

        GameObject projectile = Instantiate(GameData.Instance.m_SnakeProjectilePrefab);

        projectile.transform.rotation = transform.rotation;

        //SET POSITION TO CURRENT POSITION + DIRECTION (Check if it's outside the map)
    }

    public void UseFirstAbility()
    {
        if (m_Item != null && !m_CanUseAbility)
        {
            Debug.Log("Use First Ability !");

            AddBody();
            m_CanUseAbility = true;
        }
    }

    public void UseSecondAbility()
    {
        if (m_Item != null && !m_CanUseAbility)
        {
            Debug.Log("Use Second Ability !");

            ActivateShield();
            m_CanUseAbility = true;
        }
    }

    public void UseThirdAbility()
    {
        if (m_Item != null && !m_CanUseAbility)
        {
            Debug.Log("Use Third Ability !");

            ShootProjectile();
            m_CanUseAbility = true;
        }
    }

    public void ResetAbility()
    {
        if (m_CanUseAbility)
        {
            m_TimerAbility += Time.deltaTime;

            if (true)
            {
                m_CanUseAbility = false;
                m_TimerAbility = 0f;
            }
        }
    }
}
