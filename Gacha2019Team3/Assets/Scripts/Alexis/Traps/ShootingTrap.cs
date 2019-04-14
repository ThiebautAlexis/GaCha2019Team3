using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap : Trap
{

    #region Fields and Properties
    #endregion 

    #region Methods
    /// <summary>
    /// 
    /// </summary>
    protected override IEnumerator TriggerTrap()
    {

        yield return new WaitForSeconds(m_activationTick * GameUpdater.Instance.m_TickEvent);
        GameObject _projectileObject = Instantiate((Resources.Load("TrapsProjectile") as GameObject), transform.position, Quaternion.identity); 
        TrapProjectile _projectile = _projectileObject.GetComponent<TrapProjectile>(); 
        if(_projectile)
        {
            _projectile.InitProjectile(m_GridPosition, new Vector3Int((int)transform.forward.x, 0, (int)transform.forward.z));
        }
        else
        {
            Destroy(_projectileObject); 
        }
        yield return new WaitForSeconds(m_activationTick * GameUpdater.Instance.m_TickEvent);
        CleanTile(); 
        yield break; 
    }


    #region UnityMethods


    #endregion

    #endregion

}
