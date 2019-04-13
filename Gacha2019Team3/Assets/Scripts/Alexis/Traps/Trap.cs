using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    #region Fields and properties
    [Header("Trap Settings")]
    ///Time to wait before spawning
    [SerializeField, Range(4,50)] protected int m_spawningTick = 4; 
    public int SpawningTick
    {
        get { return m_spawningTick;  }
    }
    /// Min Time to wait before activation
    [SerializeField, Range(1, 9)] protected int m_minActivationTick = 1;
    /// Max Time to wait before activation
    [SerializeField, Range(2, 10)]protected int m_maxActivationTick = 2;
    #endregion

    #region Methods
    /// <summary>
    /// This Method is called when the trap has to trigger
    /// </summary>
    protected virtual void TriggerTrap()
    {
        Debug.Log("Trigger Trap"); 
    }

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
    #endregion

}
