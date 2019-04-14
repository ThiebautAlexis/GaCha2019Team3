using System; 
using System.Collections;
using System.Collections.Generic;
using System.IO; 
using UnityEngine;
using Random = UnityEngine.Random; 

public class AIManager : Singleton<AIManager>
{
    #region Fields and properties 
    [SerializeField] private string m_TrapPath ="Traps/"; 
    [SerializeField] private float[] m_statesCoefficient = new float[4] {1, .7f, .5f, .2f };
    private int m_currentStateIndex = 0;
    public int m_CurrentStateIndex { get { return m_currentStateIndex; } }
    [SerializeField] private string[] m_spawningTrapsNames;
    #endregion

    #region Event
    public event Action<int> OnStateChanged; 
    #endregion

    #region Methods 
    /// <summary>
    /// Get a random empty tile
    /// </summary>
    /// <returns></returns>
    private Vector2Int GetRandomSpawningPosition()
    {
        List<CustomTile> _tiles = GameData.Instance.m_TileManager.GetEmptyTiles();
        if (_tiles.Count == 0) return Vector2Int.zero; 
        CustomTile _tile = _tiles[UnityEngine.Random.Range(0, _tiles.Count)];
        return GameData.Instance.m_TileManager.GetPosition(_tile); 
    }

    /// <summary>
    /// Get the trap to spawn and its spawn delay
    /// Then make it spawn after its delay
    /// </summary>
    /// <param name="_trapName"></param>
    public void StartSpawningRoutine(string _trapName)
    {
        GameObject _trap = Resources.Load(m_TrapPath + _trapName) as GameObject;
        if (!_trap || !_trap.GetComponent<Trap>())
        {
            Debug.LogWarning($"This trap doesn't exist in the {Application.dataPath + "/" + m_TrapPath + _trapName} or a trap component is missing"); 
            return;
        }
        StartCoroutine(SpawnTrap(_trap)); 
    }

    /// <summary>
    /// Spawn a trap after its waiting delay
    /// </summary>
    /// <param name="_trapToSpawn">Trap to spawn</param>
    /// <param name="_waitingDelay">Delay to wait</param>
    /// <returns></returns>
    private IEnumerator SpawnTrap(GameObject _trapToSpawn)
    {
        float _delay = _trapToSpawn.GetComponent<Trap>().m_SpawningTick * GameUpdater.Instance.m_TickEvent * m_statesCoefficient[m_currentStateIndex];
        yield return new WaitForSeconds(_delay);
        Vector2Int _spawningPos = GetRandomSpawningPosition(); 
        if(_spawningPos == null)
        {
            Debug.LogWarning($"There is no free space to spawn a {_trapToSpawn.name} on the grid"); 
            yield break; 
        }
        GameObject.Instantiate(_trapToSpawn, new Vector3(_spawningPos.x, 0, _spawningPos.y), Quaternion.identity);
        if (GameData.Instance.m_TileManager.GetEmptyTiles().Count == 0)
            yield break; 
        StartCoroutine(SpawnTrap(_trapToSpawn));
        yield break; 
    }

    /// <summary>
    /// Increase the state of the AI Manager and call the event OnStateChanged
    /// </summary>
    public void IncreaseState()
    {
        m_currentStateIndex++;
        m_currentStateIndex = Mathf.Clamp(m_currentStateIndex, 0, m_statesCoefficient.Length); 
        OnStateChanged?.Invoke(m_currentStateIndex); 
    }

    /// <summary>
    /// Decrease the state of the AI Manager and call the event OnStateChanged
    /// </summary>
    public void DecreaseState()
    {
        m_currentStateIndex--;
        m_currentStateIndex = Mathf.Clamp(m_currentStateIndex, 0, m_statesCoefficient.Length);
        OnStateChanged?.Invoke(m_currentStateIndex);
    }

    #region UnityMethods
    private void Start()
    {
        for (int i = 0; i < m_spawningTrapsNames.Length; i++)
        {
            StartSpawningRoutine(m_spawningTrapsNames[i]); 
        }
    }
    #endregion

    #endregion

}
