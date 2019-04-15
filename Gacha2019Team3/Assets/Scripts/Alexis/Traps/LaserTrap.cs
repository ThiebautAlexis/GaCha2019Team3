using System; 
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using Random = UnityEngine.Random; 

public class LaserTrap : Trap
{
    #region Fields and properties
    [Header("Laser Settings")]
    [SerializeField, Range(.1f, 5)] private int m_laserDuration = 1;
    public float m_LaserDurationInSeconds
    {
        get { return m_laserDuration * GameUpdater.Instance.m_TickEvent;  }
    }

    [SerializeField] private LineRenderer m_laserRenderer;
    #endregion

    #region Methods
    /// <summary>
    /// Method called when the trap has to be activated
    /// Cast a ray in front of the trap during a certain duration
    /// If the player is touched, call the damage method 
    /// Then break and destroy the object
    /// </summary>
    protected override IEnumerator TriggerTrap()
    {
        float _timer = 0;
        RaycastHit _hitInfo;
        int _rayNumber = AIManager.Instance.m_CurrentStateIndex < 3 ? 1 : AIManager.Instance.m_CurrentStateIndex > 3 ? 4 : (int)UnityEngine.Random.Range(2,4); 
        if(m_laserRenderer)
        {
            m_laserRenderer.positionCount = 2 * _rayNumber;
            for (int i = 0; i < m_laserRenderer.positionCount; i++)
            {
                m_laserRenderer.SetPosition(i, transform.position); 
            }
        }
        Vector3 _direction = Vector3.zero; 
        while (_timer < m_LaserDurationInSeconds)
        {
            for (int i = 0; i < m_laserRenderer.positionCount; i+=2)
            {
                _direction = transform.forward * Mathf.Cos(90*(i/2)*Mathf.Deg2Rad) + transform.right * Mathf.Sin(90 * (i/2) * Mathf.Deg2Rad);
                if (Physics.Raycast(new Ray(transform.position, _direction), out _hitInfo))
                {
                    if (m_laserRenderer != null)
                    {
                        m_laserRenderer.SetPosition(i, transform.position); 
                        m_laserRenderer.SetPosition(i+1, _hitInfo.point);
                    }
                    // Check if the player is touched by the ray
                    if (_hitInfo.collider.GetComponent<SnakePart>())
                    {
                        //Call Method to damage the player
                        _hitInfo.collider.GetComponent<SnakePart>().Hit();
                        break;
                    }
                }
            }
            if (GameUpdater.Instance.IsStoped())
            {
                yield return new WaitForSeconds(GameUpdater.Instance.m_TickEvent);
                continue;
            }
            _timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        CleanTile(); 
        yield break;
    }

    public override Vector2Int GetSpawningPosition()
    {
        List<CustomTile> _tiles = GameData.Instance.m_TileManager.GetEmptyTiles();
        if (_tiles.Count == 0) return Vector2Int.zero;
        if (AIManager.Instance.m_CurrentStateIndex <= 1)
        {
            return GameData.Instance.m_TileManager.GetPosition(_tiles[Random.Range(0, _tiles.Count)]);
        }
        else
        {
            SnakeHead.Direction _dir = GameData.Instance.m_Players[0].m_Controller.m_Direction;
            Vector2Int _playerPosition = GameData.Instance.m_Players[0].m_TilePosition;
            List<Vector2Int> _availablePosition;
            switch (_dir)
            {
                case SnakeHead.Direction.UP:
                    _availablePosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).Where(p => p.y > _playerPosition.y).ToList();
                    break;
                case SnakeHead.Direction.RIGHT:
                    _availablePosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).Where(p => p.x > _playerPosition.x).ToList();
                    break;
                case SnakeHead.Direction.DOWN:
                    _availablePosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).Where(p => p.y < _playerPosition.y).ToList();
                    break;
                case SnakeHead.Direction.LEFT:
                    _availablePosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).Where(p => p.x < _playerPosition.x).ToList();
                    break;
                case SnakeHead.Direction.NONE:
                    _availablePosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).ToList();
                    break;
                default:
                    _availablePosition = _tiles.Select(t => GameData.Instance.m_TileManager.GetPosition(t)).ToList();
                    break;
            }
            int _randomIndex = (int)Random.Range(0, _availablePosition.Count);
            return _availablePosition[_randomIndex];
        }
    }

    public override Quaternion GetBestOrientation()
    {
        if(AIManager.Instance.m_CurrentStateIndex == 0)
        {
            return Quaternion.Euler(0, 90 * (int)UnityEngine.Random.Range(0, 3), 0);
        }
        if (AIManager.Instance.m_CurrentStateIndex >= 1)
        {
            SnakeHead.Direction _dir = GameData.Instance.m_Players[0].m_Controller.m_Direction;
            if (_dir == SnakeHead.Direction.UP || _dir == SnakeHead.Direction.DOWN)
                return Quaternion.identity;
            else return Quaternion.Euler(0, 90, 0);
        }
        else return Quaternion.identity; 
    }

    #region UnityMethods
    protected override void Start()
    {
        base.Start();
        if (!m_laserRenderer)
        {
            m_laserRenderer = GetComponent<LineRenderer>();
        }
        m_laserRenderer.positionCount = 0;
    }


    #endregion
    #endregion
}
