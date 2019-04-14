using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region Fields and Properties
    private Vector2Int m_tilePosition = Vector2Int.zero;

    private Vector2Int m_lastTilePosition = Vector2Int.zero; 

    public Vector2Int m_TilePosition { get { return m_tilePosition; } }

    private Vector2Int m_shootDirection = Vector2Int.zero;
    #endregion

    #region Methods
    protected abstract void ApplyEffect(CustomTile _tile);
    protected abstract bool CanHit(CustomTile _nextTile);

    /// <summary>
    /// Init the tile position of the projectile
    /// </summary>
    /// <param name="_tilePosition"></param>
    public void InitProjectile(Vector2Int _tilePosition, Vector3Int _dir)
    {
        m_tilePosition = _tilePosition;
        transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(m_tilePosition);
        GameData.Instance.m_TileManager.m_MapTile[_tilePosition.x, _tilePosition.y].m_Entities.Add(gameObject);
        transform.rotation = Quaternion.Euler(_dir.x, 0, _dir.y); 
        m_shootDirection = new Vector2Int(_dir.x, _dir.z);
        StartCoroutine(MoveProjectile()); 
    }

    /// <summary>
    /// Set the tile position of the projectile
    /// </summary>
    /// <param name="_tilePosition"></param>
    protected void SetTilePosition(Vector2Int _tilePosition)
    {
        m_lastTilePosition = m_TilePosition;


        GameData.Instance.m_TileManager.m_MapTile[_tilePosition.x, _tilePosition.y].m_Entities.Remove(gameObject);
        m_tilePosition = _tilePosition;

        transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(m_tilePosition); 
        GameData.Instance.m_TileManager.m_MapTile[_tilePosition.x, _tilePosition.y].m_Entities.Add(gameObject);
    }

    protected IEnumerator MoveProjectile()
    {
        //MAKE THE PROJECTILE MOVE UNTIL IT REACHS A WALL OR THE PLAYER
        Vector2Int _nextPos;
        CustomTile _nextTile;
        while (true)
        {
            _nextPos = m_tilePosition + m_shootDirection;
            if (_nextPos.x < 0 || _nextPos.x >= GameData.Instance.m_MapSizeX || _nextPos.y < 0 || _nextPos.y >= GameData.Instance.m_MapSizeY)
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
        GameData.Instance.m_TileManager.GetTile(m_tilePosition).m_Entities.Remove(gameObject); 
        Destroy(gameObject); 
    }

    #region UnityMethods
    #endregion

    #endregion
}
