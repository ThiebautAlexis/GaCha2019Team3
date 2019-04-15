using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    #region Fields and properties
    [Header("Trap Settings")]
    ///Time to wait before spawning
    [SerializeField, Range(4, 50)] protected int m_spawningTick = 4;
    public int m_SpawningTick
    {
        get { return m_spawningTick; }
    }
    /// Time to wait before activation
    [SerializeField, Range(0, 9)] protected int m_activationTick = 1;

    public Vector2Int m_GridPosition { get; set; }

    #endregion

    #region Methods
    /// <summary>
    /// This Method is called when the trap has to trigger
    /// </summary>
    protected abstract IEnumerator TriggerTrap();

    public abstract Vector2Int GetSpawningPosition();

    public abstract Quaternion GetBestOrientation(); 

    /// <summary>
    /// Wait some time before triggering the trap
    /// </summary>
    /// <param name="_waitingTime">Time to wait</param>
    /// <returns></returns>
    protected virtual IEnumerator PrepareTrigger()
    {
        yield return new WaitForSeconds(m_activationTick * GameUpdater.Instance.m_TickEvent);
        StartCoroutine(TriggerTrap());
        yield break; 
    }
    
    protected void CleanTile()
    {
        CustomTile _tile = GameData.Instance.m_TileManager.GetTile(m_GridPosition);
        _tile.m_Entities.Clear(); 
        _tile.m_Walkable = true;
        Destroy(gameObject);
    }

    protected virtual void Start()
    {
        //Set a random Rotation
        transform.rotation = GetBestOrientation(); 
        //Prepare to be triggered
        StartCoroutine(PrepareTrigger());
    }
    #endregion

}
