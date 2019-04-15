using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region Fields and Properties
    [SerializeField, Range(1,4)] int m_projectileLength = 1;

    [SerializeField]private Vector2Int[] m_tilesPosition;  

    // private Vector2Int m_tilePosition = Vector2Int.zero;

    private Vector2Int m_lastTilePosition = Vector2Int.zero; 

    //public Vector2Int m_TilePosition { get { return m_tilePosition; } }

    private Vector2Int m_shootDirection = Vector2Int.zero;
    #endregion

    #region Methods
    protected abstract void ApplyEffect(CustomTile _tile);
    protected abstract bool CanHit(CustomTile _nextTile);

    /// <summary>
    /// Init the tile position of the projectile
    /// </summary>
    /// <param name="_tilePosition"></param>
    public void InitProjectile(Vector2Int _tilePosition, Quaternion _dir)
    {
        m_tilesPosition = new Vector2Int[m_projectileLength];
        m_tilesPosition.ToList().ForEach(t => t = _tilePosition);
        transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(_tilePosition);
        GameData.Instance.m_TileManager.GetRestrictedMap()[_tilePosition.x, _tilePosition.y].m_Entities.Add(gameObject);
        transform.rotation = _dir;
        m_shootDirection = new Vector2Int((int)transform.forward.x, (int)transform.forward.z); 
        StartCoroutine(MoveProjectile()); 
    }

    /// <summary>
    /// Set the tile position of the projectile
    /// </summary>
    /// <param name="_tilePosition"></param>
    protected void SetTilePosition(Vector2Int _tilePosition)
    {
        m_lastTilePosition = m_tilesPosition.Last();

        GameData.Instance.m_TileManager.GetRestrictedMap()[m_lastTilePosition.x, m_lastTilePosition.y].m_Entities.Remove(gameObject);
        if (m_projectileLength > 1)
        {
            for (int i = 1; i < m_projectileLength; i++)
            {
                m_tilesPosition[i] = m_tilesPosition[i - 1]; 
            }
        }
        m_tilesPosition[0] = _tilePosition;

        transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(m_tilesPosition[0]); 
        GameData.Instance.m_TileManager.GetRestrictedMap()[m_tilesPosition[0].x, m_tilesPosition[0].y].m_Entities.Add(gameObject);
    }

    protected IEnumerator MoveProjectile()
    {
        //MAKE THE PROJECTILE MOVE UNTIL IT REACHS A WALL OR THE PLAYER
        Vector2Int _nextPos;
        CustomTile _nextTile;
        while (true)
        {
            _nextPos = m_tilesPosition[0] + m_shootDirection;
            if (_nextPos.x < 0 || _nextPos.x >= GameData.Instance.m_TileManager.GetRestrictedMapSize().x || _nextPos.y < 0 || _nextPos.y >= GameData.Instance.m_TileManager.GetRestrictedMapSize().y)
            {
                break;
            }

            _nextTile = GameData.Instance.m_TileManager.GetTile(_nextPos);
            /*
             * Le projectile peut-il passer au travers des tiles non walkables?
            if (!_nextTile.m_Walkable)
                break;
            */
            if(CanHit(_nextTile))
            {
                SetTilePosition(_nextPos);
                ApplyEffect(_nextTile); 
                break; 
            }
            SetTilePosition(_nextPos); 
            yield return new WaitForSeconds(GameUpdater.Instance.m_TickEvent);
        }
        m_tilesPosition.ToList().ForEach(t => GameData.Instance.m_TileManager.GetTile(t).m_Entities.Remove(gameObject)); 
        Destroy(gameObject); 
    }

    #region UnityMethods
    #endregion

    #endregion
}
