using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap : Trap
{

    #region Fields and Properties
    [SerializeField] TrapProjectile m_projectile; 
    #endregion 

    #region Methods
    /// <summary>
    /// 
    /// </summary>
    protected override IEnumerator TriggerTrap()
    {
        TrapProjectile _projectile = Instantiate(m_projectile, transform.position, Quaternion.identity);
        _projectile.InitProjectile(m_GridPosition, new Vector2Int((int)transform.forward.x, (int)transform.forward.z)); 
        Destroy(gameObject); 
        yield break; 
    }


    #region UnityMethods


    #endregion

    #endregion

}
