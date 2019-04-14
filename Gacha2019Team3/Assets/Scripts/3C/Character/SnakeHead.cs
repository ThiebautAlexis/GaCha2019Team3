using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool m_HasItem = false;

    public int m_Size = 0;
    public float m_ResetAbility = 0.5f;

    private float m_TimerAbility = 0f;
    private bool m_CanUseAbility = true;

    void Start()
    {
        m_Controller = new GameController();
        m_Controller.UseFirstAbility += UseFirstAbility;
        m_Controller.UseSecondAbility += UseSecondAbility;
        m_Controller.UseThirdAbility += UseThirdAbility;
    }

    void Update()
    {
        m_Controller.Update();
        m_Size = CountBodies();
        ResetAbility();
    }

    override public void HitEffect()
    {
        base.HitEffect();

        Debug.Log("Die");
        Die();
    }

    public void Die()
    {
        SceneManager.LoadScene("Win");
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

        newPos.x = Mathf.Clamp(newPos.x, 0, GameData.Instance.m_TileManager.GetRestrictedMapSize().x - 1);
        newPos.y = Mathf.Clamp(newPos.y, 0, GameData.Instance.m_TileManager.GetRestrictedMapSize().y - 1);

        if (newPos == m_TilePosition)
        {
            return;
        }

        if (CanMove(newPos))
        {
            SetTilePosition(newPos);

            if (m_Body != null)
            {
                m_Body.Move(previousPos);
            }
        }

    }

    public bool CanMove(Vector2Int _WantedTilePosition)
    {
        CustomTile wantedTile = GameData.Instance.m_TileManager.GetTile(_WantedTilePosition);
        List<GameObject> entities = wantedTile.m_Entities;

        if (wantedTile != null)
        {
            if (entities.Count > 0)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (!wantedTile.m_Walkable)
                    {
                        return false;
                    }
                    else
                    {
                        SnakePart snakePart = entities[i].GetComponent<SnakePart>();
                        if (snakePart != null && !snakePart.m_CanWalkOnItself)
                        {
                            Debug.Log("Hit my queue");
                            Hit();
                        }
                        else
                        {
                            Item item;
                            bool doesItemExists = ItemManager.Instance.CheckItem(_WantedTilePosition, out item);
                            if (doesItemExists)
                            {
                                m_HasItem = true;
                                ItemManager.Instance.SetImageUIActive(true);
                                ItemManager.Instance.DestroyItem(_WantedTilePosition, item);
                            }
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }

    public int CountBodies()
    {
        if (m_Body != null)
        {
            return m_Body.Count();
        }

        return 0;
    }

    public void ShootProjectile()
    {
        Debug.Log("Shoot !!!");

        if (GameData.Instance.m_SnakeProjectilePrefab != null)
        {
            GameObject projectile = Instantiate(GameData.Instance.m_SnakeProjectilePrefab);
            projectile.transform.rotation = transform.rotation;
        }

        //SET POSITION TO CURRENT POSITION + DIRECTION (Check if it's outside the map)
    }

    public void UseFirstAbility()
    {
        if (m_HasItem && m_CanUseAbility)
        {
            Debug.Log("Add Body !");

            // Add x2
            if (m_Size == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    AddBody();
                }
            }

            for (int i = 0; i < m_Size; i++)
            {
                AddBody();
            }

            // Add +2
            //for (int i = 0; i < 2; i++)
            //{
            //    AddBody();
            //}

            UseItemGenericFunction();
        }
    }

    public void UseSecondAbility()
    {
        if (m_HasItem && m_CanUseAbility)
        {
            Debug.Log("Shield !");

            ActivateShield();
            UseItemGenericFunction();
        }
    }

    public void UseThirdAbility()
    {
        if (m_HasItem && m_CanUseAbility)
        {
            Debug.Log("Shoot !");

            ShootProjectile();
            UseItemGenericFunction();
        }
    }

    void UseItemGenericFunction()
    {
        m_CanUseAbility = false;
        m_HasItem = false;
        ItemManager.Instance.SetImageUIActive(false);
    }

    public void ResetAbility()
    {
        if (!m_CanUseAbility)
        {
            m_TimerAbility += Time.deltaTime;

            if (m_TimerAbility > m_ResetAbility)
            {
                m_CanUseAbility = true;
                m_TimerAbility = 0f;
            }
        }
    }
}
