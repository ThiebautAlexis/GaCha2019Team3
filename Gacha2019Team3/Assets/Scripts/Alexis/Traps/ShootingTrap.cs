using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class ShootingTrap : Trap
{
    #region Fields and Properties
    [Header("Shooting Trap Settings")]
    [SerializeField] private string[] m_projectilePrefabNames; 
    #endregion

    #region Methods
    /// <summary>
    /// 
    /// </summary>
    protected override IEnumerator TriggerTrap()
    {
        Debug.Log(transform.position); 
        if(GameUpdater.Instance.IsStoped())
        {
            yield return new WaitForSeconds(GameUpdater.Instance.m_TickEvent);
            StartCoroutine(TriggerTrap());
            yield break; 
        }
        yield return new WaitForSeconds(m_activationTick * GameUpdater.Instance.m_TickEvent);
        int _randomProjectileIndex = (int)Random.Range(0, m_projectilePrefabNames.Length); 
        GameObject _projectileObject = Instantiate((Resources.Load(m_projectilePrefabNames[_randomProjectileIndex]) as GameObject), GameData.Instance.m_TileManager.TilePositionToWorldPosition(m_GridPosition), Quaternion.identity); 
        TrapProjectile _projectile = _projectileObject.GetComponent<TrapProjectile>(); 
        if(_projectile)
        {
            _projectile.InitProjectile(m_GridPosition, GetBestOrientation());
        }
        else
        {
            Destroy(_projectileObject); 
        }
        // yield return new WaitForSeconds(m_activationTick * GameUpdater.Instance.m_TickEvent);
        //CleanTile(); 
        yield break; 
    }

    public override Vector2Int GetSpawningPosition()
    {
        List<CustomTile> _tiles = GameData.Instance.m_TileManager.GetEmptyTiles();
        if (_tiles.Count == 0) return Vector2Int.zero;
        List<Vector2Int> _availablesPosition;
        Vector2Int _playerPosition = GameData.Instance.m_Players[0].m_TilePosition;
        if (AIManager.Instance.m_CurrentStateIndex == 0)
        {
            _availablesPosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).Where(p => (p.x == 0 || p.x == GameData.Instance.m_TileManager.GetRestrictedMapSize().x)).ToList();
        }
        else if (AIManager.Instance.m_CurrentStateIndex == 1)
        {
            _availablesPosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).Where(p => p.y == _playerPosition.y && (p.x == 0 || p.x == GameData.Instance.m_TileManager.GetRestrictedMapSize().x)).ToList();
        }
        else if(AIManager.Instance.m_CurrentStateIndex > 1)
        {
            SnakeHead.Direction _dir = GameData.Instance.m_Players[0].m_Controller.m_Direction;
            if(_dir == SnakeHead.Direction.LEFT || _dir == SnakeHead.Direction.RIGHT)
                _availablesPosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).Where(p => p.y == _playerPosition.y && (p.x == 0 || p.x == GameData.Instance.m_TileManager.GetRestrictedMapSize().x)).ToList();
            else if (_dir == SnakeHead.Direction.UP || _dir == SnakeHead.Direction.DOWN)
                _availablesPosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).Where(p => p.x == _playerPosition.y && (p.y == 0 || p.y == GameData.Instance.m_TileManager.GetRestrictedMapSize().x)).ToList();
            else _availablesPosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).Where(p => p.x == _playerPosition.y && p.y == _playerPosition.y).ToList();
        }
        else
        {
            _availablesPosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).Where(p => p.x == 0 || p.x == GameData.Instance.m_TileManager.GetRestrictedMapSize().x || p.y == 0 || p.y == GameData.Instance.m_TileManager.GetRestrictedMapSize().y).ToList();
        }
        int _randomIndex = (int)Random.Range(0, _availablesPosition.Count); 
        return _availablesPosition[_randomIndex];
    }

    public override Quaternion GetBestOrientation()
    {
        if(m_GridPosition.x == 0 )
        {
            return Quaternion.Euler(0, 90, 0);
        }
        if(m_GridPosition.x == GameData.Instance.m_TileManager.GetRestrictedMapSize().x)
        {
            return Quaternion.Euler(0, -90, 0);
        }
        if (m_GridPosition.y == 0)
        {
            return Quaternion.Euler(0, 180, 0);

        }
        else return Quaternion.identity;
    }

    #region UnityMethods


    #endregion

    #endregion

}