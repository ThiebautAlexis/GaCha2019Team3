using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields and Properties
    [SerializeField] string m_targetedTag = "Player"; 

    private Vector2Int m_tilePosition = Vector2Int.zero;

    private Vector2Int m_lastTilePosition = Vector2Int.zero; 

    public Vector2Int m_TilePosition { get { return m_tilePosition; } }

    private Vector2Int m_shootDirection = Vector2Int.zero; 
    #endregion

    #region Methods
    /// <summary>
    /// Init the tile position of the projectile
    /// </summary>
    /// <param name="_tilePosition"></param>
    private void InitTilePosition(Vector2Int _tilePosition, Vector2Int _dir)
    {
        m_tilePosition = _tilePosition;
        transform.position = new Vector3(m_TilePosition.x, m_TilePosition.y, 0) * 0.5f;
        GameData.Instance.m_TileManager.m_MapTile[_tilePosition.x, _tilePosition.y].m_Entities.Add(gameObject);

    }

    /// <summary>
    /// Set the tile position of the projectile
    /// </summary>
    /// <param name="_tilePosition"></param>
    public void SetTilePosition(Vector2Int _tilePosition)
    {
        m_lastTilePosition = m_tilePosition;

        GameData.Instance.m_TileManager.m_MapTile[_tilePosition.x, _tilePosition.y].m_Entities.Remove(gameObject);
        m_tilePosition = _tilePosition;
        transform.position = new Vector3(m_TilePosition.x, 0, -m_TilePosition.y) * 0.5f;
        GameData.Instance.m_TileManager.m_MapTile[_tilePosition.x, _tilePosition.y].m_Entities.Add(gameObject);
    }

    private bool CanHit(Vector2Int _checkedPosition)
    {
        CustomTile _nextTile = GameData.Instance.m_TileManager.GetTile(_checkedPosition);
        if (_nextTile.m_Entities.Any(e => e.tag.ToLower() == m_targetedTag.ToLower()))
        {
            return true;
        }
        return false; 
    }

    public IEnumerator MoveProjectile()
    {
        //MAKE THE PROJECTILE MOVE UNTIL IT REACHS A WALL OR THE PLAYER
        Vector2Int _nextPos;
        CustomTile _nextTile; 
        while (true)
        {
            _nextPos = m_tilePosition + m_shootDirection;
            if (_nextPos.x < 0 || _nextPos.x > GameData.Instance.m_MapSizeX || _nextPos.y < 0 || _nextPos.y > GameData.Instance.m_MapSizeY)
            {
                break;
            }
            _nextTile = GameData.Instance.m_TileManager.GetTile(_nextPos);
            SetTilePosition(_nextPos); 
            yield return new WaitForSeconds(GameUpdater.Instance.m_TickEvent);
        }
        Destroy(gameObject); 
    }
    #region UnityMethods

    #endregion

    #endregion
}
