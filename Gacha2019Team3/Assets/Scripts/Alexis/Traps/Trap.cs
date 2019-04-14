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
    [SerializeField] private string m_prefabName; 
    #endregion

    #region Methods
    /// <summary>
    /// This Method is called when the trap has to trigger
    /// </summary>
    protected abstract void TriggerTrap(); 

    /// <summary>
    /// Wait some time before triggering the trap
    /// </summary>
    /// <param name="_waitingTime">Time to wait</param>
    /// <returns></returns>
    public virtual IEnumerator PrepareTrigger(int _waitedTicks)
    {
        yield return new WaitForSeconds(_waitedTicks * GameUpdater.Instance.m_TickEvent);
        TriggerTrap(); 
    }

    protected virtual void Start()
    {
        //Set a random Rotation
        transform.rotation = Quaternion.Euler(0, 90 * (int)UnityEngine.Random.Range(0, 3), 0);
        //Prepare to be triggered
        StartCoroutine(PrepareTrigger(m_activationTick));
    }

    private void OnDestroy()
    {
        AIManager.Instance?.PrepareSpawnedTrap(m_prefabName); 
    }
    #endregion

}
