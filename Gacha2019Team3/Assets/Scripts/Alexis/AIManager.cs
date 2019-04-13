using System; 
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using Random = UnityEngine.Random; 

public class AIManager : MonoBehaviour
{
    #region Fields and properties 
    [SerializeField] private float[] m_statesCoefficient = new float[4] {1, .7f, .5f, .2f };
    private int m_currentStateIndex = 0;

    [SerializeField] private string[] m_spawningTraps;
    #endregion

    #region Methods 

    /// <summary>
    /// Get a random empty tile
    /// </summary>
    /// <returns></returns>
    private CustomTile GetRandomSpawningTile()
    {
        List<CustomTile> _tiles = GameData.Instance.m_TileManager.GetEmptyTiles();
        return _tiles[UnityEngine.Random.Range(0, _tiles.Count)]; 
    }

    #region UnityMethods

    #endregion

    #endregion

}
